using MediatR; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{
    public class OrderLineExistsQuery : IRequest<APIResponseDto>
    {
        public Guid OrderLineId { get; }
        public OrderLineExistsQuery(Guid orderLineId) => OrderLineId = orderLineId;
    }

}
