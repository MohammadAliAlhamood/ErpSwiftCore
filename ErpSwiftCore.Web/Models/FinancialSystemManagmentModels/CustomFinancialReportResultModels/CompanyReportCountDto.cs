namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CustomFinancialReportResultModels
{
    /// <summary>
    /// عنصر إحصائية: عدد التقارير لكل شركة
    /// </summary>
    public class CompanyReportCountDto
    {
        public Guid CompanyId { get; set; }
        public int Count { get; set; }
    }

}
