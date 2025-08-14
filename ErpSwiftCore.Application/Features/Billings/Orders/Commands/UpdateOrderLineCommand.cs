using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{ 
    public class UpdateOrderLineCommand : IRequest<APIResponseDto>
    {
        public UpdateOrderLineDto Dto { get; }
        public UpdateOrderLineCommand(UpdateOrderLineDto dto) => Dto = dto;
    }
}
