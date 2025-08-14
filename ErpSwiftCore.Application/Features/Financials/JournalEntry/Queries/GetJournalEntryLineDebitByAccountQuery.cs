using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{ 
    public class GetJournalEntryLineDebitByAccountQuery : IRequest<APIResponseDto>
    {
        public Guid AccountId { get; }

        public GetJournalEntryLineDebitByAccountQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }
    }



