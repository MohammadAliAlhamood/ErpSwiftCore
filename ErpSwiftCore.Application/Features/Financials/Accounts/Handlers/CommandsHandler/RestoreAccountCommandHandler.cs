using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class RestoreAccountCommandHandler : BaseHandler<RestoreAccountCommand>
    {
        private readonly IAccountCommandService _svc;
        public RestoreAccountCommandHandler(IAccountCommandService svc, ILogger<BaseHandler<RestoreAccountCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreAccountCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreAccountAsync(req.Id, ct);
            return new { Success = ok };
        }
    }


}
