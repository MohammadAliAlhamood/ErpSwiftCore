namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{
    /// <summary>
    /// بيانات حذف مجموعة مراكز تكلفة
    /// </summary>
    public class BatchDeleteCostCentersDto
    {
        public IEnumerable<Guid> CenterIds { get; set; } = new List<Guid>();
    }
}
