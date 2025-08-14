using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Payments.Commands
{
    public class UpdatePaymentCommand : IRequest<APIResponseDto>
    {
        public UpdatePaymentDto Dto { get; }
        public UpdatePaymentCommand(UpdatePaymentDto dto) => Dto = dto;
    }

}
