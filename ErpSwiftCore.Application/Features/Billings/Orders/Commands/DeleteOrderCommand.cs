using MediatR; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{
    /// <summary>
    /// 3. Delete order
    /// </summary>
    public class DeleteOrderCommand : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public DeleteOrderCommand(Guid orderId) => OrderId = orderId;
    }
}
