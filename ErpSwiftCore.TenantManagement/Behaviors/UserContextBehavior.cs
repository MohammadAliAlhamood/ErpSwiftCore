using ErpSwiftCore.TenantManagement.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ErpSwiftCore.TenantManagement.Behaviors
{
    /// <summary>
    /// ينقل معرّف المستخدم من ClaimsPrincipal إلى IUserContext قبل معالجة الطلب (MediatR).
    /// </summary>
    public class UserContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;

        public UserContextBehavior(
            IHttpContextAccessor httpContextAccessor,
            IUserContext userContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(userIdStr, out var userId))
                {
                    _userContext.SetUser(userId);
                }
            }

            return await next();
        }
    }
}