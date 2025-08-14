using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryService
{
    public interface IInventoryQueryService
    {


        // -------------------- [Get/Query Operations - Single] --------------------
        Task<Inventory?> GetInventoryByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetSoftDeletedInventoryByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetInventoryByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<Inventory>> GetAllInventoriesAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<Inventory>> GetInventoriesByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Inventory>> GetInventoriesByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Inventory>> GetInventoriesByFilterAsync(Expression<Func<Inventory, bool>> filter, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Inventory>> GetInventoriesByIdsAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default);
             // -------------------- [Relations: Product/Warehouse/Policy/Transactions/Notifications] --------------------
        Task<Inventory?> GetInventoryWithProductAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetInventoryWithWarehouseAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetInventoryWithPolicyAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetInventoryWithTransactionsAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetInventoryWithNotificationsAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        // -------------------- [Counts & Stats] --------------------
        Task<int> GetInventoriesCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetInventoriesCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> GetInventoriesCountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetTotalAvailableQuantityByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetTotalReservedQuantityByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, int>> GetProductAvailabilityAcrossWarehousesAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, int>> GetStockSummaryByProductAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Inventory>> GetBelowReorderLevelAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Inventory>> GetAboveMaxStockLevelAsync(CancellationToken cancellationToken = default);
        Task<decimal> CalculateInventoryValueAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<decimal> GetAverageStockLevelAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetLowStockCountAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetOverstockedCountAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<decimal> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
         Task<IReadOnlyList<InventoryTransaction>> GetInventoryTransactionsAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<InventoryPolicy?> GetInventoryPolicyAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetCurrentInventorySnapshotAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetCurrentStockAfterAdjustmentsAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
        Task<Inventory?> GetLastInventoryRecordAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> SumQuantityChangeByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default);


    }
}
