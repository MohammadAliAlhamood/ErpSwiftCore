using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class OrderLine : AuditableEntity
    {
        public Guid OrderId { get; set; }
        public Order  Order { get; set; }
        public Guid ProductId { get; set; }
        public Product  Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal => (Quantity * UnitPrice) - Discount;
    }
}