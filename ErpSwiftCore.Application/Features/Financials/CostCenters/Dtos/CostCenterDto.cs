using ErpSwiftCore.Application.Dtos;
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos
{ 
    public class CostCenterDto : AuditableEntityDto
    {
        public string CenterName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Code { get; set; } 
    }  
}