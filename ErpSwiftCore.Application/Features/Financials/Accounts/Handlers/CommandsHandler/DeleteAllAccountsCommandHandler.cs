using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class DeleteAllAccountsCommandHandler : BaseHandler<DeleteAllAccountsCommand>
    {
        private readonly IAccountCommandService _svc;
        public DeleteAllAccountsCommandHandler(IAccountCommandService svc, ILogger<BaseHandler<DeleteAllAccountsCommand>> logger) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteAllAccountsCommand req, CancellationToken ct)
        {
            return await _svc.DeleteAllAccountsAsync(ct);
        }
    }

}
