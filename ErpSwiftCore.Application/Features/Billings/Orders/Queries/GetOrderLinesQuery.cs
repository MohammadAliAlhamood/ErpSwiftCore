using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{
    public class GetOrderLinesQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public GetOrderLinesQuery(Guid orderId) => OrderId = orderId;
    }

}
