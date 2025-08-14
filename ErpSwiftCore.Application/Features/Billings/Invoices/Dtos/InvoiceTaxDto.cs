using ErpSwiftCore.Application.Dtos; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{
    public class InvoiceTaxDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    } 
}
