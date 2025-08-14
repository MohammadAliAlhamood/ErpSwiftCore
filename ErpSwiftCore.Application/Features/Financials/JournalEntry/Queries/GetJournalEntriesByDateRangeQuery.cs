using MediatR; 

namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries
{
    /// <summary>
    /// 3. استرجاع كل قيود اليومية ضمن نطاق تاريخ
    /// </summary>
    public class GetJournalEntriesByDateRangeQuery : IRequest<APIResponseDto>
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public GetJournalEntriesByDateRangeQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }



}
