using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Queries
{
    public class GetAccountsByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> AccountIds { get; }
        public GetAccountsByIdsQuery(IEnumerable<Guid> accountIds) => AccountIds = accountIds;
    }

}
