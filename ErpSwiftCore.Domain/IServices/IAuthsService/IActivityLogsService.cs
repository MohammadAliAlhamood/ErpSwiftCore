using ErpSwiftCore.Domain.Entities.EntityAuth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface IActivityLogsService
    {
        Task<IEnumerable<ActivityLog>> GetUserActivityLogsAsync(string userId, DateTime startDate, DateTime endDate);
        Task<(int TotalUsers, int ActiveUsers, int BlockedUsers, int TotalRoles)> GetSystemUsageStatisticsAsync();
    }
}
