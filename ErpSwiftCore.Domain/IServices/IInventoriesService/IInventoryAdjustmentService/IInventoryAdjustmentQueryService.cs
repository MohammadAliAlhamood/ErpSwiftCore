using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService
{
    public interface IInventoryAdjustmentQueryService
    {

        // -------------------- [Get/Query Operations - Single] --------------------
        Task<InventoryAdjustment?> GetAdjustmentByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        Task<InventoryAdjustment?> GetSoftDeletedAdjustmentByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryAdjustment>> GetAllAdjustmentsAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByProductAsync(Guid productId, Guid? warehouseId = null, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByDateRangeAsync(Guid? warehouseId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
         Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByIdsAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default);
                Task<int> GetAdjustmentsCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetAdjustmentsCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> GetAdjustmentsCountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> GetCurrentStockAfterAdjustmentsAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
        Task<Dictionary<string, int>> GetAdjustmentCountsByReasonAsync(Guid? warehouseId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
        Task<InventoryAdjustment?> GetAdjustmentWithProductAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        Task<InventoryAdjustment?> GetAdjustmentWithWarehouseAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        // -------------------- [Advanced/Custom Queries] --------------------
         Task<IReadOnlyList<InventoryAdjustment>> GetAllSoftDeletedAdjustmentsAsync(CancellationToken cancellationToken = default);
        Task<int> SumQuantityChangeByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<InventoryAdjustment?> GetLastAdjustmentAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByStockTakeIdAsync(Guid stockTakeId, CancellationToken cancellationToken = default);


    }
}
