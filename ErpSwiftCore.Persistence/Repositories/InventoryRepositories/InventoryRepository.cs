using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
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
    public class InventoryRepository : MultiTenantRepository<Inventory>, IInventoryRepository
    {
        private static readonly Expression<Func<Inventory, object>>[] DefaultIncludes =
        {   x => x.Product,x => x.Warehouse ,   x => x.Transactions,x => x.Policy  };
        public InventoryRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<Inventory> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        { }
        public async Task<Guid> CreateAsync(Inventory inventory, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(inventory, true, cancellationToken);
            return inventory.ID;
        } 
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Inventory> inventories, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var inv in inventories)
                ids.Add(await CreateAsync(inv, cancellationToken));
            return ids;
        } 
        public async Task<bool> UpdateAsync(Inventory inventory, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(inventory, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid inventoryId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(i => i.ID == inventoryId, cancellationToken: cancellationToken);
            if (entity is null) return false;
            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default)
        {
            var items = await GetByIdsAsync(inventoryIds, cancellationToken);
            await base.SoftDeleteRangeAsync(items, true, cancellationToken);
            return items.Count;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            await base.SoftDeleteRangeAsync(all, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAsync(Guid inventoryId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(i => i.ID == inventoryId, cancellationToken);
            if (entity is null) return false;
            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<int> BulkRestoreAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await GetSoftDeletedAsync(cancellationToken);
            var toRestore = SoftDeleted.Where(i => inventoryIds.Contains(i.ID)).ToList();
            await base.RestoreRangeAsync(toRestore, true, cancellationToken);
            return toRestore.Count;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await GetSoftDeletedAsync(cancellationToken);
            await base.RestoreRangeAsync(SoftDeleted, true, cancellationToken);
            return true;
        }
        public Task<Inventory?> GetByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == inventoryId, cancellationToken, DefaultIncludes);
        public Task<Inventory?> GetWithProductAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == inventoryId, cancellationToken, DefaultIncludes);
        public Task<Inventory?> GetWithWarehouseAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == inventoryId, cancellationToken, DefaultIncludes);
        public Task<Inventory?> GetWithTransactionsAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == inventoryId, cancellationToken, DefaultIncludes);
        public Task<Inventory?> GetWithPolicyAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == inventoryId, cancellationToken, DefaultIncludes);
        public Task<Inventory?> GetWithNotificationsAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == inventoryId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Inventory>> GetAllAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
        public Task<Inventory?> GetSoftDeletedByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.GetOneSoftDeletedAsync(i => i.ID == inventoryId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Inventory>> GetByFilterAsync(Expression<Func<Inventory, bool>> filter, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Inventory>> GetByIdsAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(i => inventoryIds.Contains(i.ID), cancellationToken, DefaultIncludes);
        public Task<Inventory?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ProductID == productId && i.WarehouseID == warehouseId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Inventory>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(i => i.ProductID == productId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Inventory>> GetByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(i => i.WarehouseID == warehouseId, cancellationToken, DefaultIncludes);
        public async Task<(IReadOnlyList<Inventory> Inventories, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(null, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, null, i => i.ID, true, cancellationToken, DefaultIncludes);
            return (items, total);
        }
        public async Task<(IReadOnlyList<Inventory> Inventories, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<Inventory, bool>> filter = i => i.ProductID == productId;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, i => i.ID, true, cancellationToken, DefaultIncludes);
            return (items, total);
        }
        public async Task<(IReadOnlyList<Inventory> Inventories, int TotalCount)> GetPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<Inventory, bool>> filter = i => i.WarehouseID == warehouseId;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, i => i.ID, true, cancellationToken, DefaultIncludes);
            return (items, total);
        }
        public async Task<IReadOnlyList<Inventory>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(productName)) return Array.Empty<Inventory>();
            var lowered = productName.ToLower();
            return await base.GetAllAsync(i => i.Product != null && i.Product.Name.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<Inventory>> SearchByWarehouseNameAsync(string warehouseName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(warehouseName)) return Array.Empty<Inventory>();
            var lowered = warehouseName.ToLower();
            return await base.GetAllAsync(i => i.Warehouse != null && i.Warehouse.Name.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }
        public Task<bool> ExistsByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(i => i.ID == inventoryId, cancellationToken);
        public Task<bool> ExistsForProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(i => i.ProductID == productId && i.WarehouseID == warehouseId, cancellationToken);
        public Task<bool> ExistsForProductAndWarehouseOnDateAsync(Guid productId, Guid warehouseId, DateTime date, Guid? excludeId = null, CancellationToken cancellationToken = default)
            => base.ExistsAsync(
                i => i.ProductID == productId
                  && i.WarehouseID == warehouseId
                  && i.CreatedAt.Date == date.Date
                  && (!excludeId.HasValue || i.ID != excludeId.Value),
                cancellationToken);
        public Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(i => i.ProductID == productId, cancellationToken);
        public Task<bool> IsValidWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(i => i.WarehouseID == warehouseId, cancellationToken);
        public Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default)
            => Task.FromResult(quantity >= 0);
        public Task<int> CountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
        public Task<int> CountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.CountAsync(i => i.ProductID == productId, cancellationToken);
        public Task<int> CountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => base.CountAsync(i => i.WarehouseID == warehouseId, cancellationToken);
        public Task<IReadOnlyList<Inventory>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}