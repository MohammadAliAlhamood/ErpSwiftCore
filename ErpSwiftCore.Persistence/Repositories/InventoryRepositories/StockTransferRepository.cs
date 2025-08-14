using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IRepositories.IInventoryRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;
namespace ErpSwiftCore.Persistence.Repositories.InventoryRepositories
{
    public class StockTransferRepository : MultiTenantRepository<StockTransfer>, IStockTransferRepository
    {
        private static readonly Expression<Func<StockTransfer, object>>[] DefaultIncludes =
          {  x => x.Product , x => x.FromWarehouse , x => x.ToWarehouse   };

        public StockTransferRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<StockTransfer> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }
        public async Task<Guid> CreateAsync(StockTransfer transfer, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(transfer, true, cancellationToken);
            return transfer.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<StockTransfer> transfers, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var t in transfers)
                ids.Add(await CreateAsync(t, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(StockTransfer transfer, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(transfer, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(t => t.ID == transferId, cancellationToken: cancellationToken);
            if (entity is null) return false;
            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default)
        {
            var items = await GetByIdsAsync(transferIds, cancellationToken);
            await base.SoftDeleteRangeAsync(items, true, cancellationToken);
            return items.Count;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            await base.SoftDeleteRangeAsync(all, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(t => t.ID == transferId, cancellationToken);
            if (entity is null) return false;
            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<int> BulkRestoreAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await GetSoftDeletedAsync(cancellationToken);
            var toRestore = SoftDeleted.Where(t => transferIds.Contains(t.ID)).ToList();
            await base.RestoreRangeAsync(toRestore, true, cancellationToken);
            return toRestore.Count;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await GetSoftDeletedAsync(cancellationToken);
            await base.RestoreRangeAsync(SoftDeleted, true, cancellationToken);
            return true;
        }
        public Task<bool> ExistsByIdAsync(Guid transferId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(t => t.ID == transferId, cancellationToken);
        public async Task<bool> ExistsDuplicateTransferAsync(
            Guid productId,
            Guid fromWarehouseId,
            Guid toWarehouseId,
            DateTime transferDate,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(
                t =>
                    t.ProductID == productId &&
                    t.FromWarehouseID == fromWarehouseId &&
                    t.ToWarehouseID == toWarehouseId &&
                    t.TransferDate.Date == transferDate.Date &&
                    (!excludeId.HasValue || t.ID != excludeId.Value),
                cancellationToken);
        }
        public Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default)
            => Task.FromResult(quantity > 0);
        public Task<int> CountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
        public Task<int> CountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.CountAsync(t => t.ProductID == productId, cancellationToken);
        public Task<int> CountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => base.CountAsync(t => t.FromWarehouseID == warehouseId || t.ToWarehouseID == warehouseId, cancellationToken);
        public async Task<StockTransfer?> GetByIdAsync(Guid transferId, CancellationToken cancellationToken = default)
            => await base.GetAsync(t => t.ID == transferId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetByIdsAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => transferIds.Contains(t.ID), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetAllAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetActiveAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
            =>  await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes) ;
        public Task<StockTransfer?> GetSoftDeletedByIdAsync(Guid transferId, CancellationToken cancellationToken = default)
            => base.GetOneSoftDeletedAsync(t => t.ID == transferId, cancellationToken);
        public async Task<IReadOnlyList<StockTransfer>> GetByFilterAsync(Expression<Func<StockTransfer, bool>> filter, CancellationToken cancellationToken = default)
            =>  await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.TransferDate >= from && t.TransferDate <= to, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
            =>  await base.GetAllAsync(t => t.ProductID == productId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetByFromWarehouseAsync(Guid fromWarehouseId, CancellationToken cancellationToken = default)
            =>  await base.GetAllAsync(t => t.FromWarehouseID == fromWarehouseId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetByToWarehouseAsync(Guid toWarehouseId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.ToWarehouseID == toWarehouseId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            =>  await base.GetAllAsync(    t => t.FromWarehouseID == warehouseId || t.ToWarehouseID == warehouseId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<StockTransfer>> GetBetweenWarehousesAsync(Guid fromWarehouseId, Guid toWarehouseId, CancellationToken cancellationToken = default)
            =>  await base.GetAllAsync(  t => t.FromWarehouseID == fromWarehouseId && t.ToWarehouseID == toWarehouseId,   cancellationToken, DefaultIncludes) ;
        public async Task<StockTransfer?> GetLastTransferForProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => (await GetByProductIdAsync(productId, cancellationToken))
               .OrderByDescending(t => t.TransferDate)
               .FirstOrDefault();
        public async Task<StockTransfer?> GetLastTransferForWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => (await GetByWarehouseAsync(warehouseId, cancellationToken))
               .OrderByDescending(t => t.TransferDate)
               .FirstOrDefault();
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(null, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, null, t => t.TransferDate, false, cancellationToken, DefaultIncludes);
            return (items.ToList().AsReadOnly(), total);
        }
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedByActiveStatusAsync(bool isActive, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            // active = not soft-deleted when isActive=true
            Expression<Func<StockTransfer, bool>> filter = t => t.IsDeleted == !isActive;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, t => t.TransferDate, false, cancellationToken, DefaultIncludes);
            return (items.ToList().AsReadOnly(), total);
        }
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<StockTransfer, bool>> filter = t => t.ProductID == productId;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, t => t.TransferDate, false, cancellationToken, DefaultIncludes);
            return (items.ToList().AsReadOnly(), total);
        }
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<StockTransfer, bool>> filter = t => t.FromWarehouseID == warehouseId || t.ToWarehouseID == warehouseId;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, t => t.TransferDate, false, cancellationToken, DefaultIncludes);
            return (items.ToList().AsReadOnly(), total);
        }

        // ----------- [GetWith] -----------

        public Task<StockTransfer?> GetWithProductAsync(Guid transferId, CancellationToken cancellationToken = default)
            => base.GetAsync(t => t.ID == transferId,   cancellationToken, DefaultIncludes);

        public Task<StockTransfer?> GetWithWarehousesAsync(Guid transferId, CancellationToken cancellationToken = default)
            => base.GetAsync(t => t.ID == transferId, cancellationToken, DefaultIncludes);

        // ----------- [Search] -----------

        public async Task<IReadOnlyList<StockTransfer>> SearchByNotesAsync(string noteTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(noteTerm)) return Array.Empty<StockTransfer>();
            var lower = noteTerm.ToLower();
            return (await base.GetAllAsync(t => t.Notes != null && t.Notes.ToLower().Contains(lower), cancellationToken, DefaultIncludes))
                   .ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<StockTransfer>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(productName)) return Array.Empty<StockTransfer>();
            var lower = productName.ToLower();
            return  await base.GetAllAsync(   t => t.Product != null && t.Product.Name.ToLower().Contains(lower),  cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<StockTransfer>> SearchByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(referenceNumber)) return Array.Empty<StockTransfer>();
            var lower = referenceNumber.ToLower();
            return await base.GetAllAsync(t => t.ReferenceNumber != null && t.ReferenceNumber.ToLower().Contains(lower), cancellationToken, DefaultIncludes);
        }
    }
}