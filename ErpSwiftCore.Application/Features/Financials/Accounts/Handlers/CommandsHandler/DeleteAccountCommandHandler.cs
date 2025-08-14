using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class DeleteAccountCommandHandler : BaseHandler<DeleteAccountCommand>
    {
        private readonly IAccountCommandService _svc;
        public DeleteAccountCommandHandler(IAccountCommandService svc, ILogger<BaseHandler<DeleteAccountCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteAccountCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteAccountAsync(req.Id, ct);
            return new { Success = ok };
        }
    }

}
