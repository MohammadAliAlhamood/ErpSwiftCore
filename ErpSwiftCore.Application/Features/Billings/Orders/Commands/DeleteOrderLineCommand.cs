using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{ 
    public class DeleteOrderLineCommand : IRequest<APIResponseDto>
    {
        public Guid OrderLineId { get; }
        public DeleteOrderLineCommand(Guid orderLineId) => OrderLineId = orderLineId;
    }
}
