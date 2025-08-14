namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CustomFinancialReportResultModels
{
    /// <summary>
    /// بيانات التحقق من صلاحية التقرير
    /// </summary>
    public class ValidateReportDto
    {
        public Guid? Id { get; set; }
        public string ReportName { get; set; } = null!;
        public List<Dictionary<string, object>>? Data { get; set; }
    }

}
