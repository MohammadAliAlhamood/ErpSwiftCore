using ErpSwiftCore.Domain.Entities.EntityInventory;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IInventoryRepositories
{
    /// <summary>
    /// Repository interface for managing Warehouse entities.
    /// Covers CRUD, retrieval, paging, search, analytics, and validation scenarios.
    /// </summary>
    public interface IWarehouseRepository : IMultiTenantRepository<Warehouse>
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAsync(
            Warehouse entity,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Warehouse> bundles, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            Warehouse entity,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid warehouseId,
            CancellationToken cancellationToken = default);

        Task<int> DeleteRangeAsync(
            IEnumerable<Guid> warehouseIds,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(
            Guid warehouseId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> warehouseIds,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(
            CancellationToken cancellationToken = default);
         

        // -------------------- [Get/Query Operations - Single] --------------------
        Task<Warehouse?> GetByIdAsync(
            Guid warehouseId,
            CancellationToken cancellationToken = default);

        Task<Warehouse?> GetSoftDeletedByIdAsync(
            Guid warehouseId,
            CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<Warehouse>> GetAllAsync(
            CancellationToken cancellationToken = default);
         

        Task<IReadOnlyList<Warehouse>> GetSoftDeletedAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Warehouse>> GetByBranchIdAsync(
            Guid branchId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Warehouse>> GetStorageOnlyAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Warehouse>> GetOperationalWarehousesAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Warehouse>> GetRecentWarehousesAsync(
            int maxCount,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Warehouse>> GetByIdsAsync(
            IEnumerable<Guid> warehouseIds,
            CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering/Searching - Specialized] --------------------
        Task<(IReadOnlyList<Warehouse> Warehouses, int TotalCount)> GetPagedAsync(
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Warehouse> Warehouses, int TotalCount)> GetPagedByBranchAsync(
            Guid branchId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);
         

        Task<IReadOnlyList<Warehouse>> SearchByNameAsync(
            string nameTerm,
            CancellationToken cancellationToken = default);

        // -------------------- [Existence & Validation] --------------------
        Task<bool> ExistsWithNameAsync(
            string name,
            Guid branchId,
            Guid? excludeId,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(
            Guid warehouseId,
            CancellationToken cancellationToken = default);

        // -------------------- [Analytics & Stats] --------------------
        Task<int> CountAsync(
            CancellationToken cancellationToken = default);

        Task<int> CountSoftDeletedAsync(
            CancellationToken cancellationToken = default);

        Task<int> CountByBranchAsync(
            Guid branchId,
            CancellationToken cancellationToken = default);
         

        // -------------------- [Advanced/Custom Queries] --------------------
        Task<IReadOnlyList<Warehouse>> GetByFilterAsync(
            Expression<Func<Warehouse, bool>> filter,
            CancellationToken cancellationToken = default);
    }
}