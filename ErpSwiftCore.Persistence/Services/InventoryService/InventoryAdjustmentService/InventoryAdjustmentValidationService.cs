using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.InventoryService.InventoryAdjustmentService
{
    public class InventoryAdjustmentValidationService : IInventoryAdjustmentValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public InventoryAdjustmentValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AdjustmentExistsByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.ExistsByIdAsync(adjustmentId, cancellationToken);
        public async Task<bool> ExistsForProductAndWarehouseOnDateAsync(Guid productId, Guid warehouseId, DateTime date, Guid? excludeId = null, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment
                .ExistsForProductAndWarehouseOnDateAsync(productId, warehouseId, date, excludeId, cancellationToken);
        public async Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment
                .ExistsAsync(adj => adj.ProductID == productId, cancellationToken);
        public async Task<bool> IsValidWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment
                .ExistsAsync(adj => adj.WarehouseID == warehouseId, cancellationToken);
        public Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default)
            => Task.FromResult(quantity != 0);


    }
}
