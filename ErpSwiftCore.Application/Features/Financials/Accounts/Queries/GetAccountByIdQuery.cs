using MediatR;
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Queries
{
    public class GetAccountByIdQuery : IRequest<APIResponseDto>
    {
        public Guid AccountId { get; }
        public GetAccountByIdQuery(Guid accountId) => AccountId = accountId;
    }
}

 