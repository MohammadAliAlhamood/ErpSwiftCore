using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{ 
    public class GetJournalEntryTotalByAccountQuery : IRequest<APIResponseDto>
    {
        public Guid AccountId { get; }
        public GetJournalEntryTotalByAccountQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
