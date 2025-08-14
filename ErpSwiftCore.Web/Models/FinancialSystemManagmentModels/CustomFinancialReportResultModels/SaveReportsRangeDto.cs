namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CustomFinancialReportResultModels
{
    /// <summary>
    /// بيانات إنشاء أو تحديث تقرير مالي
    /// </summary>
    public class SaveReportDto
    {
        public Guid? Id { get; set; }
        public string ReportName { get; set; } = null!;
        public List<Dictionary<string, object>>? Data { get; set; }
    }


}
