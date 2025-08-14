using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetInvoiceApprovalsCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceApprovalsCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

}
