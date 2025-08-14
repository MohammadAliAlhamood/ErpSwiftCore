using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class ValidateParentAccountCommand : IRequest<APIResponseDto>
    {
        public Guid Id { get; }
        public ValidateParentAccountCommand(Guid id) => Id = id;
    }
}
