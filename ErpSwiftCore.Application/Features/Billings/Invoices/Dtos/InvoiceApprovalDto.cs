using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{
    public class InvoiceApprovalDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    }
}
