using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class DeleteAccountsRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> AccountIds { get; }
        public DeleteAccountsRangeCommand(IEnumerable<Guid> accountIds) => AccountIds = accountIds;
    } 
}
