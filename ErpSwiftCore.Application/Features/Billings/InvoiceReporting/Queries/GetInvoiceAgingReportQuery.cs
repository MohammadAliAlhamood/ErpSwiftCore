using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>6. Get invoice aging report as of a date</summary>
    public class GetInvoiceAgingReportQuery : IRequest<APIResponseDto>
    {
        public DateTime AsOfDate { get; }
        public GetInvoiceAgingReportQuery(DateTime asOfDate) => AsOfDate = asOfDate;
    } 
}
