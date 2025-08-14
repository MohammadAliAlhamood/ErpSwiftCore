using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetInvoiceByIdQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceByIdQuery(Guid invoiceId) => InvoiceId = invoiceId;
    } 
}