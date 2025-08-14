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
    // 5. Sum Debit by Account
    public class GetJournalEntryLineDebitByAccountQueryHandler
        : BaseHandler<GetJournalEntryLineDebitByAccountQuery>
    {
        private readonly IJournalEntryQueryService _svc;

        public GetJournalEntryLineDebitByAccountQueryHandler(
            IJournalEntryQueryService svc,
            ILogger<BaseHandler<GetJournalEntryLineDebitByAccountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override Task<object?> HandleRequestAsync(
            GetJournalEntryLineDebitByAccountQuery req,
            CancellationToken ct)
        {
            // returns decimal
            return _svc.GetJournalEntryLineDebitByAccountAsync(req.AccountId, ct)
                       .ContinueWith(t => (object?)t.Result, ct);
        }
    }



}
