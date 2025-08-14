namespace ErpSwiftCore.TenantManagement.Interfaces
{
    /// <summary>
    /// Provides the current tenant’s ID to application code.
    /// </summary>
    public interface ITenantProvider
    {
        Guid GetTenantId();
    }
}