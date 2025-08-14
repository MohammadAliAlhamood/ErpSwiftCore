namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{
    /// <summary>
    /// نتيجة عد مراكز التكلفة حسب أب
    /// </summary>
    public class CostCentersCountByParentDto
    {
        public Guid? ParentId { get; set; }
        public int Count { get; set; }
    }

}
