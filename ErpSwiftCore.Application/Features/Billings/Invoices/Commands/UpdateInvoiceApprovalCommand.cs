using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Commands
{
    public class UpdateInvoiceApprovalCommand : IRequest<APIResponseDto>
    {
        public UpdateInvoiceApprovalDto Dto { get; }
        public UpdateInvoiceApprovalCommand(UpdateInvoiceApprovalDto dto) => Dto = dto;
    }
}
