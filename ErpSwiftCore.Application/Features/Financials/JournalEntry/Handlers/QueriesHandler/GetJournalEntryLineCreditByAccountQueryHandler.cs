using ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Handlers.QueriesHandler
{
    public class GetJournalEntryLineCreditByAccountQueryHandler
       : BaseHandler<GetJournalEntryLineCreditByAccountQuery>
    {
        private readonly IJournalEntryQueryService _svc;

        public GetJournalEntryLineCreditByAccountQueryHandler(
            IJournalEntryQueryService svc,
            ILogger<BaseHandler<GetJournalEntryLineCreditByAccountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override Task<object?> HandleRequestAsync(
            GetJournalEntryLineCreditByAccountQuery req,
            CancellationToken ct)
        {
            return _svc.GetJournalEntryLineCreditByAccountAsync(req.AccountId, ct)
                       .ContinueWith(t => (object?)t.Result, ct);
        }
    }
}
