using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class RestoreAccountCommand : IRequest<APIResponseDto>
    {
        public Guid Id { get; }
        public RestoreAccountCommand(Guid id) => Id = id;
    }

}
