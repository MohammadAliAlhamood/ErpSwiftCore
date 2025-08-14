using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class InvoiceTax : AuditableEntity
    {
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    }
}