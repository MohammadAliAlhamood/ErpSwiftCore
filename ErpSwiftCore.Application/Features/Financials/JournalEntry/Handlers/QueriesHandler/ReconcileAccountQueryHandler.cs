using ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Handlers.QueriesHandler
{
    public class ReconcileAccountQueryHandler
        : BaseHandler<ReconcileAccountQuery>
    {
        private readonly IJournalEntryQueryService _svc;
        public ReconcileAccountQueryHandler(
            IJournalEntryQueryService svc,
            ILogger<BaseHandler<ReconcileAccountQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(ReconcileAccountQuery req, CancellationToken ct)
        {
            var balanceChange = await _svc.ReconcileAccountAsync(req.AccountId, req.From, req.To, ct);
            return new AccountReconcileDto
            {
                AccountId = req.AccountId,
                From = req.From,
                To = req.To,
                BalanceChange = balanceChange
            };
        }
    }
}
