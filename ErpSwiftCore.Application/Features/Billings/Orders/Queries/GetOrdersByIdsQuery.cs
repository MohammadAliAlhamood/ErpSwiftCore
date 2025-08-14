using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{
    public class GetOrdersByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> OrderIds { get; }
        public GetOrdersByIdsQuery(IEnumerable<Guid> orderIds) => OrderIds = orderIds;
    }

}
