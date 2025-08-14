using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class RestoreAccountsRangeCommandHandler : BaseHandler<RestoreAccountsRangeCommand>
    {
        private readonly IAccountCommandService _svc;
        public RestoreAccountsRangeCommandHandler(IAccountCommandService svc, ILogger<BaseHandler<RestoreAccountsRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreAccountsRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreAccountsRangeAsync(req.AccountIds, ct);
            return new { Success = ok };
        }
    }


}
