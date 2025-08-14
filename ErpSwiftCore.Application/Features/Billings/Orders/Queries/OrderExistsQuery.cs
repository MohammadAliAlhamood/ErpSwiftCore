using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{

    public class OrderExistsQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public OrderExistsQuery(Guid orderId) => OrderId = orderId;
    }
    }

