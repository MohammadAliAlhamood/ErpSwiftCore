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
    public class CheckAccountExistsByNumberCommandHandler : BaseHandler<CheckAccountExistsByNumberCommand>
    {
        private readonly IAccountValidationService _validation;
        public CheckAccountExistsByNumberCommandHandler(IAccountValidationService validation, ILogger<BaseHandler<CheckAccountExistsByNumberCommand>> logger) : base(logger)
        {
            _validation = validation;
        }
        protected override async Task<object?> HandleRequestAsync(CheckAccountExistsByNumberCommand req, CancellationToken ct)
        {
            return await _validation.AccountExistsByNumberAsync(req.AccountNumber, ct);
        }
    }

}
