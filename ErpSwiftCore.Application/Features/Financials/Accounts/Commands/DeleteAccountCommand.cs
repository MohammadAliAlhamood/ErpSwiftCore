using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class DeleteAccountCommand : IRequest<APIResponseDto>
    {
        public Guid Id { get; }
        public DeleteAccountCommand(Guid id) => Id = id;
    }
}
