using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class InvoiceApprovalExistsQuery : IRequest<APIResponseDto>
    {
        public Guid ApprovalId { get; }
        public InvoiceApprovalExistsQuery(Guid approvalId) => ApprovalId = approvalId;
    }
}
