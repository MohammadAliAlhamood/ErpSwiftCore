using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ErpSwiftCore.Persistence.Services.InventoryService.InventoryAdjustmentService
{
    public class InventoryAdjustmentCommandService : IInventoryAdjustmentCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IInventoryAdjustmentValidationService _validation;
        private readonly ILogger<InventoryAdjustmentCommandService> _logger;

        public InventoryAdjustmentCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            IInventoryAdjustmentValidationService validation,
            ILogger<InventoryAdjustmentCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
            _logger = logger;
        }

        /// <summary>
        /// إنشاء تعديل يدوي وتسجيل معاملة مقابلة.
        /// </summary>
        public async Task<Guid> CreateManualAdjustmentAsync(
            Guid productId,
            Guid warehouseId,
            int quantityChanged,
            string reason,
            CancellationToken cancellationToken = default)
        {
            // تحقق من صحة البيانات
            if (!await _validation.IsValidProductAsync(productId, cancellationToken))
                throw new InvalidOperationException("Invalid product.");
            if (!await _validation.IsValidWarehouseAsync(warehouseId, cancellationToken))
                throw new InvalidOperationException("Invalid warehouse.");
            if (!await _validation.IsValidQuantityAsync(quantityChanged, cancellationToken))
                throw new InvalidOperationException("Quantity must be non-zero.");

            // بدء المعاملة
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // 1. إنشاء سجل التعديل
                var adjustment = new InventoryAdjustment
                {
                    ProductID = productId,
                    WarehouseID = warehouseId,
                    QuantityChanged = quantityChanged,
                    Reason = reason
                };
                var adjustmentId = await _unitOfWork.InventoryAdjustment.CreateAsync(adjustment, cancellationToken);

                // 2. تطبيق التعديل على Aggregate المخزون
                var inventory = await _unitOfWork.Inventory
                    .GetByProductAndWarehouseAsync(productId, warehouseId, cancellationToken)
                    ?? throw new InvalidOperationException("Inventory record not found.");

                inventory.AdjustQuantity(quantityChanged, reason, reference: adjustmentId.ToString());
                await _unitOfWork.Inventory.UpdateAsync(inventory, cancellationToken);

                // 3. تسجيل المعاملة في InventoryTransaction
                var transaction = new InventoryTransaction
                {
                    InventoryID = inventory.ID,
                    TransactionType = quantityChanged > 0
                        ? InventoryTransactionType.AdjustmentIncrease
                        : InventoryTransactionType.AdjustmentDecrease,
                    TransactionDate = DateTime.UtcNow,
                    Quantity = quantityChanged,
                    RunningBalance = inventory.QuantityOnHand,
                    ReferenceNumber = adjustmentId.ToString(),
                    Notes = reason
                };
                await _unitOfWork.InventoryTransaction.CreateAsync(transaction, cancellationToken);

                await tx.CommitAsync(cancellationToken);

                return adjustmentId;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error creating manual adjustment for Product {ProductId} in Warehouse {WarehouseId}.",
                    productId, warehouseId);
                throw;
            }
        }

        /// <summary>
        /// حذف تعديل: يعكس أثره على المخزون ويسجل معاملة مضادة ثم يعمل SoftDelete.
        /// </summary>
        public async Task<bool> DeleteAdjustmentAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
        {
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // جلب سجل التعديل
                var adj = await _unitOfWork.InventoryAdjustment.GetByIdAsync(adjustmentId, cancellationToken)
                          ?? throw new InvalidOperationException("Adjustment not found.");

                // جلب سجل المخزون
                var inv = await _unitOfWork.Inventory
                    .GetByProductAndWarehouseAsync(adj.ProductID, adj.WarehouseID, cancellationToken)
                    ?? throw new InvalidOperationException("Inventory record not found.");

                // عكس التعديل
                var reverseQty = -adj.QuantityChanged;
                inv.AdjustQuantity(reverseQty,
                    reason: $"Reversal of Adjustment {adjustmentId}",
                    reference: adjustmentId.ToString());
                await _unitOfWork.Inventory.UpdateAsync(inv, cancellationToken);

                // تسجيل المعاملة المضادة
                var reverseTxn = new InventoryTransaction
                {
                    InventoryID = inv.ID,
                    TransactionType = adj.QuantityChanged > 0
                        ? InventoryTransactionType.AdjustmentDecrease
                        : InventoryTransactionType.AdjustmentIncrease,
                    TransactionDate = DateTime.UtcNow,
                    Quantity = reverseQty,
                    RunningBalance = inv.QuantityOnHand,
                    ReferenceNumber = adjustmentId.ToString(),
                    Notes = $"Reversal of: {adj.Reason}"
                };
                await _unitOfWork.InventoryTransaction.CreateAsync(reverseTxn, cancellationToken);

                // حذف سجل التعديل نرمياً
                await _unitOfWork.InventoryAdjustment.DeleteAsync(adjustmentId, cancellationToken);

                // حفظ وارتكاب
                await tx.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error deleting adjustment {AdjustmentId}.", adjustmentId);
                throw;
            }
        }

        /// <summary>
        /// استرجاع تعديل: يعيد تطبيق أثره على المخزون ويسجل معاملة مقابلة ثم يلغي SoftDelete.
        /// </summary>
        public async Task<bool> RestoreAdjustmentAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
        {
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // جلب سجل التعديل المحذوف نرمياً
                var adj = await _unitOfWork.InventoryAdjustment.GetSoftDeletedByIdAsync(adjustmentId, cancellationToken)
                          ?? throw new InvalidOperationException("Deleted adjustment not found.");

                // استرجاع سجل التعديل
                await _unitOfWork.InventoryAdjustment.RestoreAsync(adjustmentId, cancellationToken);

                // جلب سجل المخزون
                var inv = await _unitOfWork.Inventory
                    .GetByProductAndWarehouseAsync(adj.ProductID, adj.WarehouseID, cancellationToken)
                    ?? throw new InvalidOperationException("Inventory record not found.");

                // إعادة تطبيق التعديل
                inv.AdjustQuantity(adj.QuantityChanged,
                    reason: $"Restore of Adjustment {adjustmentId}",
                    reference: adjustmentId.ToString());
                await _unitOfWork.Inventory.UpdateAsync(inv, cancellationToken);

                // تسجيل المعاملة المرافقة
                var txn = new InventoryTransaction
                {
                    InventoryID = inv.ID,
                    TransactionType = adj.QuantityChanged > 0
                        ? InventoryTransactionType.AdjustmentIncrease
                        : InventoryTransactionType.AdjustmentDecrease,
                    TransactionDate = DateTime.UtcNow,
                    Quantity = adj.QuantityChanged,
                    RunningBalance = inv.QuantityOnHand,
                    ReferenceNumber = adjustmentId.ToString(),
                    Notes = $"Restore of: {adj.Reason}"
                };
                await _unitOfWork.InventoryTransaction.CreateAsync(txn, cancellationToken);

                // حفظ وارتكاب
                await tx.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error restoring adjustment {AdjustmentId}.", adjustmentId);
                throw;
            }
        }
        /// <summary>
        /// حذف مجموعة من التعديلات مع عكس أثر كل منها على المخزون وتوثيق المعاملات المضادة.
        /// </summary>
        public async Task<bool> DeleteAdjustmentsRangeAsync(
            IEnumerable<Guid> adjustmentIds,
            CancellationToken cancellationToken = default)
        {
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var id in adjustmentIds)
                {
                    await DeleteAdjustmentAsync(id, cancellationToken);
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
        /// <summary>
        /// حذف كل التعديلات: يعكس أثرها دفعة واحدة.
        /// </summary>
        public async Task<bool> DeleteAllAdjustmentsAsync(CancellationToken cancellationToken = default)
        {
            // جلب جميع التعديلات غير المحذوفة
            var all = await _unitOfWork.InventoryAdjustment.GetAllAsync(cancellationToken);
            var ids = all.Select(a => a.ID);

            return await DeleteAdjustmentsRangeAsync(ids, cancellationToken);
        }

        /// <summary>
        /// استرجاع مجموعة من التعديلات مع إعادة تطبيق أثرها على المخزون.
        /// </summary>
        public async Task<bool> RestoreAdjustmentsRangeAsync(
            IEnumerable<Guid> adjustmentIds,
            CancellationToken cancellationToken = default)
        {
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var id in adjustmentIds)
                {
                    await RestoreAdjustmentAsync(id, cancellationToken);
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

        /// <summary>
        /// استرجاع كل التعديلات المحذوفة نرمياً، مع إعادة تطبيقها دفعة واحدة.
        /// </summary>
        public async Task<bool> RestoreAllAdjustmentsAsync(CancellationToken cancellationToken = default)
        {
            // جلب جميع التعديلات المحذوفة
            var deleted = await _unitOfWork.InventoryAdjustment.GetSoftDeletedAsync(cancellationToken);
            var ids = deleted.Select(a => a.ID);

            return await RestoreAdjustmentsRangeAsync(ids, cancellationToken);
        }

        /// <summary>
        /// حذف مجموعة تعديلات (Bulk) — يعكس أثرها تماماً مثل DeleteAdjustmentsRangeAsync ويُرجع عددها.
        /// </summary>
        public async Task<int> BulkDeleteAdjustmentsAsync(
            IEnumerable<Guid> adjustmentIds,
            CancellationToken cancellationToken = default)
        {
            var ids = adjustmentIds.ToList();
            await DeleteAdjustmentsRangeAsync(ids, cancellationToken);
            return ids.Count;
        }

        /// <summary>
        /// تحديث مجموعة من التعديلات دفعة واحدة،
        /// ويعكس أي فرق في الكمية على المخزون.
        /// </summary>
        public async Task<bool> UpdateAdjustmentsAsync(
            IEnumerable<InventoryAdjustment> adjustments,
            CancellationToken cancellationToken = default)
        {
            // 1. بدء المعاملة
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var updated in adjustments)
                {
                    // 2. جلب السجل الحالي
                    var existing = await _unitOfWork.InventoryAdjustment
                        .GetByIdAsync(updated.ID, cancellationToken)
                        ?? throw new InvalidOperationException($"Adjustment {updated.ID} not found.");

                    // 3. حساب الفرق في الكمية
                    var delta = updated.QuantityChanged - existing.QuantityChanged;
                    if (delta != 0)
                    {
                        // 3a. عكس الفرق على المخزون
                        var inv = await _unitOfWork.Inventory
                            .GetByProductAndWarehouseAsync(existing.ProductID, existing.WarehouseID, cancellationToken)
                            ?? throw new InvalidOperationException("Inventory record not found.");

                        inv.AdjustQuantity(delta,
                            reason: $"Update Adjustment {existing.ID}",
                            reference: existing.ID.ToString());
                        await _unitOfWork.Inventory.UpdateAsync(inv, cancellationToken);

                        // 3b. تسجيل المعاملة الجديدة
                        var txn = new InventoryTransaction
                        {
                            InventoryID = inv.ID,
                            TransactionType = delta > 0
                                ? InventoryTransactionType.AdjustmentIncrease
                                : InventoryTransactionType.AdjustmentDecrease,
                            TransactionDate = DateTime.UtcNow,
                            Quantity = delta,
                            RunningBalance = inv.QuantityOnHand,
                            ReferenceNumber = existing.ID.ToString(),
                            Notes = $"Applied delta on update: {delta}"
                        };
                        await _unitOfWork.InventoryTransaction.CreateAsync(txn, cancellationToken);
                    }

                    // 4. تحديث حقول التعديل (كمية وسبب وتاريخ)
                    existing.QuantityChanged = updated.QuantityChanged;
                    existing.Reason = updated.Reason;
                    existing.AdjustmentDate = updated.AdjustmentDate;
                    await _unitOfWork.InventoryAdjustment.UpdateAsync(existing, cancellationToken);
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

        /// <summary>
        /// تغيير سبب التعديل لجميع التعديلات ضمن نطاق زمني،
        /// دون التأثير على المخزون (لأن الكمية لم تتغير).
        /// </summary>
        public async Task<bool> UpdateAdjustmentReasonByDateRangeAsync(
            DateTime startDate,
            DateTime endDate,
            string newReason,
            CancellationToken cancellationToken = default)
        {
            // استخدمنا المستودع بما أنه لا حاجة لعكس مخزون
            return await _unitOfWork.InventoryAdjustment
                .UpdateReasonByDateRangeAsync(startDate, endDate, newReason, cancellationToken);
        }

    }
}