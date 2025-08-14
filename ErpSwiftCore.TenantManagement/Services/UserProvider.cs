using ErpSwiftCore.TenantManagement.Interfaces;

namespace ErpSwiftCore.TenantManagement.Services
{
    /// <summary>
    /// يقدّم معرّف المستخدم الحالي للمستدعين.
    /// </summary>
    public class UserProvider : IUserProvider
    {
        private readonly IUserContext _userContext;

        public UserProvider(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public Guid GetUserId() => _userContext.UserId;
    }
}