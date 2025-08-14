using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{ 
    public class GetJournalEntryLineCreditByAccountQuery : IRequest<APIResponseDto>
    {
        public Guid AccountId { get; }

        public GetJournalEntryLineCreditByAccountQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

}
