using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels; 
namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{ 
    public class CostCenterDto : AuditableEntityDto
    {
        public string CenterName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Code { get; set; } 

    }
}