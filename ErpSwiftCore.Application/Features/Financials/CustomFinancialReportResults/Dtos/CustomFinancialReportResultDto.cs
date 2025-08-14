using ErpSwiftCore.Application.Dtos;
namespace ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos
{
    public class CustomFinancialReportResultDto : AuditableEntityDto
    {
        public string ReportName { get; set; } = null!;
        public List<Dictionary<string, object>>? Data { get; set; }
    }
    public class SaveReportDto
    {
        public Guid? Id { get; set; }
        public string ReportName { get; set; } = null!;
        public List<Dictionary<string, object>>? Data { get; set; }
    }
    public class SaveReportsRangeDto
    {
        public IEnumerable<SaveReportDto> Reports { get; set; } = new List<SaveReportDto>();
    }

    public class CompanyReportCountDto
    {
        public Guid CompanyId { get; set; }
        public int Count { get; set; }
    }
    public class ReportsCountByCompanyDto
    {
        public IEnumerable<CompanyReportCountDto> Counts { get; set; } = new List<CompanyReportCountDto>();
    }
    public class ValidateReportDto
    {
        public Guid? Id { get; set; }
        public string ReportName { get; set; } = null!;
        public List<Dictionary<string, object>>? Data { get; set; }
    }


















     







    
  





}