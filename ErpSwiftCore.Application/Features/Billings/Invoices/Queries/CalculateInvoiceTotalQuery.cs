using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class CalculateInvoiceTotalQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public CalculateInvoiceTotalQuery(Guid invoiceId) => InvoiceId = invoiceId;
    } 
}
