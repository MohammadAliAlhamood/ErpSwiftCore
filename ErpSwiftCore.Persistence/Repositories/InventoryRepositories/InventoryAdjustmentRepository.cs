using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories.IInventoryRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Repositories.InventoryRepositories
{
    /// <summary>
    /// Repository implementation for managing inventory adjustment records.
    /// Inherits from MultiTenantRepository to support multi-tenant logic and soft delete.
    /// </summary>
    public class InventoryAdjustmentRepository :
        MultiTenantRepository<InventoryAdjustment>,
        IInventoryAdjustmentRepository
    {
        private static readonly Expression<Func<InventoryAdjustment, object>>[] DefaultIncludes =
        {  x => x.Product,   x => x.Warehouse };

        public InventoryAdjustmentRepository(AppDbContext db, ITenantProvider tenantProvider, 
            IUserProvider userProvider, IIncludeValidator<InventoryAdjustment> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        #region Core CRUD
        public async Task<Guid> CreateAsync(InventoryAdjustment adjustment, CancellationToken cancellationToken = default)
        {
            await AddAsync(adjustment, autoSave: true, cancellationToken);
            return adjustment.ID;
        }
        public async Task<bool> UpdateAsync(InventoryAdjustment adjustment, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(adjustment, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
        {
            var adjustment = await GetByIdAsync(adjustmentId, cancellationToken);
            if (adjustment == null) return false;

            await SoftDeleteAsync(adjustment, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
        {
            var adjustment = await GetOneSoftDeletedAsync(adj => adj.ID == adjustmentId, cancellationToken, DefaultIncludes);
            if (adjustment == null) return false;

            await RestoreAsync(adjustment, autoSave: true, cancellationToken);
            return true;
        }
        public Task<InventoryAdjustment?> GetByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
        {
            return GetAsync(adj => adj.ID == adjustmentId, cancellationToken, DefaultIncludes);
        }
        #endregion Core CRUD

        #region Get All & Paged

        public async Task<IReadOnlyList<InventoryAdjustment>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
         }
        public Task<(IReadOnlyList<InventoryAdjustment> Adjustments, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return GetPagedInternalAsync(pageIndex, pageSize, null, cancellationToken);
        }
        private async Task<(IReadOnlyList<InventoryAdjustment> Adjustments, int TotalCount)> GetPagedInternalAsync(
            int pageIndex, int pageSize,
            Expression<Func<InventoryAdjustment, bool>>? filter,
            CancellationToken cancellationToken)
        {
            var data = await base.GetPagedAsync(pageIndex, pageSize, filter,
                                                orderBy: adj => adj.AdjustmentDate, ascending: false,
                                                cancellationToken: cancellationToken, DefaultIncludes);
            var total = await base.CountAsync(filter, cancellationToken);
            return (data , total);
        }

        #endregion Get All & Paged

        #region Filtering Methods
        public Task<IReadOnlyList<InventoryAdjustment>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => GetFilteredListAsync(adj => adj.ProductID == productId, cancellationToken);

        public Task<IReadOnlyList<InventoryAdjustment>> GetByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => GetFilteredListAsync(adj => adj.WarehouseID == warehouseId, cancellationToken);

        public Task<IReadOnlyList<InventoryAdjustment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
            => GetFilteredListAsync(adj => adj.AdjustmentDate >= startDate && adj.AdjustmentDate <= endDate, cancellationToken);

        public Task<IReadOnlyList<InventoryAdjustment>> SearchByReasonAsync(string reasonTerm, CancellationToken cancellationToken = default)
            => GetFilteredListAsync(adj => adj.Reason.Contains(reasonTerm), cancellationToken);

        public async Task<IReadOnlyList<InventoryAdjustment>> GetRecentAdjustmentsAsync(int maxCount = 10, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            return list.OrderByDescending(adj => adj.AdjustmentDate).Take(maxCount).ToList().AsReadOnly();
        }

        private async Task<IReadOnlyList<InventoryAdjustment>> GetFilteredListAsync(Expression<Func<InventoryAdjustment, bool>> predicate, CancellationToken cancellationToken)
        {
            var list = await base.GetAllAsync(predicate, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        #endregion Filtering Methods

        #region SoftDeleted

        public Task<InventoryAdjustment?> GetOneSoftDeletedAsync(Expression<Func<InventoryAdjustment, bool>> filter, CancellationToken cancellationToken = default)
            => base.GetOneSoftDeletedAsync(filter, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<InventoryAdjustment>> GetAllSoftDeletedAsync(Expression<Func<InventoryAdjustment, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            return await base.GetAllSoftDeletedAsync(filter, cancellationToken, DefaultIncludes);
         }

        #endregion SoftDeleted

        #region Validation

        public Task<bool> ExistsByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(adj => adj.ID == adjustmentId, cancellationToken);

        public Task<bool> ExistsForProductAndWarehouseAsync(Guid productId, Guid warehouseId, DateTime adjustmentDate, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return base.ExistsAsync(adj =>  adj.ProductID == productId &&  adj.WarehouseID == warehouseId &&   adj.AdjustmentDate.Date == adjustmentDate.Date &&  (excludeId == null || adj.ID != excludeId),   cancellationToken);
        }

        public Task<bool> ExistsForProductAndWarehouseOnDateAsync(Guid productId, Guid warehouseId, DateTime adjustmentDate, Guid? excludeId = null, CancellationToken cancellationToken = default)
            => ExistsForProductAndWarehouseAsync(productId, warehouseId, adjustmentDate, excludeId, cancellationToken);

        #endregion Validation

        #region Analytics

        public Task<int> CountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => base.CountAsync(adj => adj.WarehouseID == warehouseId, cancellationToken);

        public Task<int> CountAdjustmentsByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => CountByWarehouseAsync(warehouseId, cancellationToken);

        public async Task<int> SumQuantityChangedByProductAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var list = await GetByDateRangeAsync(from, to, cancellationToken);
            return list.Where(adj => adj.ProductID == productId).Sum(adj => adj.QuantityChanged);
        }

        public Task<int> SumQuantityChangeByProductAndDateRangeAsync(Guid productId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
            => SumQuantityChangedByProductAsync(productId, startDate, endDate, cancellationToken);

        public Task<IReadOnlyList<InventoryAdjustment>> GetRecentAsync(int maxCount = 10, CancellationToken cancellationToken = default)
            => GetRecentAdjustmentsAsync(maxCount, cancellationToken);

        #region Bulk / Batch Operations

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(adj => adjustmentIds.Contains(adj.ID), cancellationToken, DefaultIncludes);
            await SoftDeleteRangeAsync(list, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            await SoftDeleteRangeAsync(list, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllSoftDeletedAsync(adj => adjustmentIds.Contains(adj.ID), cancellationToken, DefaultIncludes);
            await RestoreRangeAsync(list, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
            await RestoreRangeAsync(list, autoSave: true, cancellationToken);
            return true;
        }

        #endregion Bulk / Batch Operations
        #region State & Query Helpers
        public async Task<IReadOnlyList<InventoryAdjustment>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
         }
        public async Task<IReadOnlyList<InventoryAdjustment>> GetByIdsAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(adj => adjustmentIds.Contains(adj.ID), cancellationToken, DefaultIncludes);
         
        }

        #endregion State & Query Helpers

        #region Advanced Paging
        public Task<(IReadOnlyList<InventoryAdjustment> Adjustments, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => GetPagedInternalAsync(pageIndex, pageSize, adj => adj.ProductID == productId, cancellationToken);
        public Task<(IReadOnlyList<InventoryAdjustment> Adjustments, int TotalCount)> GetPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => GetPagedInternalAsync(pageIndex, pageSize, adj => adj.WarehouseID == warehouseId, cancellationToken);
        #endregion Advanced Paging

        #region Search by Message
        public Task<IReadOnlyList<InventoryAdjustment>> SearchByMessageAsync(string messageTerm, CancellationToken cancellationToken = default)
            => GetFilteredListAsync(adj => adj.Reason.Contains(messageTerm), cancellationToken);
        #endregion Search by Message

        

        #region Relations
        public Task<InventoryAdjustment?> GetWithProductAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => GetAsync(adj => adj.ID == adjustmentId, cancellationToken, DefaultIncludes);
        public Task<InventoryAdjustment?> GetWithWarehouseAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => GetAsync(adj => adj.ID == adjustmentId, cancellationToken, DefaultIncludes);
        #endregion Relations

        #region Counts & Stats
        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
        #endregion Counts & Stats

        #region Bulk Import/Delete
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<InventoryAdjustment> adjustments, CancellationToken cancellationToken = default)
        {
            await base.AddRangeAsync(adjustments, autoSave: true, cancellationToken);
            return adjustments.Select(a => a.ID);
        }
        public async Task<int> BulkImportAsync(IEnumerable<InventoryAdjustment> adjustments, CancellationToken cancellationToken = default)
        {
            await base.AddRangeAsync(adjustments, autoSave: true, cancellationToken);
            return adjustments.Count();
        }
        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(adj => adjustmentIds.Contains(adj.ID), cancellationToken, DefaultIncludes);
            await base.RemoveRangeAsync(list, autoSave: true);
            return list.Count();
        }
        #endregion Bulk Import/Delete

        #region Custom Filter
        public async Task<IReadOnlyList<InventoryAdjustment>> GetByFilterAsync(Expression<Func<InventoryAdjustment, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        }
        #endregion Custom Filter
        public Task<InventoryAdjustment?> GetSoftDeletedByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => base.GetOneSoftDeletedAsync(adj => adj.ID == adjustmentId, cancellationToken, DefaultIncludes);

        public Task<bool> UpdateRangeAsync(IEnumerable<InventoryAdjustment> adjustments, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReasonByDateRangeAsync(DateTime startDate, DateTime endDate, string newReason, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion Analytics
    }
}