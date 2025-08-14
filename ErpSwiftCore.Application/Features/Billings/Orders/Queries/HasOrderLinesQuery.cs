using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{

    public class HasOrderLinesQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public HasOrderLinesQuery(Guid orderId) => OrderId = orderId;
    }


}
