using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.Domain.Entities.EntityProduct;
namespace ErpSwiftCore.Domain.Entities.EntityInventory
{
    public class InventoryAdjustment : AuditableEntity
    {
        public Guid ProductID { get; set; }
        public Product  Product { get; set; }
        public Guid WarehouseID { get; set; }
        public Warehouse  Warehouse { get; set; }
        public int QuantityChanged { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime AdjustmentDate { get; set; } = DateTime.UtcNow;
    }
}