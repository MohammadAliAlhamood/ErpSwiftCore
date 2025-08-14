using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{ 
    public class GetOrderByIdQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public GetOrderByIdQuery(Guid orderId) => OrderId = orderId;
    }

 
  
 
   
}