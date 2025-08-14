using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class RestoreAccountsRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> AccountIds { get; }
        public RestoreAccountsRangeCommand(IEnumerable<Guid> accountIds) => AccountIds = accountIds;
    }
}
