using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IInventoryRepositories
{
    /// <summary>
    /// Repository interface for managing Inventory aggregates.
    /// Covers CRUD, retrieval, search, analytics, paging, validation, relations, and custom use cases for inventory.
    /// </summary>
    public interface IInventoryRepository : IMultiTenantRepository<Inventory>
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAsync(Inventory inventory, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(Inventory inventory, CancellationToken cancellationToken = default);

        // -------------------- [Bulk Operations] --------------------
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Inventory> inventories, CancellationToken cancellationToken = default);

        Task<int> BulkDeleteAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default);

        Task<int> BulkRestoreAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default);

        // -------------------- [Delete/Archive/Restore Operations] --------------------
        Task<bool> DeleteAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

      
        // -------------------- [Get/Query Operations - Single] --------------------
        Task<Inventory?> GetByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<Inventory?> GetSoftDeletedByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<Inventory?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<Inventory>> GetAllAsync(CancellationToken cancellationToken = default);
 
        Task<IReadOnlyList<Inventory>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Inventory>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Inventory>> GetByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Inventory>> GetByIdsAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Inventory>> GetByFilterAsync(Expression<Func<Inventory, bool>> filter, CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering/Searching - Specialized] --------------------
        Task<(IReadOnlyList<Inventory> Inventories, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Inventory> Inventories, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Inventory> Inventories, int TotalCount)> GetPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<Inventory>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Inventory>> SearchByWarehouseNameAsync(string warehouseName, CancellationToken cancellationToken = default);

        // -------------------- [Existence & Validation] --------------------
        Task<bool> ExistsByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<bool> ExistsForProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);

        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<bool> IsValidWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);

        Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default);

        Task<bool> ExistsForProductAndWarehouseOnDateAsync(Guid productId, Guid warehouseId, DateTime date, Guid? excludeId = null, CancellationToken cancellationToken = default);

        // -------------------- [Relations: Product/Warehouse/Policy/Transactions/Notifications/History] --------------------
        Task<Inventory?> GetWithProductAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<Inventory?> GetWithWarehouseAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<Inventory?> GetWithPolicyAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<Inventory?> GetWithTransactionsAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<Inventory?> GetWithNotificationsAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        // -------------------- [Counts & Stats] --------------------
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<int> CountByProductAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<int> CountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
    }
}