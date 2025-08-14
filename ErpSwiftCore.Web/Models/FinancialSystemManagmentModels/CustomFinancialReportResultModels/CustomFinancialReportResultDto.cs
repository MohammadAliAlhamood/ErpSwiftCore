using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos
{ 
    public class CustomFinancialReportResultDto : AuditableEntityDto
    {
        public string ReportName { get; set; } = null!;
        public List<Dictionary<string, object>>? Data { get; set; }
    } 
}