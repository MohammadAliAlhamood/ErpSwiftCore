using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{ 
    public class CreateInvoiceApprovalDto
    {
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    }
}
