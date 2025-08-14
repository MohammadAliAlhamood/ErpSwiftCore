using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    public class GetCustomersByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CustomerIds { get; }
        public GetCustomersByIdsQuery(IEnumerable<Guid> customerIds) => CustomerIds = customerIds;
    }

}
