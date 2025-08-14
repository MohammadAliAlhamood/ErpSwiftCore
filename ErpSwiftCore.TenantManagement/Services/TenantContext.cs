using ErpSwiftCore.TenantManagement.Interfaces;

namespace ErpSwiftCore.TenantManagement.Services
{
    /// <summary>
    /// يحتفظ بمعرّف التينانت الحالي فقط.
    /// </summary>
    public class TenantContext : ITenantContext
    {
        public Guid TenantId { get; private set; }

        public void SetTenant(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}