using ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Handlers.QueriesHandler
{
    public class GetJournalEntryTotalByAccountQueryHandler
        : BaseHandler<GetJournalEntryTotalByAccountQuery>
    {
        private readonly IJournalEntryQueryService _svc;

        public GetJournalEntryTotalByAccountQueryHandler(
            IJournalEntryQueryService svc,
            ILogger<BaseHandler<GetJournalEntryTotalByAccountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override Task<object?> HandleRequestAsync(
            GetJournalEntryTotalByAccountQuery req,
            CancellationToken ct)
        {
            return _svc.GetJournalEntryTotalByAccountAsync(req.AccountId, ct)
                       .ContinueWith(t => (object?)t.Result, ct);
        }
    }
}
