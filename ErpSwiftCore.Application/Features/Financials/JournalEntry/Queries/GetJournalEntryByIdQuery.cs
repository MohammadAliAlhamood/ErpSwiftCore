using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{ 
    public class GetJournalEntryByIdQuery : IRequest<APIResponseDto>
    {
        public Guid EntryId { get; }
        public bool IncludeLines { get; }

        public GetJournalEntryByIdQuery(Guid entryId, bool includeLines = false)
        {
            EntryId = entryId;
            IncludeLines = includeLines;
        }
    } 
}