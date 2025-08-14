namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{ 
    public class AddCostCentersRangeDto
    {
        public IEnumerable<CreateCostCenterDto> Centers { get; set; } = new List<CreateCostCenterDto>();
    }

}
