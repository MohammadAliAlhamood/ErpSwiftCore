using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService
{
    public interface IWarehouseValidationService
    {
        // -------------------- [Existence & Validation] --------------------
        Task<bool> WarehouseExistsByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<bool> ExistsWithNameAsync(string name, Guid branchId, Guid? excludeId = null, CancellationToken cancellationToken = default);
         Task<bool> IsValidBranchAsync(Guid branchId, CancellationToken cancellationToken = default);




    }
}
