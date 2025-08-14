using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService
{
    public interface IStockTransferQueryService
    {
       // -------------------- [Get/Query Operations - Single] --------------------
        Task<StockTransfer?> GetStockTransferByIdAsync(Guid transferId, CancellationToken cancellationToken = default);
        Task<StockTransfer?> GetSoftDeletedStockTransferByIdAsync(Guid transferId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetAllStockTransfersAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetActiveStockTransfersAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetSoftDeletedStockTransfersAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetStockTransfersByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetStockTransfersByFromWarehouseAsync(Guid fromWarehouseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetStockTransfersByToWarehouseAsync(Guid toWarehouseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetStockTransfersByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetStockTransfersByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetStockTransfersByFilterAsync(Expression<Func<StockTransfer, bool>> filter, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> GetStockTransfersByIdsAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default);
        // -------------------- [Paging/Filtering/Searching - Specialized] --------------------
        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedByActiveStatusAsync(bool isActive, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        // -------------------- [Search Operations] --------------------
        Task<IReadOnlyList<StockTransfer>> SearchStockTransfersByProductNameAsync(string productName, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> SearchStockTransfersByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<StockTransfer>> SearchStockTransfersByNotesAsync(string noteTerm, CancellationToken cancellationToken = default);
       // -------------------- [Relations: Product/Warehouse] --------------------
        Task<StockTransfer?> GetStockTransferWithProductAsync(Guid transferId, CancellationToken cancellationToken = default);
        Task<StockTransfer?> GetStockTransferWithWarehousesAsync(Guid transferId, CancellationToken cancellationToken = default);
        // -------------------- [Counts & Stats] --------------------
        Task<int> GetStockTransfersCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetStockTransfersCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> GetStockTransfersCountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        // -------------------- [Analytics & Custom Use Cases] --------------------
        Task<int> GetTotalTransferredQuantityByProductAsync(Guid productId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
        Task<int> GetTotalTransferredQuantityByWarehouseAsync(Guid warehouseId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
        Task<StockTransfer?> GetLastStockTransferForProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<StockTransfer?> GetLastStockTransferForWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
    }
}