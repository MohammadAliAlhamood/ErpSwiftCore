using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{ 
    public class UpdateOrderCommand : IRequest<APIResponseDto>
    {
        public UpdateOrderDto Dto { get; }
        public UpdateOrderCommand(UpdateOrderDto dto) => Dto = dto;
    }

}
