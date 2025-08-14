using ErpSwiftCore.TenantManagement.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ErpSwiftCore.TenantManagement.Behaviors
{
    /// <summary>
    /// ينقل معرّف التينانت من ClaimsPrincipal إلى ITenantContext قبل معالجة الطلب (MediatR).
    /// </summary>
    public class TenantContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantContext _tenantContext;

        public TenantContextBehavior(
            IHttpContextAccessor httpContextAccessor,
            ITenantContext tenantContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantContext = tenantContext;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                var tenantIdStr = user.FindFirst("TenantId")?.Value;
                if (Guid.TryParse(tenantIdStr, out var tenantId))
                {
                    _tenantContext.SetTenant(tenantId);
                }
            }

            return await next();
        }
    }
}