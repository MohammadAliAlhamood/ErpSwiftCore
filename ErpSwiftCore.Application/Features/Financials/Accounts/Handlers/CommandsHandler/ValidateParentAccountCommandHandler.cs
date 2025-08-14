using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class ValidateParentAccountCommandHandler : BaseHandler<ValidateParentAccountCommand>
    {
        private readonly IAccountValidationService _validation;

        public ValidateParentAccountCommandHandler(IAccountValidationService validation, ILogger<BaseHandler<ValidateParentAccountCommand>> logger) : base(logger)
        {
            _validation = validation;
        }
        protected override async Task<object?> HandleRequestAsync(ValidateParentAccountCommand req, CancellationToken ct)
        {
            return await _validation.IsValidParentAccountAsync(req.Id, ct);
        }
    }

}
