using ErpSwiftCore.Domain.Entities.EntityInventory;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IInventoryRepositories
{
    /// <summary>
    /// Repository interface for managing StockTransfer records.
    /// Covers CRUD, bulk, retrieval, search, analytics, paging, validation, relations, and custom use cases for stock transfers.
    /// </summary>
    public interface IStockTransferRepository : IMultiTenantRepository<StockTransfer>
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAsync(StockTransfer transfer, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(StockTransfer transfer, CancellationToken cancellationToken = default);

        // -------------------- [Bulk Operations] --------------------
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<StockTransfer> transfers, CancellationToken cancellationToken = default);

        Task<int> BulkDeleteAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default);

        Task<int> BulkRestoreAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default);

        // -------------------- [Delete/Archive/Restore Operations] --------------------
        Task<bool> DeleteAsync(Guid transferId, CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid transferId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Single] --------------------
        Task<StockTransfer?> GetByIdAsync(Guid transferId, CancellationToken cancellationToken = default);

        Task<StockTransfer?> GetSoftDeletedByIdAsync(Guid transferId, CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<StockTransfer>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetActiveAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetByFromWarehouseAsync(Guid fromWarehouseId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetByToWarehouseAsync(Guid toWarehouseId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetBetweenWarehousesAsync(Guid fromWarehouseId, Guid toWarehouseId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetByFilterAsync(Expression<Func<StockTransfer, bool>> filter, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> GetByIdsAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering/Searching] --------------------
        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetPagedByActiveStatusAsync(bool isActive, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> SearchByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockTransfer>> SearchByNotesAsync(string noteTerm, CancellationToken cancellationToken = default);

        // -------------------- [Existence & Validation] --------------------
        Task<bool> ExistsByIdAsync(Guid transferId, CancellationToken cancellationToken = default);

        Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default);

        Task<bool> ExistsDuplicateTransferAsync(Guid productId, Guid fromWarehouseId, Guid toWarehouseId, DateTime transferDate, Guid? excludeId = null, CancellationToken cancellationToken = default);

        // -------------------- [Relations: Product/Warehouse] --------------------
        Task<StockTransfer?> GetWithProductAsync(Guid transferId, CancellationToken cancellationToken = default);

        Task<StockTransfer?> GetWithWarehousesAsync(Guid transferId, CancellationToken cancellationToken = default);

        // -------------------- [Counts & Stats] --------------------
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<int> CountByProductAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<int> CountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);

        // -------------------- [Analytics & Custom Use Cases] --------------------
        Task<StockTransfer?> GetLastTransferForProductAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<StockTransfer?> GetLastTransferForWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
    }
}