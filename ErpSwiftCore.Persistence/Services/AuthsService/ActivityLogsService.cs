using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.AuthsService
{
    public class ActivityLogsService : IActivityLogsService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ActivityLogsService> _logger;

        public ActivityLogsService(
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ActivityLogsService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        } 
        public async Task<IEnumerable<ActivityLog>> GetUserActivityLogsAsync(string userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _db.ActivityLogs
                    .AsNoTracking()
                    .Where(log => log.UserId == userId && log.Timestamp >= startDate && log.Timestamp <= endDate)
                    .OrderByDescending(log => log.Timestamp)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUserActivityLogsAsync");
                throw;
            }
        }

        public async Task<(int TotalUsers, int ActiveUsers, int BlockedUsers, int TotalRoles)> GetSystemUsageStatisticsAsync()
        {
            try
            {
                var totalUsers = await _db.Users.CountAsync();
                var activeUsers = await _db.Users.CountAsync(u =>
                    !u.LockoutEnabled || (u.LockoutEnd != null && u.LockoutEnd <= DateTimeOffset.UtcNow));
                var blockedUsers = await _db.Users.CountAsync(u =>
                    u.LockoutEnabled && (u.LockoutEnd == null || u.LockoutEnd > DateTimeOffset.UtcNow));
                var totalRoles = await _roleManager.Roles.CountAsync();

                return (totalUsers, activeUsers, blockedUsers, totalRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSystemUsageStatisticsAsync");
                throw;
            }
        }
         
    }
}
