namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{
    public class BulkImportCostCentersDto
    {
        public IEnumerable<CreateCostCenterDto> Centers { get; set; } = new List<CreateCostCenterDto>();
    }
}
