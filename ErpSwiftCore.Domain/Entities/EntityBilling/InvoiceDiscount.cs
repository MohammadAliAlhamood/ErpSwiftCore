using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class InvoiceDiscount : AuditableEntity
    {
        public Guid InvoiceId { get; set; }
        public Invoice  Invoice { get; set; }
        public string DiscountName { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}