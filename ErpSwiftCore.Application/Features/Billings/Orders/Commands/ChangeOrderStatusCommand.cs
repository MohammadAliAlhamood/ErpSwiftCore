using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{ 
    public class ChangeOrderStatusCommand : IRequest<APIResponseDto>
    {
        public ChangeOrderStatusDto Dto { get; }
        public ChangeOrderStatusCommand(ChangeOrderStatusDto dto) => Dto = dto;
    }

}
