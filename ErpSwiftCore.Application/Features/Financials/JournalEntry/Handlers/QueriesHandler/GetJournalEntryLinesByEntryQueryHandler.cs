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
    public class GetJournalEntryLinesByEntryQueryHandler
        : BaseHandler<GetJournalEntryLinesByEntryQuery>
    {
        private readonly IJournalEntryQueryService _svc;
        private readonly IMapper _mapper;

        public GetJournalEntryLinesByEntryQueryHandler(
            IJournalEntryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetJournalEntryLinesByEntryQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetJournalEntryLinesByEntryQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetJournalEntryLinesByEntryAsync(req.JournalEntryId, ct);
            return list.Select(e => _mapper.Map<JournalEntryLineDto>(e));
        }
    }
}
