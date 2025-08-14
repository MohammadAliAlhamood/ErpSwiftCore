namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{
    public class BatchRestoreCostCentersDto
    {
        public IEnumerable<Guid> CenterIds { get; set; } = new List<Guid>();
    }

}
