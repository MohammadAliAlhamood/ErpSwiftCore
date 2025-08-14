namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{

    public class CreateCostCenterDto
    {
        public string CenterName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Code { get; set; }
        public Guid? ParentId { get; set; }
    }

}
