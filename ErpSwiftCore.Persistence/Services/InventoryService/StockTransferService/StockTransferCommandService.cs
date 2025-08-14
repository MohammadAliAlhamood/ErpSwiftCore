using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.Entities.EntityNotification;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService;
using ErpSwiftCore.Notifications.Interface;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Persistence.Services.InventoryService.StockTransferService
{
    /// <summary>
    /// خدمة تنفيذ أوامر نقل المخزون (Stock Transfer) مع استخدام نظام الإشعارات الجديد.
    /// تستبدل إنشـاء إشعارات المخزون القديم بالاستدعاءات إلى INotificationService.
    /// تعتمد على خدمة مخصصة للحصول على المستلمين المناسبين لإشعارات المخزون.
    /// </summary>
    public class StockTransferCommandService : IStockTransferCommandService
    {
        private readonly IMultiTenantUnitOfWork _uow;
        private readonly IStockTransferValidationService _validation;
        private readonly INotificationService _notification;
        private readonly IInventoryNotificationRecipientService _recipients;
        private readonly ILogger<StockTransferCommandService> _logger;

        public StockTransferCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            IStockTransferValidationService validation,
            INotificationService notificationService,
            IInventoryNotificationRecipientService recipientService,
            ILogger<StockTransferCommandService> logger)
        {
            _uow = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validation = validation ?? throw new ArgumentNullException(nameof(validation));
            _notification = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _recipients = recipientService ?? throw new ArgumentNullException(nameof(recipientService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> CreateStockTransferAsync(StockTransfer transfer, CancellationToken cancellationToken = default)
        {
            if (transfer == null) throw new ArgumentNullException(nameof(transfer));
            if (!await _validation.IsValidProductAsync(transfer.ProductID, cancellationToken)) throw new InvalidOperationException("Invalid product.");
            if (!await _validation.IsValidWarehouseAsync(transfer.FromWarehouseID, cancellationToken)) throw new InvalidOperationException("Invalid from warehouse.");
            if (!await _validation.IsValidWarehouseAsync(transfer.ToWarehouseID, cancellationToken)) throw new InvalidOperationException("Invalid to warehouse.");
            if (transfer.FromWarehouseID == transfer.ToWarehouseID) throw new InvalidOperationException("Source and destination cannot be the same.");
            if (!await _validation.IsValidQuantityAsync(transfer.Quantity, cancellationToken)) throw new InvalidOperationException("Invalid quantity.");
            if (await _validation.ExistsDuplicateTransferAsync(
                    transfer.ProductID, transfer.FromWarehouseID, transfer.ToWarehouseID, transfer.TransferDate, null, cancellationToken))
                throw new InvalidOperationException("Duplicate transfer.");

            var sourceInv = await _uow.Inventory.GetByProductAndWarehouseAsync(transfer.ProductID, transfer.FromWarehouseID, cancellationToken)
                              ?? throw new InvalidOperationException("Source inventory not found.");
            if (transfer.Quantity > sourceInv.QuantityOnHand - sourceInv.QuantityReserved) throw new InvalidOperationException("Insufficient stock.");

            var tx = await _uow.BeginTransactionAsync(cancellationToken);
            try
            {
                sourceInv.AdjustQuantity(-transfer.Quantity, $"TransferOut→{_short(transfer.ToWarehouseID)}", transfer.ReferenceNumber);
                await _uow.Inventory.UpdateAsync(sourceInv, cancellationToken);
                await _uow.InventoryTransaction.CreateAsync(new InventoryTransaction
                {
                    InventoryID = sourceInv.ID,
                    TransactionType = InventoryTransactionType.TransferOut,
                    TransactionDate = transfer.TransferDate,
                    Quantity = -transfer.Quantity,
                    RunningBalance = sourceInv.QuantityOnHand,
                    ReferenceNumber = transfer.ReferenceNumber,
                    Notes = transfer.Notes
                }, cancellationToken);

                var destInv = await _uow.Inventory.GetByProductAndWarehouseAsync(transfer.ProductID, transfer.ToWarehouseID, cancellationToken);
                if (destInv == null)
                {
                    destInv = new Inventory
                    {
                        ProductID = transfer.ProductID,
                        WarehouseID = transfer.ToWarehouseID,
                        TenantID = transfer.TenantID
                    };
                    destInv.ID = await _uow.Inventory.CreateAsync(destInv, cancellationToken);
                }
                destInv.AdjustQuantity(+transfer.Quantity, $"TransferIn←{_short(transfer.FromWarehouseID)}", transfer.ReferenceNumber);
                await _uow.Inventory.UpdateAsync(destInv, cancellationToken);
                await _uow.InventoryTransaction.CreateAsync(new InventoryTransaction
                {
                    InventoryID = destInv.ID,
                    TransactionType = InventoryTransactionType.TransferIn,
                    TransactionDate = transfer.TransferDate,
                    Quantity = transfer.Quantity,
                    RunningBalance = destInv.QuantityOnHand,
                    ReferenceNumber = transfer.ReferenceNumber,
                    Notes = transfer.Notes
                }, cancellationToken);

                var id = await _uow.StockTransfer.CreateAsync(transfer, cancellationToken);

                await _notifyIfNeededAsync(sourceInv, InventoryLevelNotificationType.LowStock, transfer, cancellationToken);
                await _notifyIfNeededAsync(destInv, InventoryLevelNotificationType.Overstock, transfer, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return id;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Failed CreateStockTransfer");
                throw;
            }
        }

        public async Task<IEnumerable<Guid>> AddStockTransfersRangeAsync(IEnumerable<StockTransfer> transfers, CancellationToken cancellationToken = default)
        {
            if (transfers == null) throw new ArgumentNullException(nameof(transfers));
            var ids = new List<Guid>();
            foreach (var t in transfers)
            {
                var id = await CreateStockTransferAsync(t, cancellationToken);
                ids.Add(id);
            }
            return ids;
        }

        public async Task<bool> UpdateStockTransferAsync(StockTransfer transfer, CancellationToken cancellationToken = default)
        {
            if (transfer == null) throw new ArgumentNullException(nameof(transfer));
            var existing = await _uow.StockTransfer.GetByIdAsync(transfer.ID, cancellationToken);
            if (existing == null) return false;

            var tx = await _uow.BeginTransactionAsync(cancellationToken);
            try
            {
                // reverse old
                var oldSource = await _uow.Inventory.GetByProductAndWarehouseAsync(existing.ProductID, existing.FromWarehouseID, cancellationToken);
                if (oldSource != null)
                {
                    oldSource.AdjustQuantity(+existing.Quantity, $"RevertOut(Update){_short(existing.ToWarehouseID)}", existing.ReferenceNumber);
                    await _uow.Inventory.UpdateAsync(oldSource, cancellationToken);
                    await _uow.InventoryAdjustment.CreateAsync(new InventoryAdjustment
                    {
                        ProductID = oldSource.ProductID,
                        WarehouseID = oldSource.WarehouseID,
                        QuantityChanged = +existing.Quantity,
                        Reason = $"Revert(Update) Out→{_short(existing.ToWarehouseID)}",
                        AdjustmentDate = existing.TransferDate
                    }, cancellationToken);
                }
                var oldDest = await _uow.Inventory.GetByProductAndWarehouseAsync(existing.ProductID, existing.ToWarehouseID, cancellationToken);
                if (oldDest != null)
                {
                    oldDest.AdjustQuantity(-existing.Quantity, $"RevertIn(Update){_short(existing.FromWarehouseID)}", existing.ReferenceNumber);
                    await _uow.Inventory.UpdateAsync(oldDest, cancellationToken);
                    await _uow.InventoryAdjustment.CreateAsync(new InventoryAdjustment
                    {
                        ProductID = oldDest.ProductID,
                        WarehouseID = oldDest.WarehouseID,
                        QuantityChanged = -existing.Quantity,
                        Reason = $"Revert(Update) In←{_short(existing.FromWarehouseID)}",
                        AdjustmentDate = existing.TransferDate
                    }, cancellationToken);
                }

                // apply new
                await CreateStockTransferAsync(transfer, cancellationToken);

                var success = await _uow.StockTransfer.UpdateAsync(transfer, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return success;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteStockTransferAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            var existing = await _uow.StockTransfer.GetByIdAsync(transferId, cancellationToken);
            if (existing == null) return false;

            var tx = await _uow.BeginTransactionAsync(cancellationToken);
            try
            {
                var src = await _uow.Inventory.GetByProductAndWarehouseAsync(existing.ProductID, existing.FromWarehouseID, cancellationToken);
                if (src != null)
                {
                    src.AdjustQuantity(+existing.Quantity, $"RevertOut(Delete){_short(existing.ToWarehouseID)}", existing.ReferenceNumber);
                    await _uow.Inventory.UpdateAsync(src, cancellationToken);
                    await _uow.InventoryAdjustment.CreateAsync(new InventoryAdjustment
                    {
                        ProductID = src.ProductID,
                        WarehouseID = src.WarehouseID,
                        QuantityChanged = +existing.Quantity,
                        Reason = $"Revert(Delete) Out→{_short(existing.ToWarehouseID)}",
                        AdjustmentDate = existing.TransferDate
                    }, cancellationToken);
                }

                var dst = await _uow.Inventory.GetByProductAndWarehouseAsync(existing.ProductID, existing.ToWarehouseID, cancellationToken);
                if (dst != null)
                {
                    dst.AdjustQuantity(-existing.Quantity, $"RevertIn(Delete){_short(existing.FromWarehouseID)}", existing.ReferenceNumber);
                    await _uow.Inventory.UpdateAsync(dst, cancellationToken);
                    await _uow.InventoryAdjustment.CreateAsync(new InventoryAdjustment
                    {
                        ProductID = dst.ProductID,
                        WarehouseID = dst.WarehouseID,
                        QuantityChanged = -existing.Quantity,
                        Reason = $"Revert(Delete) In←{_short(existing.FromWarehouseID)}",
                        AdjustmentDate = existing.TransferDate
                    }, cancellationToken);
                }

                var success = await _uow.StockTransfer.DeleteAsync(transferId, cancellationToken);
                await tx.CommitAsync(cancellationToken);
                return success;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteStockTransfersRangeAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default)
        {
            var tx = await _uow.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var id in transferIds)
                {
                    await DeleteStockTransferAsync(id, cancellationToken);
                }
                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteAllStockTransfersAsync(CancellationToken cancellationToken = default)
        {
            var all = await _uow.StockTransfer.GetAllAsync(cancellationToken);
            return await DeleteStockTransfersRangeAsync(all.Select(t => t.ID), cancellationToken);
        }

        public async Task<bool> RestoreStockTransferAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            var existing = await _uow.StockTransfer.GetSoftDeletedByIdAsync(transferId, cancellationToken);
            if (existing == null) return false;

            var tx = await _uow.BeginTransactionAsync(cancellationToken);
            try
            {
                await _uow.StockTransfer.RestoreAsync(transferId, cancellationToken);

                existing = await _uow.StockTransfer.GetByIdAsync(transferId, cancellationToken);
                await CreateStockTransferAsync(existing, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> RestoreStockTransfersRangeAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default)
        {
            var tx = await _uow.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var id in transferIds)
                    await RestoreStockTransferAsync(id, cancellationToken);
                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> RestoreAllStockTransfersAsync(CancellationToken cancellationToken = default)
        {
            var all = await _uow.StockTransfer.GetSoftDeletedAsync(cancellationToken);
            return await RestoreStockTransfersRangeAsync(all.Select(t => t.ID), cancellationToken);
        }

        public async Task<int> BulkImportStockTransfersAsync(IEnumerable<StockTransfer> transfers, CancellationToken cancellationToken = default)
        {
            var count = 0;
            foreach (var t in transfers)
            {
                await CreateStockTransferAsync(t, cancellationToken);
                count++;
            }
            return count;
        }

        public async Task<int> BulkDeleteStockTransfersAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default)
        {
            await DeleteStockTransfersRangeAsync(transferIds, cancellationToken);
            return transferIds.Count();
        }

        private async Task _notifyIfNeededAsync(Inventory inventory, InventoryLevelNotificationType levelType, StockTransfer transfer, CancellationToken cancellationToken)
        {
            var policy = await _uow.InventoryPolicy.GetByInventoryIdAsync(inventory.ID, cancellationToken);
            if (policy == null) return;
            bool trigger = levelType == InventoryLevelNotificationType.LowStock
                ? inventory.QuantityOnHand < policy.ReorderLevel
                : inventory.QuantityOnHand > policy.MaxStockLevel;
            if (!trigger) return;

            IEnumerable<Guid> users;
            try { users = await _recipients.GetRecipientsAsync(inventory.ID, levelType, cancellationToken); }
            catch { return; }

            var payload = System.Text.Json.JsonSerializer.Serialize(new
            {
                inventory.ID,
                inventory.ProductID,
                inventory.WarehouseID,
                transfer.TransferDate,
                transfer.Quantity,
                inventory.QuantityOnHand,
                LevelType = levelType.ToString()
            });

            foreach (var u in users)
            {
                var n = new Notification
                {
                    RecipientId = u,
                    Channel = NotificationChannel.InApp,
                    Type = NotificationType.Alert,
                    Title = levelType == InventoryLevelNotificationType.LowStock
                                    ? "Low Stock Alert" : "Overstock Alert",
                    Message = levelType == InventoryLevelNotificationType.LowStock
                                    ? "Stock below reorder level." : "Stock above max level.",
                    PayloadJson = payload,
                    Priority = NotificationPriority.High,
                    TenantID = transfer.TenantID
                };
                try
                {
                    var nid = await _notification.CreateAsync(n, cancellationToken);
                    await _notification.SendAsync(nid, cancellationToken);
                }
                catch { }
            }
        }

        private static string _short(Guid id) => id.ToString("N").Substring(0, 8);
    } 

    /// <summary>
    /// واجهة لتحديد المستلمين لإشعارات مستوى المخزون بناءً على InventoryId والنوع.
    /// يجب تنفيذها بحيث تُرجع GUIDs للمستخدمين المناسبين (مثلاً مدراء المستودع أو قسم المخزون).
    /// </summary>
    public interface IInventoryNotificationRecipientService
    {
        /// <summary>
        /// الحصول على قائمة معرفات المستخدمين الذين يجب إشعارهم عند حدوث تغيّر مستوى المخزون في سجل مخزون معين.
        /// </summary>
        /// <param name="inventoryId">معرف سجل المخزون.</param>
        /// <param name="levelType">نوع الإشعار (انخفاض LowStock أو زيادة Overstock).</param>
        /// <param name="cancellationToken">رمز الإلغاء.</param>
        /// <returns>قائمة معرفات المستخدمين.</returns>
        Task<IEnumerable<Guid>> GetRecipientsAsync(Guid inventoryId, InventoryLevelNotificationType levelType, CancellationToken cancellationToken = default);
    }
    public class InventoryNotificationRecipientService : IInventoryNotificationRecipientService
    {
        private readonly IMultiTenantUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public InventoryNotificationRecipientService(
            IMultiTenantUnitOfWork uow,
            IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<IEnumerable<Guid>> GetRecipientsAsync(
            Guid inventoryId,
            InventoryLevelNotificationType levelType,
            CancellationToken cancellationToken = default)
        {
            // مثال: جلب مدراء المستودع المرتبط بالسجل
            var inventory = await _uow.Inventory.GetByIdAsync(inventoryId, cancellationToken);
            if (inventory == null)
                return Enumerable.Empty<Guid>();

            // نفترض أن لديك مستودعاً لـ WarehouseRepository يمكنه إرجاع المدير
            var warehouse = await _uow.Warehouse.GetByIdAsync(inventory.WarehouseID, cancellationToken);
            if (warehouse == null || warehouse.CreatedBy == Guid.Empty)
                return Enumerable.Empty<Guid>();

            // يمكن أيضاً إضافة المنطق بناءً على levelType
            return new[] { warehouse.CreatedBy };
        }
    }


    /// <summary>
    /// نوع إشعار مستوى المخزون لتحديد LowStock أو Overstock.
    /// يُستخدم داخلياً لتمييز الرسالة والـpayload.
    /// </summary>
    public enum InventoryLevelNotificationType
    {
        LowStock,
        Overstock
    }
}