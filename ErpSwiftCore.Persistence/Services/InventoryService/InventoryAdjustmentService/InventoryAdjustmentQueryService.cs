using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService;
using System.Linq.Expressions;
namespace ErpSwiftCore.Persistence.Services.InventoryService.InventoryAdjustmentService
{
    public class InventoryAdjustmentQueryService : IInventoryAdjustmentQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public InventoryAdjustmentQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<InventoryAdjustment?> GetAdjustmentByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetByIdAsync(adjustmentId, cancellationToken);
        public async Task<IReadOnlyList<InventoryAdjustment>> GetAllAdjustmentsAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetAllAsync(cancellationToken);
        public async Task<IReadOnlyList<InventoryAdjustment>> GetAllSoftDeletedAdjustmentsAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetSoftDeletedAsync(cancellationToken);
        public async Task<InventoryAdjustment?> GetSoftDeletedAdjustmentByIdAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetOneSoftDeletedAsync(adj => adj.ID == adjustmentId, cancellationToken);
        public async Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByIdsAsync(IEnumerable<Guid> adjustmentIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetByIdsAsync(adjustmentIds, cancellationToken);
        public async Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByProductAsync(Guid productId, Guid? warehouseId = null, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.InventoryAdjustment.GetByProductIdAsync(productId, cancellationToken);
            return warehouseId.HasValue ? list.Where(adj => adj.WarehouseID == warehouseId).ToList().AsReadOnly() : list;
        }
        public async Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetByWarehouseIdAsync(warehouseId, cancellationToken);
        public async Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByDateRangeAsync(Guid? warehouseId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.InventoryAdjustment.GetByDateRangeAsync(from, to, cancellationToken);
            return warehouseId.HasValue ? list.Where(adj => adj.WarehouseID == warehouseId).ToList().AsReadOnly() : list;
        }
        public Task<IReadOnlyList<InventoryAdjustment>> GetAdjustmentsByStockTakeIdAsync(Guid stockTakeId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.InventoryAdjustment.GetByFilterAsync(adj => adj.Reason.Contains(stockTakeId.ToString()), cancellationToken);
        }

       
        public async Task<Dictionary<string, int>> GetAdjustmentCountsByReasonAsync(Guid? warehouseId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.InventoryAdjustment.GetAllAsync(cancellationToken);
            var filtered = list.Where(adj => (!warehouseId.HasValue || adj.WarehouseID == warehouseId) &&
                                             (!from.HasValue || adj.AdjustmentDate >= from) &&
                                             (!to.HasValue || adj.AdjustmentDate <= to));

            return filtered.GroupBy(adj => adj.Reason).ToDictionary(g => g.Key, g => g.Count());
        }

        public Task<int> GetAdjustmentsCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryAdjustment.GetCountAsync(cancellationToken);

        public async Task<int> GetAdjustmentsCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.InventoryAdjustment.GetByProductIdAsync(productId, cancellationToken);
            return list.Count;
        }

        public async Task<int> GetAdjustmentsCountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.InventoryAdjustment.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            return list.Count;
        }

        public async Task<InventoryAdjustment?> GetAdjustmentWithProductAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetWithProductAsync(adjustmentId, cancellationToken);

        public async Task<InventoryAdjustment?> GetAdjustmentWithWarehouseAsync(Guid adjustmentId, CancellationToken cancellationToken = default)
            => await _unitOfWork.InventoryAdjustment.GetWithWarehouseAsync(adjustmentId, cancellationToken);

        public async Task<InventoryAdjustment?> GetLastAdjustmentAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.InventoryAdjustment.GetByProductIdAsync(productId, cancellationToken);
            return list.Where(adj => adj.WarehouseID == warehouseId)
                       .OrderByDescending(adj => adj.AdjustmentDate)
                       .FirstOrDefault();
        }
        public async Task<int> GetCurrentStockAfterAdjustmentsAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.InventoryAdjustment.GetByProductIdAsync(productId, cancellationToken);
            return list.Where(adj => adj.WarehouseID == warehouseId).Sum(adj => adj.QuantityChanged);
        }
        public Task<int> SumQuantityChangeByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.InventoryAdjustment.SumQuantityChangedByProductAsync(productId, from, to, cancellationToken);
        }

    
    
    }
}
