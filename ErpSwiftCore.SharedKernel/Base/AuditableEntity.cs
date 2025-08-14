using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.SharedKernel.Interface;
namespace ErpSwiftCore.SharedKernel.Base
{
    public class AuditableEntity : BaseEntity, ITenantEntity
    {
        public Guid TenantID { get; set; }
        public Company? Company { get; set; }
    }
}