using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.ICore;
using System.Linq.Expressions;

namespace ErpSwiftCore.Domain.IRepositories.IInventoryRepositories
{
    /// <summary>
    /// Repository interface for managing inventory adjustment records.
    /// Covers CRUD, retrieval, search, analytics, and validation scenarios.
    /// </summary>
    public interface IInventoryAdjustmentRepository : IMultiTenantRepository<InventoryAdjustment>
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAsync(InventoryAdjustment adjustment, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(InventoryAdjustment adjustment, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid adjustmentId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid adjustmentId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
         
        // -------------------- [Get/Query Operations - Single] --------------------
        Task<InventoryAdjustment?> GetByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        Task<InventoryAdjustment?> GetSoftDeletedByIdAsync(Guid adjustmentId, CancellationToken cancellationToken);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<InventoryAdjustment>> GetAllAsync(CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<InventoryAdjustment>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryAdjustment>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryAdjustment>> GetByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryAdjustment>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryAdjustment>> GetByIdsAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering/Searching - Specialized] --------------------
        Task<(IReadOnlyList<InventoryAdjustment> Adjustments, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<InventoryAdjustment> Adjustments, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<InventoryAdjustment> Adjustments, int TotalCount)> GetPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
         
        // -------------------- [Search Operations] --------------------
        Task<IReadOnlyList<InventoryAdjustment>> SearchByReasonAsync(string reasonTerm, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryAdjustment>> SearchByMessageAsync(string messageTerm, CancellationToken cancellationToken = default);

        // -------------------- [Existence & Validation] --------------------
        Task<bool> ExistsByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default);

        Task<bool> ExistsForProductAndWarehouseOnDateAsync(Guid productId, Guid warehouseId, DateTime adjustmentDate, Guid? excludeId = null, CancellationToken cancellationToken = default);

        // -------------------- [Relations] --------------------
        Task<InventoryAdjustment?> GetWithProductAsync(Guid adjustmentId, CancellationToken cancellationToken = default);

        Task<InventoryAdjustment?> GetWithWarehouseAsync(Guid adjustmentId, CancellationToken cancellationToken = default);

        // -------------------- [Counts & Stats] --------------------
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);

        Task<int> CountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);

        Task<int> SumQuantityChangedByProductAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

        // -------------------- [Bulk Operations] --------------------
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<InventoryAdjustment> adjustments, CancellationToken cancellationToken = default);

        Task<int> BulkImportAsync(IEnumerable<InventoryAdjustment> adjustments, CancellationToken cancellationToken = default);

        Task<int> BulkDeleteAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default);

        // -------------------- [Advanced/Custom Queries] --------------------
        Task<IReadOnlyList<InventoryAdjustment>>
            GetByFilterAsync(Expression<Func<InventoryAdjustment, bool>> filter, CancellationToken cancellationToken = default);
        Task<bool> UpdateRangeAsync(IEnumerable<InventoryAdjustment> adjustments, CancellationToken cancellationToken);
        Task<bool> UpdateReasonByDateRangeAsync(DateTime startDate, DateTime endDate, string newReason, CancellationToken cancellationToken);
    }
}