using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{

    public class GetOrderLinesCountQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public GetOrderLinesCountQuery(Guid orderId) => OrderId = orderId;
    }

}
