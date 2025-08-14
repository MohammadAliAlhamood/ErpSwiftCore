using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Queries
{
    public class GetSoftDeletedAccountByIdQuery : IRequest<APIResponseDto>
    {
        public Guid AccountId { get; }
        public GetSoftDeletedAccountByIdQuery(Guid accountId) => AccountId = accountId;
    }

}
