using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class InvoiceLine : AuditableEntity
    {
        public Guid InvoiceId { get; set; }
        public Invoice  Invoice { get; set; }
        public Guid ProductId { get; set; }
        public Product  Product { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal => Quantity * UnitPrice - Discount;
    }
}