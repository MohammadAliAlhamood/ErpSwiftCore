using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.InventoryService.InventoryService
{
    public class InventoryQueryService : IInventoryQueryService
    {
        private readonly IMultiTenantUnitOfWork _uow;

        public InventoryQueryService(IMultiTenantUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Inventory?> GetInventoryByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => await _uow.Inventory.GetByIdAsync(inventoryId, cancellationToken);
        public async Task<Inventory?> GetSoftDeletedInventoryByIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => await _uow.Inventory.GetSoftDeletedByIdAsync(inventoryId, cancellationToken);
        public async Task<Inventory?> GetInventoryByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
            => await _uow.Inventory.GetByProductAndWarehouseAsync(productId, warehouseId, cancellationToken);
        public async Task<Inventory?> GetCurrentInventorySnapshotAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
            => await _uow.Inventory.GetByProductAndWarehouseAsync(productId, warehouseId, cancellationToken);
        public async Task<Inventory?> GetLastInventoryRecordAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var inv = await _uow.Inventory.GetByProductAndWarehouseAsync(productId, warehouseId, cancellationToken);
            if (inv == null) return null;

            var lastTxn = await _uow.InventoryTransaction.GetLastTransactionForInventoryAsync(inv.ID, cancellationToken);
            return inv;
        }
        public Task<IReadOnlyList<Inventory>> GetAllInventoriesAsync(CancellationToken cancellationToken = default)
            => _uow.Inventory.GetAllAsync(cancellationToken);

        public Task<IReadOnlyList<Inventory>> GetInventoriesByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetByProductIdAsync(productId, cancellationToken);
        public Task<IReadOnlyList<Inventory>> GetInventoriesByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
        public Task<IReadOnlyList<Inventory>> GetInventoriesByIdsAsync(IEnumerable<Guid> inventoryIds, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetByIdsAsync(inventoryIds, cancellationToken);
        public Task<IReadOnlyList<Inventory>> GetInventoriesByFilterAsync(Expression<Func<Inventory, bool>> filter, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetByFilterAsync(filter, cancellationToken);
        public Task<Inventory?> GetInventoryWithProductAsync(Guid inventoryId, CancellationToken cancellationToken = default)
      => _uow.Inventory.GetWithProductAsync(inventoryId, cancellationToken);
        public Task<Inventory?> GetInventoryWithWarehouseAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetWithWarehouseAsync(inventoryId, cancellationToken);
        public Task<Inventory?> GetInventoryWithPolicyAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetWithPolicyAsync(inventoryId, cancellationToken);
        public Task<Inventory?> GetInventoryWithTransactionsAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetWithTransactionsAsync(inventoryId, cancellationToken);
        public Task<Inventory?> GetInventoryWithNotificationsAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _uow.Inventory.GetWithNotificationsAsync(inventoryId, cancellationToken);
        public async Task<IReadOnlyList<InventoryTransaction>> GetInventoryTransactionsAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => await _uow.InventoryTransaction.GetByInventoryIdAsync(inventoryId, cancellationToken);
        public Task<InventoryPolicy?> GetInventoryPolicyAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _uow.InventoryPolicy.GetByInventoryIdAsync(inventoryId, cancellationToken);
        public Task<int> GetInventoriesCountAsync(CancellationToken cancellationToken = default)
            => _uow.Inventory.CountAsync(cancellationToken);
        public Task<int> GetInventoriesCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => _uow.Inventory.CountByProductAsync(productId, cancellationToken);
        public Task<int> GetInventoriesCountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => _uow.Inventory.CountByWarehouseAsync(warehouseId, cancellationToken);
        public async Task<int> GetLowStockCountAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var all = await _uow.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            var below = new List<Inventory>();
            foreach (var inv in all)
            {
                var policy = await _uow.InventoryPolicy.GetByInventoryIdAsync(inv.ID, cancellationToken);
                if (policy != null && inv.QuantityOnHand < policy.ReorderLevel)
                    below.Add(inv);
            }
            return below.Count;
        }
        public async Task<int> GetOverstockedCountAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var all = await _uow.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            var over = new List<Inventory>();
            foreach (var inv in all)
            {
                var policy = await _uow.InventoryPolicy.GetByInventoryIdAsync(inv.ID, cancellationToken);
                if (policy != null && inv.QuantityOnHand > policy.MaxStockLevel)
                    over.Add(inv);
            }
            return over.Count;
        }
        public async Task<Dictionary<Guid, int>> GetProductAvailabilityAcrossWarehousesAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var inventories = await _uow.Inventory.GetByProductIdAsync(productId, cancellationToken);
            return inventories.ToDictionary(i => i.WarehouseID, i => i.QuantityOnHand - i.QuantityReserved);
        }
        public async Task<Dictionary<Guid, int>> GetStockSummaryByProductAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            var dict = new Dictionary<Guid, int>();
            foreach (var pid in productIds)
            {
                var invs = await _uow.Inventory.GetByProductIdAsync(pid, cancellationToken);
                var total = invs.Sum(i => i.QuantityOnHand - i.QuantityReserved);
                dict[pid] = total;
            }
            return dict;
        }
        public async Task<int> GetTotalAvailableQuantityByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _uow.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            return invs.Sum(i => i.QuantityOnHand - i.QuantityReserved);
        }
        public async Task<int> GetTotalReservedQuantityByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _uow.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            return invs.Sum(i => i.QuantityReserved);
        }
        public async Task<decimal> CalculateInventoryValueAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            // حساب قيمة المخزون يتطلب مستودع أسعار المنتجات (غير منجز حالياً)
            throw new NotImplementedException("Requires ProductPrice repository to fetch current product prices.");
        }
        public async Task<decimal> GetAverageStockLevelAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var invs = await _uow.Inventory.GetByWarehouseIdAsync(warehouseId, cancellationToken);
            if (!invs.Any()) return 0m;
            var sum = invs.Sum(i => i.QuantityOnHand);
            return (decimal)sum / invs.Count;
        }
        public async Task<decimal> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            // معدل الدوران = (مجموع الكميات المنهكة) / متوسط المخزون
            var allTxns = await _uow.InventoryTransaction.GetByProductIdAsync(productId, cancellationToken);
            var periodTxns = allTxns
                .Where(tx => tx.TransactionDate >= from
                          && tx.TransactionDate <= to
                          && tx.TransactionType == InventoryTransactionType.AdjustmentDecrease)
                .ToList();

            var consumptionSum = periodTxns.Sum(tx => Math.Abs(tx.Quantity));
            var invs = await _uow.Inventory.GetByProductIdAsync(productId, cancellationToken);
            if (!invs.Any())
                return 0m;

            var avgStockDouble = invs.Average(i => i.QuantityOnHand);
            var avgStock = (decimal)avgStockDouble;
            return avgStock == 0m
                ? 0m
                : (decimal)consumptionSum / avgStock;
        }

        public async Task<int> GetCurrentStockAfterAdjustmentsAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var inv = await _uow.Inventory.GetByProductAndWarehouseAsync(productId, warehouseId, cancellationToken);
            if (inv == null) return 0;
            var txns = await _uow.InventoryTransaction.GetByInventoryIdAsync(inv.ID, cancellationToken);
            return txns.Sum(t => t.Quantity);
        }
        public async Task<IReadOnlyList<Inventory>> GetBelowReorderLevelAsync(CancellationToken cancellationToken = default)
        {
            var all = await _uow.Inventory.GetAllAsync(cancellationToken);
            var result = new List<Inventory>();
            foreach (var inv in all)
            {
                var policy = await _uow.InventoryPolicy.GetByInventoryIdAsync(inv.ID, cancellationToken);
                if (policy != null && inv.QuantityOnHand < policy.ReorderLevel)
                    result.Add(inv);
            }
            return result;
        }
        public async Task<IReadOnlyList<Inventory>> GetAboveMaxStockLevelAsync(CancellationToken cancellationToken = default)
        {
            var all = await _uow.Inventory.GetAllAsync(cancellationToken);
            var result = new List<Inventory>();
            foreach (var inv in all)
            {
                var policy = await _uow.InventoryPolicy.GetByInventoryIdAsync(inv.ID, cancellationToken);
                if (policy != null && inv.QuantityOnHand > policy.MaxStockLevel)
                    result.Add(inv);
            }
            return result;
        }
        public Task<int> GetInventoryCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => _uow.Inventory.CountByProductAsync(productId, cancellationToken);
        public Task<int> GetInventoryCountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => _uow.Inventory.CountByWarehouseAsync(warehouseId, cancellationToken);
        public async Task<int> SumQuantityChangeByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            return await _uow.InventoryTransaction.SumQuantityChangeByProductAndDateRangeAsync(productId, from, to, cancellationToken);
        }
    }
}