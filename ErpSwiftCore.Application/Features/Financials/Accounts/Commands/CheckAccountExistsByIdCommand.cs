using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{ 
    public class CheckAccountExistsByIdCommand : IRequest<APIResponseDto>
    {
        public Guid Id { get; }
        public CheckAccountExistsByIdCommand(Guid id) => Id = id;
    }

}
