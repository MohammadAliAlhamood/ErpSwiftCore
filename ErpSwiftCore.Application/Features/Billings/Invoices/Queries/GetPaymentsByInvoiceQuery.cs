using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetPaymentsByInvoiceQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetPaymentsByInvoiceQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }
}
