using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using MediatR;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{

    /// <summary>
    /// 4. Add single order line
    /// </summary>
    public class AddOrderLineCommand : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public CreateOrderLineDto Dto { get; }

        public AddOrderLineCommand(Guid orderId, CreateOrderLineDto dto)
        {
            OrderId = orderId;
            Dto = dto;
        }
    }
}
