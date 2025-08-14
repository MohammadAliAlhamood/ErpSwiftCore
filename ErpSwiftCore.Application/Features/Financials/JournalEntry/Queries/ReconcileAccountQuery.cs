using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{ 
    public class ReconcileAccountQuery : IRequest<APIResponseDto>
    {
        public Guid AccountId { get; }
        public DateTime From { get; }
        public DateTime To { get; }

        public ReconcileAccountQuery(Guid accountId, DateTime from, DateTime to)
        {
            AccountId = accountId;
            From = from;
            To = to;
        }
    }
}
