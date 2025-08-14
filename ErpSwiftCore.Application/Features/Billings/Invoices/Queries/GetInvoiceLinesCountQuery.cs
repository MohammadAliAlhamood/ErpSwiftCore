using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetInvoiceLinesCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceLinesCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }
}
