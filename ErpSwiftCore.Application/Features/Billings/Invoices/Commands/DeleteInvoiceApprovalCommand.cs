using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Commands
{
    public class DeleteInvoiceApprovalCommand : IRequest<APIResponseDto>
    {
        public Guid ApprovalId { get; }
        public DeleteInvoiceApprovalCommand(Guid approvalId) => ApprovalId = approvalId;
    }

}
