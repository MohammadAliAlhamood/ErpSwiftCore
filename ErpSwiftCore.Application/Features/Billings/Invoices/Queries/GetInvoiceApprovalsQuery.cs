using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetInvoiceApprovalsQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceApprovalsQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }
}
