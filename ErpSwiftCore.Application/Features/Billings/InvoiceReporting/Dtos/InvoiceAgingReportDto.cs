 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos
{
    /// <summary>
    /// تقرير أعمار الفواتير
    /// </summary>
    public class InvoiceAgingReportDto
    {
        public decimal Current { get; set; }
        public decimal Days30 { get; set; }
        public decimal Days60 { get; set; }
        public decimal Days90 { get; set; }
        public decimal Over90 { get; set; }
    }
}
