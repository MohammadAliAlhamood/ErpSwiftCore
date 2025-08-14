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
    public class WarehouseRepository : MultiTenantRepository<Warehouse>, IWarehouseRepository
    {
        private static readonly Expression<Func<Warehouse, object>>[] DefaultIncludes =  {  x => x.Branch    };

        public WarehouseRepository(AppDbContext db, ITenantProvider tenantProvider, 
            IUserProvider userProvider, IIncludeValidator<Warehouse> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        #region Counts & Stats
        public Task<int> CountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
        public Task<int> CountSoftDeletedAsync(CancellationToken cancellationToken = default)
            => base.CountSoftDeletedAsync(null, cancellationToken);
        public Task<int> CountByBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
            => base.CountAsync(w => w.BranchID == branchId, cancellationToken);
        #endregion Counts & Stats
        #region Core CRUD + Soft-Delete
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Warehouse> bundles, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var b in bundles) ids.Add(await CreateAsync(b, cancellationToken));
            return ids;
        }
        public async Task<Guid> CreateAsync(Warehouse entity, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(entity, autoSave: true, cancellationToken);
            return entity.ID;
        }
        public async Task<bool> UpdateAsync(Warehouse entity, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(entity, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(warehouseId, cancellationToken);
            if (entity == null) return false;
            await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<int> DeleteRangeAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(w => warehouseIds.Contains(w.ID), cancellationToken, DefaultIncludes);
            await base.SoftDeleteRangeAsync(list, autoSave: true, cancellationToken);
            return list.Count();
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            await base.SoftDeleteRangeAsync(list, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(w => w.ID == warehouseId, cancellationToken, DefaultIncludes);
            if (entity == null) return false;
            await base.RestoreAsync(entity, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllSoftDeletedAsync(w => warehouseIds.Contains(w.ID), cancellationToken, DefaultIncludes);
            await base.RestoreRangeAsync(list, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
            await base.RestoreRangeAsync(list, autoSave: true, cancellationToken);
            return true;
        }
        #endregion Core CRUD + Soft-Delete
         
        #region Get / Query Operations
        public async Task<Warehouse?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await base.GetAsync(w => w.ID == warehouseId, cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<Warehouse>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);

        }
        public async Task<Warehouse?> GetSoftDeletedByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await base.GetOneSoftDeletedAsync(w => w.ID == warehouseId, cancellationToken, DefaultIncludes);
        }
        #endregion Get / Query Operations

        #region Custom Retrieval
        public async Task<IReadOnlyList<Warehouse>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(w => w.BranchID == branchId, cancellationToken, DefaultIncludes);
        }
        public Task<bool> ExistsByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(w => w.ID == warehouseId, cancellationToken);
        public Task<bool> ExistsWithNameAsync(string name, Guid branchId, Guid? excludeId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(w =>
                   w.Name == name &&
                   w.BranchID == branchId &&
                   (excludeId == null || w.ID != excludeId),
               cancellationToken);
        public async Task<IReadOnlyList<Warehouse>> GetStorageOnlyAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(w => w.IsStorage == true, cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<Warehouse>> GetOperationalWarehousesAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(w => w.IsOperational == true, cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<Warehouse>> GetRecentWarehousesAsync(int maxCount, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            return list.OrderByDescending(w => w.CreatedAt)
                       .Take(maxCount)
                       .ToList()
                       .AsReadOnly();
        }
        public async Task<IReadOnlyList<Warehouse>> GetByIdsAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(w => warehouseIds.Contains(w.ID), cancellationToken, DefaultIncludes);
        }
        #endregion Custom Retrieval

        #region Paging / Filtering / Searching
        public async Task<(IReadOnlyList<Warehouse> Warehouses, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return await GetPagedInternalAsync(pageIndex, pageSize, null, cancellationToken);
        }
        public Task<(IReadOnlyList<Warehouse> Warehouses, int TotalCount)> GetPagedByBranchAsync(Guid branchId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => GetPagedInternalAsync(pageIndex, pageSize, w => w.BranchID == branchId, cancellationToken);
        public async Task<IReadOnlyList<Warehouse>> SearchByNameAsync(string nameTerm, CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(w => w.Name.Contains(nameTerm), cancellationToken, DefaultIncludes);
        }
        private async Task<(IReadOnlyList<Warehouse> Warehouses, int TotalCount)> GetPagedInternalAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<Warehouse, bool>>? filter,
            CancellationToken cancellationToken)
        {
            var data = await base.GetPagedAsync(pageIndex, pageSize, filter, orderBy: w => w.CreatedAt, ascending: false, cancellationToken: cancellationToken, DefaultIncludes);
            var total = await base.CountAsync(filter, cancellationToken);
            return (data, total);
        }
        #endregion Paging / Filtering / Searching
        #region Custom Filter
        public async Task<IReadOnlyList<Warehouse>> GetByFilterAsync(Expression<Func<Warehouse, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);

        }
        #endregion Custom Filter
    }
}