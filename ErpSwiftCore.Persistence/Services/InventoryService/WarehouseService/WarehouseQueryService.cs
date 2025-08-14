using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService;
namespace ErpSwiftCore.Persistence.Services.InventoryService.WarehouseService
{
    public class WarehouseQueryService : IWarehouseQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public WarehouseQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Inventory>> GetInventoriesByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
        }
        public async Task<int> GetTotalInventoriesInWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _unitOfWork.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            return invs.Count;
        }
        public async Task<int> GetTotalProductsInWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _unitOfWork.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            return invs.Select(i => i.ProductID).Distinct().Count();
        }
        public async Task<int> GetLowStockCountAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _unitOfWork.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            var count = 0;
            foreach (var inv in invs)
            {
                var policy = await _unitOfWork.InventoryPolicy.GetByInventoryIdAsync(inv.ID, cancellationToken);
                if (policy != null && inv.QuantityOnHand < policy.ReorderLevel)
                    count++;
            }
            return count;
        }
        public async Task<int> GetOverstockedCountAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _unitOfWork.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            var count = 0;
            foreach (var inv in invs)
            {
                var policy = await _unitOfWork.InventoryPolicy.GetByInventoryIdAsync(inv.ID, cancellationToken);
                if (policy != null && inv.QuantityOnHand > policy.MaxStockLevel)
                    count++;
            }
            return count;
        }
        public async Task<decimal> GetAverageStockLevelAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _unitOfWork.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            if (!invs.Any()) return 0m;
            var sum = invs.Sum(i => i.QuantityOnHand);
            return (decimal)sum / invs.Count;
        }
        public async Task<IReadOnlyList<Warehouse>> GetRecentWarehousesAsync(int maxCount = 10, CancellationToken cancellationToken = default)
        {
            var all = await _unitOfWork.Warehouse.GetAllAsync(cancellationToken);
            return all.OrderByDescending(w => w.CreatedAt).Take(maxCount).ToList();
        }
        public async Task<Dictionary<Guid, int>> GetInventoryCountPerWarehouseAsync(CancellationToken cancellationToken = default)
        {
            var allInvs = await _unitOfWork.Inventory.GetAllAsync(cancellationToken);
            return allInvs.GroupBy(i => i.WarehouseID).ToDictionary(g => g.Key, g => g.Count());
        }
        public async Task<int> GetWarehousesCountAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.CountAsync(cancellationToken);
        }
        public async Task<int> GetWarehousesCountByBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.CountByBranchAsync(branchId, cancellationToken);
        }
        public async Task<Warehouse?> GetWarehouseByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetByIdAsync(warehouseId, cancellationToken);
        }
        public async Task<Warehouse?> GetSoftDeletedWarehouseByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetSoftDeletedByIdAsync(warehouseId, cancellationToken);
        }
        public async Task<IReadOnlyList<Warehouse>> GetAllWarehousesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetAllAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<Warehouse>> GetOperationalWarehousesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetOperationalWarehousesAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<Warehouse>> GetStorageOnlyWarehousesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetStorageOnlyAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<Warehouse>> GetWarehousesByBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetByBranchIdAsync(branchId, cancellationToken);
        }
        public async Task<IReadOnlyList<Warehouse>> GetWarehousesByIdsAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetByIdsAsync(warehouseIds, cancellationToken);
        }

        public async Task<Warehouse?> GetWarehouseWithBranchAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetAsync(filter: w => w.ID == warehouseId, cancellationToken);
        }
        public async Task<Warehouse?> GetWarehouseWithInventoriesAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Warehouse.GetAsync(filter: w => w.ID == warehouseId, cancellationToken);
        }

       
    }
}
