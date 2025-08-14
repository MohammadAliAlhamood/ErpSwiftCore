using MediatR; 
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>5. Get invoice total summary grouped by date</summary>
    public class GetInvoiceTotalSummaryQuery : IRequest<APIResponseDto>
    {
        public DateGrouping Grouping { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public GetInvoiceTotalSummaryQuery(DateGrouping grouping, DateTime? from, DateTime? to)
        {
            Grouping = grouping;
            FromDate = from;
            ToDate = to;
        }
    }

}
