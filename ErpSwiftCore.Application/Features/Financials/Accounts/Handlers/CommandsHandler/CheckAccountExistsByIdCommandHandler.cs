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
    public class CheckAccountExistsByIdCommandHandler : BaseHandler<CheckAccountExistsByIdCommand>
    {
        private readonly IAccountValidationService _validation;
        public CheckAccountExistsByIdCommandHandler(IAccountValidationService validation, ILogger<BaseHandler<CheckAccountExistsByIdCommand>> logger) : base(logger)
        {
            _validation = validation;
        }
        protected override async Task<object?> HandleRequestAsync(CheckAccountExistsByIdCommand req, CancellationToken ct)
        {
            return await _validation.AccountExistsByIdAsync(req.Id, ct);
        }
    }

}
