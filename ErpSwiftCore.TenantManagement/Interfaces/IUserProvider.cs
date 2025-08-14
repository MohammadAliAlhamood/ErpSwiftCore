namespace ErpSwiftCore.TenantManagement.Interfaces
{
    /// <summary>
    /// Provides the current user’s ID to application code.
    /// </summary>
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}