using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos
{ 
    public class InvoiceReportFilterDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public InvoiceStatus? Status { get; set; }
    }
}
