using MediatR; 
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.Payments.Commands
{
    public class AddPaymentCommand : IRequest<APIResponseDto>
    {
        public AddPaymentDto Dto { get; }
        public AddPaymentCommand(AddPaymentDto dto) => Dto = dto;
    } 
}
