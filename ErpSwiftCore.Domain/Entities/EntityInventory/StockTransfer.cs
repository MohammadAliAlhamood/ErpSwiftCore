using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.Domain.Entities.EntityProduct;
namespace ErpSwiftCore.Domain.Entities.EntityInventory
{
    public class StockTransfer : AuditableEntity
    {
        public Guid ProductID { get; set; }
        public Product  Product { get; set; }
        public Guid FromWarehouseID { get; set; }
        public Warehouse  FromWarehouse { get; set; }
        public Guid ToWarehouseID { get; set; }
        public Warehouse ToWarehouse { get; set; }
        public int Quantity { get; set; }
        public DateTime TransferDate { get; set; } = DateTime.UtcNow;
        public string? ReferenceNumber { get; set; }
        public string? Notes { get; set; }
    }
}