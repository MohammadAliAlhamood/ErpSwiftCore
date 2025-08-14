using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService
{
    public interface IInventoryAdjustmentValidationService
    {



        // -------------------- [Existence & Validation] --------------------
        Task<bool> AdjustmentExistsByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> IsValidWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default);
        Task<bool> ExistsForProductAndWarehouseOnDateAsync(Guid productId, Guid warehouseId, DateTime date, Guid? excludeId = null, CancellationToken cancellationToken = default);



    }
}
