using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class InvoiceExistsQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public InvoiceExistsQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }
}
