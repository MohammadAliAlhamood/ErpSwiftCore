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
    public class SessionService : ISessionService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<SessionService> _logger; 
        public SessionService(
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<SessionService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        } 
        public async Task<IEnumerable<Session>> GetActiveSessionsAsync(string userId)
        {
            try
            {
                return await _db.Sessions
                    .AsNoTracking()
                    .Where(s => s.UserId == userId && (s.ExpiresAt == null || s.ExpiresAt > DateTime.UtcNow))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetActiveSessionsAsync");
                throw;
            }
        }

        public async Task EndSessionAsync(string sessionId)
        {
            try
            {
                if (!Guid.TryParse(sessionId, out var guid))
                    throw new FormatException($"'{sessionId}' is not a valid GUID.");

                var session = await _db.Sessions.FirstOrDefaultAsync(s => s.ID == guid)
                               ?? throw new KeyNotFoundException($"Session '{sessionId}' not found.");

                _db.Sessions.Remove(session);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EndSessionAsync");
                throw;
            }
        }

        public async Task AddTrustedDeviceAsync(TrustedDevice device)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(device.UserId)
                           ?? throw new KeyNotFoundException($"User '{device.UserId}' not found.");

                device.AddedAt = DateTime.UtcNow;
                _db.TrustedDevices.Add(device);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddTrustedDeviceAsync");
                throw;
            }
        }

        public async Task RemoveTrustedDeviceAsync(string deviceId)
        {
            try
            {
                if (!Guid.TryParse(deviceId, out var guid))
                    throw new FormatException($"'{deviceId}' is not a valid GUID.");

                var device = await _db.TrustedDevices.FirstOrDefaultAsync(d => d.ID == guid)
                             ?? throw new KeyNotFoundException($"Device '{deviceId}' not found.");

                _db.TrustedDevices.Remove(device);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RemoveTrustedDeviceAsync");
                throw;
            }
        }

        public async Task<IEnumerable<TrustedDevice>> GetTrustedDevicesAsync(string userId)
        {
            try
            {
                return await _db.TrustedDevices
                    .AsNoTracking()
                    .Where(d => d.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTrustedDevicesAsync");
                throw;
            }
        }
         
    }
}
