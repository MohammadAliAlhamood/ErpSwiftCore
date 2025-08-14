using ErpSwiftCore.TenantManagement.Interfaces;

namespace ErpSwiftCore.TenantManagement.Services
{
    /// <summary>
    /// يقدّم معرّف التينانت الحالي للمستدعين.
    /// </summary>
    public class TenantProvider : ITenantProvider
    {
        private readonly ITenantContext _tenantContext;

        public TenantProvider(ITenantContext tenantContext)
        {
            _tenantContext = tenantContext;
        }

        public Guid GetTenantId() => _tenantContext.TenantId;
    }
}