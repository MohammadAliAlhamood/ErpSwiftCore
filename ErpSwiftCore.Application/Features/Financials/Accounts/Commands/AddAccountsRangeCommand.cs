using MediatR; 
using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class AddAccountsRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<CreateAccountDto> Accounts { get; }
        public AddAccountsRangeCommand(IEnumerable<CreateAccountDto> accounts) => Accounts = accounts;
    }
}
