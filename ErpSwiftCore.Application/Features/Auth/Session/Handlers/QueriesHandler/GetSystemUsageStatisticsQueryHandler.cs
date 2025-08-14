using ErpSwiftCore.Application.Features.Auth.Dtos; 
using ErpSwiftCore.Application.Features.Auth.Role.Queries;
using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Session.Handlers.QueriesHandler
{
    public class GetSystemUsageStatisticsQueryHandler : BaseHandler<GetSystemUsageStatisticsQuery>
    {
        private readonly IActivityLogsService _authService;

        public GetSystemUsageStatisticsQueryHandler(
            IActivityLogsService authService,
            ILogger<GetSystemUsageStatisticsQueryHandler> logger
        ) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(GetSystemUsageStatisticsQuery request, CancellationToken cancellationToken)
        {
            var stats = await _authService.GetSystemUsageStatisticsAsync();
            return new SystemUsageStatisticsDto
            {
                TotalUsers = stats.TotalUsers,
                ActiveUsers = stats.ActiveUsers,
                BlockedUsers = stats.BlockedUsers,
                TotalRoles = stats.TotalRoles
            };
        }
    }
}
