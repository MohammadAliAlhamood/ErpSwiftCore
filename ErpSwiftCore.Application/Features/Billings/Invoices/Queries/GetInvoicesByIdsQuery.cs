using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetInvoicesByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> InvoiceIds { get; }
        public GetInvoicesByIdsQuery(IEnumerable<Guid> invoiceIds) => InvoiceIds = invoiceIds;
    } 
}
