using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{ 
    public class GetJournalEntryLinesByEntryQuery : IRequest<APIResponseDto>
    {
        public Guid JournalEntryId { get; }

        public GetJournalEntryLinesByEntryQuery(Guid journalEntryId)
        {
            JournalEntryId = journalEntryId;
        }
    }
}
