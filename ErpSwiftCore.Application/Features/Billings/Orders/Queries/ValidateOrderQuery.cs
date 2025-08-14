using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{
    public class ValidateOrderQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public ValidateOrderQuery(Guid orderId) => OrderId = orderId;
    }

}
