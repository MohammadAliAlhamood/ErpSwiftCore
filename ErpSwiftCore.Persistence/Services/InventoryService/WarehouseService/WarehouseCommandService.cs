using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService;
namespace ErpSwiftCore.Persistence.Services.InventoryService.WarehouseService
{
    public class WarehouseCommandService : IWarehouseCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IWarehouseValidationService _validation;
        public WarehouseCommandService(IMultiTenantUnitOfWork unitOfWork, IWarehouseValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }
        public async Task<Guid> CreateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
        {
             if (!await _validation.IsValidBranchAsync(warehouse.BranchID, cancellationToken))
                 throw new InvalidOperationException("الفرع غير صالح.");

            if (await _validation.ExistsWithNameAsync(warehouse.Name, warehouse.BranchID, excludeId: null, cancellationToken))
                throw new InvalidOperationException("يوجد مستودع بنفس الاسم في هذا الفرع.");

            return await _unitOfWork.Warehouse.CreateAsync(warehouse, cancellationToken);
        }
        public async Task<bool> UpdateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
        {
            if (!await _validation.WarehouseExistsByIdAsync(warehouse.ID, cancellationToken))
                return false;

            if (!await _validation.IsValidBranchAsync(warehouse.BranchID, cancellationToken))
                throw new InvalidOperationException("الفرع غير صالح.");

            if (await _validation.ExistsWithNameAsync(warehouse.Name, warehouse.BranchID, warehouse.ID, cancellationToken))
                throw new InvalidOperationException("يوجد مستودع بنفس الاسم في هذا الفرع.");

            return await _unitOfWork.Warehouse.UpdateAsync(warehouse, cancellationToken);
        }
        public async Task<bool> DeleteWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.DeleteAsync(warehouseId, cancellationToken);
        }
        public async Task<int> DeleteWarehousesRangeAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.DeleteRangeAsync(warehouseIds, cancellationToken);
        }
        public async Task<bool> DeleteAllWarehousesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.DeleteAllAsync(cancellationToken);
        }
        public async Task<bool> RestoreWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.RestoreAsync(warehouseId, cancellationToken);
        }
        public async Task<bool> RestoreWarehousesRangeAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.RestoreRangeAsync(warehouseIds, cancellationToken);
        }
        public async Task<bool> RestoreAllWarehousesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.RestoreAllAsync(cancellationToken);
        }
        public async Task<IEnumerable<Guid>> AddWarehousesRangeAsync(IEnumerable<Warehouse> warehouses, CancellationToken cancellationToken = default)
        {
            foreach (var wh in warehouses)
            {
                if (!await _validation.IsValidBranchAsync(wh.BranchID, cancellationToken))
                    throw new InvalidOperationException($"الفرع غير صالح: {wh.BranchID}");
                if (await _validation.ExistsWithNameAsync(wh.Name, wh.BranchID, excludeId: null, cancellationToken))
                    throw new InvalidOperationException($"يوجد مستودع بنفس الاسم في هذا الفرع: {wh.Name}");
            }
            return await _unitOfWork.Warehouse.AddRangeAsync(warehouses, cancellationToken);
        }
        public async Task<int> BulkImportWarehousesAsync(IEnumerable<Warehouse> warehouses, CancellationToken cancellationToken = default)
        {
            return (await _unitOfWork.Warehouse.AddRangeAsync(warehouses, cancellationToken)).Count();
        }
        public async Task<int> BulkDeleteWarehousesAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.DeleteRangeAsync(warehouseIds, cancellationToken);
        } 
    }
}
