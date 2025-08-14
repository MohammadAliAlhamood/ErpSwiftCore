using ErpSwiftCore.Application.Dtos; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{

    public class InvoiceDiscountDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public string DiscountName { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    } 
}
