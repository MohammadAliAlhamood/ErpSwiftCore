using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>7. Get tax & discount summary per invoice</summary>
    public class GetTaxDiscountSummaryQuery : IRequest<APIResponseDto>
    {
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public GetTaxDiscountSummaryQuery(DateTime? from, DateTime? to)
        {
            FromDate = from;
            ToDate = to;
        }
    }


}
