using MediatR; 
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{ 
    public class AddOrderLinesCommand : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public IEnumerable<CreateOrderLineDto> Dtos { get; }

        public AddOrderLinesCommand(Guid orderId, IEnumerable<CreateOrderLineDto> dtos)
        {
            OrderId = orderId;
            Dtos = dtos;
        }
    }

}
