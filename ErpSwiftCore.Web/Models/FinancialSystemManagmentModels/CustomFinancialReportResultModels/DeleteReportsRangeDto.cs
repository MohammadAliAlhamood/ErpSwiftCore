namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CustomFinancialReportResultModels
{
    /// <summary>
    /// بيانات حذف مجموعة تقارير
    /// </summary>
    public class DeleteReportsRangeDto
    {
        public IEnumerable<Guid> ReportIds { get; set; }
            = new List<Guid>();
    }


}
