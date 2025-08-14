using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService
{
    public interface IWarehouseQueryService
    {
        Task<Warehouse?> GetWarehouseByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<Warehouse?> GetSoftDeletedWarehouseByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<Warehouse?> GetWarehouseWithBranchAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<Warehouse>> GetWarehousesByBranchAsync(Guid branchId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Warehouse>> GetStorageOnlyWarehousesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Warehouse>> GetOperationalWarehousesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Warehouse>> GetRecentWarehousesAsync(int maxCount = 10, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Warehouse>> GetWarehousesByIdsAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default);
               // -------------------- [Relations: Inventories/Branch] --------------------
        Task<IReadOnlyList<Inventory>> GetInventoriesByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<Warehouse?> GetWarehouseWithInventoriesAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        // -------------------- [Counts & Stats] --------------------
        Task<int> GetWarehousesCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetWarehousesCountByBranchAsync(Guid branchId, CancellationToken cancellationToken = default);
        Task<int> GetTotalProductsInWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetTotalInventoriesInWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        // -------------------- [Analytics & Custom Use Cases] --------------------
        Task<decimal> GetAverageStockLevelAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetLowStockCountAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetOverstockedCountAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, int>> GetInventoryCountPerWarehouseAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Warehouse>> GetAllWarehousesAsync(CancellationToken cancellationToken = default);
    }
}