using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityFinancial
{
    public class CostCenter : AuditableEntity
    {
        public string? CenterName { get; set; }
        public string? Description { get; set; } 
        public string? Code { get; set; }
    }
}