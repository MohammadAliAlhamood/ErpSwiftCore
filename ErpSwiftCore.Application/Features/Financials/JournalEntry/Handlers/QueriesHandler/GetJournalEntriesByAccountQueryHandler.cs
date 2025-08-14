using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos;
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
    public class GetJournalEntriesByAccountQueryHandler
        : BaseHandler<GetJournalEntriesByAccountQuery>
    {
        private readonly IJournalEntryQueryService _svc;
        private readonly IMapper _mapper;
        public GetJournalEntriesByAccountQueryHandler(IJournalEntryQueryService svc, IMapper mapper, ILogger<BaseHandler<GetJournalEntriesByAccountQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetJournalEntriesByAccountQuery req, CancellationToken ct)
        {
            var list = await _svc.GetJournalEntriesByAccountAsync(req.AccountId, ct);
            return list.Select(e => _mapper.Map<JournalEntryDto>(e));
        }
    }

}
