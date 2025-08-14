namespace ErpSwiftCore.TenantManagement.Interfaces
{
    /// <summary>
    /// Defines storage and retrieval of the current tenant’s ID.
    /// </summary>
    public interface ITenantContext
    {
        Guid TenantId { get; }

        void SetTenant(Guid tenantId);
    }
}