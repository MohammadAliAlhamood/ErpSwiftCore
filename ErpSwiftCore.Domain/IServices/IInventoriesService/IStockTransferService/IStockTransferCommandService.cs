using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService
{
    public interface IStockTransferCommandService
    { 
        Task<Guid> CreateStockTransferAsync(StockTransfer transfer, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddStockTransfersRangeAsync(IEnumerable<StockTransfer> transfers, CancellationToken cancellationToken = default);
        Task<bool> UpdateStockTransferAsync(StockTransfer transfer, CancellationToken cancellationToken = default);
        // -------------------- [Delete/Archive/Restore Operations] --------------------
        Task<bool> DeleteStockTransferAsync(Guid transferId, CancellationToken cancellationToken = default);
        Task<bool> DeleteStockTransfersRangeAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllStockTransfersAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreStockTransferAsync(Guid transferId, CancellationToken cancellationToken = default);
        Task<bool> RestoreStockTransfersRangeAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllStockTransfersAsync(CancellationToken cancellationToken = default);
        // -------------------- [Bulk Operations] --------------------
        Task<int> BulkImportStockTransfersAsync(IEnumerable<StockTransfer> transfers, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteStockTransfersAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default);


    }
}
