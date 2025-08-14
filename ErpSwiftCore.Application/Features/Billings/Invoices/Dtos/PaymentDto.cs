using ErpSwiftCore.Application.Dtos; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{
    public class PaymentDto : AuditableEntityDto
    {
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsReconciled { get; set; }
        public Guid InvoiceId { get; set; }
    }
}
