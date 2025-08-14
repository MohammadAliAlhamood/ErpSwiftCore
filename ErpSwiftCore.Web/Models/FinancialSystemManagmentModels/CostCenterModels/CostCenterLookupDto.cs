namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{ 
    public class CostCenterLookupDto
    {
        public Guid Id { get; set; }
        public string CenterName { get; set; } = null!;
        public string? Code { get; set; }
    } 
}
