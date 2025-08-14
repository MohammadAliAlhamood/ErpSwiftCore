namespace ErpSwiftCore.TenantManagement.Interfaces
{
    /// <summary>
    /// Defines storage and retrieval of the current user’s ID.
    /// </summary>
    public interface IUserContext
    {
        Guid UserId { get; }

        void SetUser(Guid userId);
    }
}