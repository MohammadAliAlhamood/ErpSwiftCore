using ErpSwiftCore.TenantManagement.Interfaces;

namespace ErpSwiftCore.TenantManagement.Services
{
    /// <summary>
    /// يحتفظ بمعرّف المستخدم الحالي فقط.
    /// </summary>
    public class UserContext : IUserContext
    {
        public Guid UserId { get; private set; }

        public void SetUser(Guid userId)
        {
            UserId = userId;
        }
    }
}