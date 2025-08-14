using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos
{
    public class InvoiceTaxDiscountSummaryDto
    {
        public Guid InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
