using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService
{
    public interface IStockTransferValidationService
    {



        // -------------------- [Existence & Validation] --------------------
        Task<bool> StockTransferExistsByIdAsync(Guid transferId, CancellationToken cancellationToken = default);
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> IsValidWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default);
        Task<bool> IsValidTransferAsync(Guid productId, Guid fromWarehouseId, Guid toWarehouseId, int quantity, CancellationToken cancellationToken = default);
        Task<bool> ExistsDuplicateTransferAsync(Guid productId, Guid fromWarehouseId, 
            Guid toWarehouseId, DateTime transferDate, Guid? excludeId = null,
            CancellationToken cancellationToken = default);

    }
}
