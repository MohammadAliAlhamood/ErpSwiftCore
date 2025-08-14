using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetInvoiceApprovalByIdQuery : IRequest<APIResponseDto>
    {
        public Guid ApprovalId { get; }
        public GetInvoiceApprovalByIdQuery(Guid approvalId) => ApprovalId = approvalId;
    }
}
