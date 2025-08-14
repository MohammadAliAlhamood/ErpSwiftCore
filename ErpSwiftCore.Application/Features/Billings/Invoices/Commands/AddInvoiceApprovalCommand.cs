using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Commands
{
    public class AddInvoiceApprovalCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public CreateInvoiceApprovalDto Dto { get; }

        public AddInvoiceApprovalCommand(Guid invoiceId, CreateInvoiceApprovalDto dto)
        {
            InvoiceId = invoiceId;
            Dto = dto;
        }
    }
}
