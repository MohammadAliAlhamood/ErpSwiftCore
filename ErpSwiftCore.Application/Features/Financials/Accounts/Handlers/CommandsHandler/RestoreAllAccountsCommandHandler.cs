using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class RestoreAllAccountsCommandHandler : BaseHandler<RestoreAllAccountsCommand>
    {
        private readonly IAccountCommandService _svc;
        public RestoreAllAccountsCommandHandler(IAccountCommandService svc, ILogger<BaseHandler<RestoreAllAccountsCommand>> logger) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreAllAccountsCommand req, CancellationToken ct)
        {
            return await _svc.RestoreAllAccountsAsync(ct);
        }
    }

}
