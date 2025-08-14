using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{ 
    public class DeleteAllOrderLinesCommand : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public DeleteAllOrderLinesCommand(Guid orderId) => OrderId = orderId;
    }
}
