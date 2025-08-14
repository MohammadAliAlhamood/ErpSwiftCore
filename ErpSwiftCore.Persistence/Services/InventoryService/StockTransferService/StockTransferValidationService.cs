using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService;
namespace ErpSwiftCore.Persistence.Services.InventoryService.StockTransferService
{
    public class StockTransferValidationService : IStockTransferValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public StockTransferValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> StockTransferExistsByIdAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.ExistsByIdAsync(transferId, cancellationToken);
        }
        public async Task<bool> ExistsDuplicateTransferAsync(Guid productId, Guid fromWarehouseId, Guid toWarehouseId, DateTime transferDate, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.ExistsDuplicateTransferAsync(productId, fromWarehouseId, toWarehouseId, transferDate, excludeId, cancellationToken);
        }
        public async Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Product.ExistsAsync(p => p.ID == productId, cancellationToken);
        }
        public Task<bool> IsValidQuantityAsync(int quantity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(quantity > 0);
        }
        public async Task<bool> IsValidWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.ExistsByIdAsync(warehouseId, cancellationToken);
        }
        

        public async Task<bool> IsValidTransferAsync(Guid productId, Guid fromWarehouseId, Guid toWarehouseId, int quantity, CancellationToken cancellationToken = default)
        {
            if (!await IsValidProductAsync(productId, cancellationToken)) return false;
            if (!await IsValidWarehouseAsync(fromWarehouseId, cancellationToken)) return false;
            if (!await IsValidWarehouseAsync(toWarehouseId, cancellationToken)) return false;
            if (fromWarehouseId == toWarehouseId) return false;
            if (!await IsValidQuantityAsync(quantity, cancellationToken)) return false;
            var sourceInv = await _unitOfWork.Inventory.GetByProductAndWarehouseAsync(productId, fromWarehouseId, cancellationToken);
            if (sourceInv == null || quantity > sourceInv.QuantityOnHand - sourceInv.QuantityReserved)
                return false;

            return true;
        }
    }
}
