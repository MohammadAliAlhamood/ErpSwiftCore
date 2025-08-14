using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Handlers.QueriesHandler
{
    public class GetJournalEntryByIdQueryHandler : BaseHandler<GetJournalEntryByIdQuery>
    {
        private readonly IMapper _mapper;
        private readonly IJournalEntryQueryService _svc;
        public GetJournalEntryByIdQueryHandler(IJournalEntryQueryService svc, IMapper mapper, ILogger<BaseHandler<GetJournalEntryByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetJournalEntryByIdQuery req, CancellationToken ct)
        {
            var entity = await _svc.GetJournalEntryByIdAsync(req.EntryId, req.IncludeLines, ct);
            return _mapper.Map<JournalEntryDto?>(entity);
        }
    }
}
