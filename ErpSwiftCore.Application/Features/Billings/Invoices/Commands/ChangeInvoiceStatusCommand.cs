using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Commands
{
    public class ChangeInvoiceStatusCommand : IRequest<APIResponseDto>
    {
        public ChangeInvoiceStatusDto Dto { get; }
        public ChangeInvoiceStatusCommand(ChangeInvoiceStatusDto dto) => Dto = dto;
    }

}
