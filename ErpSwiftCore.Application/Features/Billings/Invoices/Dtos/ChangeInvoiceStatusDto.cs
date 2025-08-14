using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{
    public class ChangeInvoiceStatusDto
    {
        public Guid InvoiceId { get; set; }
        public InvoiceStatus NewStatus { get; set; }
    } 
}
