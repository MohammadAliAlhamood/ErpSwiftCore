using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{ 
    public class GetJournalEntriesByAccountQuery : IRequest<APIResponseDto>
    {
        public Guid AccountId { get; }

        public GetJournalEntriesByAccountQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

}
