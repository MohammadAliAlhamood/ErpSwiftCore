using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class DeleteAccountsRangeCommandHandler : BaseHandler<DeleteAccountsRangeCommand>
    {
        private readonly IAccountCommandService _svc;
        public DeleteAccountsRangeCommandHandler(IAccountCommandService svc, ILogger<BaseHandler<DeleteAccountsRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteAccountsRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteAccountsRangeAsync(req.AccountIds, ct);
            return new { Success = ok };
        }
    }

}
