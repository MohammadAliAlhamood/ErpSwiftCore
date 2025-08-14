using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.InventoryService.WarehouseService
{
    public class WarehouseValidationService : IWarehouseValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public WarehouseValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> WarehouseExistsByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.ExistsByIdAsync(warehouseId, cancellationToken);
        }
        public async Task<bool> ExistsWithNameAsync(string name, Guid branchId, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.ExistsWithNameAsync(name, branchId, excludeId, cancellationToken);
        }
        public async Task<bool> IsValidBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.ExistsAsync(w => w.BranchID == branchId, cancellationToken);
        }
         
    }
}
