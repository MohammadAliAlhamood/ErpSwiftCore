using MediatR; 
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetInvoicesCountQuery : IRequest<APIResponseDto>
    {
        public InvoiceStatus? Status { get; }
        public GetInvoicesCountQuery(InvoiceStatus? status) => Status = status;
    } 
}
