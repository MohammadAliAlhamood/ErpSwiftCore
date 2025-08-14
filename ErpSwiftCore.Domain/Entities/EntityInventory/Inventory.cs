using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots
{
    public class Inventory : AuditableEntity
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public Guid WarehouseID { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public int QuantityOnHand { get; private set; }
        public int QuantityReserved { get; private set; }
        public InventoryPolicy Policy { get; set; } = null!;
        public ICollection<InventoryTransaction> Transactions { get; set; } = new List<InventoryTransaction>();
        
        public void AdjustQuantity(int quantityDelta, string reason, string? reference = null)
        {
            QuantityOnHand += quantityDelta;
            if (QuantityOnHand < 0) throw new InvalidOperationException("Cannot have negative stock.");

            Transactions.Add(new InventoryTransaction
            {
                TransactionType = quantityDelta > 0 ? InventoryTransactionType.AdjustmentIncrease : InventoryTransactionType.AdjustmentDecrease,
                Quantity = quantityDelta,
                RunningBalance = QuantityOnHand,
                ReferenceNumber = reference,
                Notes = reason
            });
        }
        public void ReserveQuantity(int quantity)
        {
            if (quantity > QuantityOnHand - QuantityReserved)
                throw new InvalidOperationException("Insufficient stock to reserve.");
            QuantityReserved += quantity;
        }
        public void ReleaseReservedQuantity(int quantity)
        {
            QuantityReserved = Math.Max(0, QuantityReserved - quantity);
        }
    }
}