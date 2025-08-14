using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos;

namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CustomFinancialReportResultModels
{
    /// <summary>
    /// تجميع إحصائية: عدد التقارير حسب شركة
    /// </summary>
    public class ReportsCountByCompanyDto
    {
        public IEnumerable<CompanyReportCountDto> Counts { get; set; }
            = new List<CompanyReportCountDto>();
    }

}
