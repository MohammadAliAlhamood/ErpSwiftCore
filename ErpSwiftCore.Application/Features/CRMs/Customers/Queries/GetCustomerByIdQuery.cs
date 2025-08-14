using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{ 
    public class GetCustomerByIdQuery : IRequest<APIResponseDto>
    {
        public Guid CustomerId { get; }
        public GetCustomerByIdQuery(Guid customerId) => CustomerId = customerId;
    } 
}