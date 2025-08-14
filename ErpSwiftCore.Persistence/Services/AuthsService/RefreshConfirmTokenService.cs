using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Persistence.Services.AuthsService
{
    public class RefreshConfirmTokenService : IRefreshConfirmTokenService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly ILogger<RefreshConfirmTokenService> _logger;
        public RefreshConfirmTokenService(
            AppDbContext db, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            ILogger<RefreshConfirmTokenService> logger)
        {
            _db = db; 
            _userManager = userManager;
            _roleManager = roleManager; 
            _logger = logger;
        }
        public Task<string> RefreshTokenAsync(string expiredToken, string refreshToken)
            => throw new NotImplementedException();
        public Task RevokeRefreshTokenAsync(string refreshToken)
            => throw new NotImplementedException();
        public Task<ApplicationUser> GetUserByIdAsync(string userId)
            => throw new NotImplementedException();
        public Task<ApplicationUser> GetUserByEmailAsync(string email)
            => throw new NotImplementedException();
        public async Task<bool> UserExistsAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserExistsAsync");
                throw;
            }
        }
        public async Task UpdateUserAsync(ApplicationUser user)
        {
            try
            {
                var existing = await _userManager.FindByIdAsync(user.Id)
                               ?? throw new KeyNotFoundException($"User '{user.Id}' not found.");

                // copy updatable properties
                existing.Email = user.Email;
                existing.UserName = user.UserName;
                existing.NormalizedEmail = user.Email?.ToUpper();
                existing.NormalizedUserName = user.UserName?.ToUpper();
                existing.Name = user.Name;
                existing.PhoneNumber = user.PhoneNumber;
                existing.Address = user.Address;
                existing.ProfilePictureUrl = user.ProfilePictureUrl;
                existing.IsSubscribedToSecurityNotifications = user.IsSubscribedToSecurityNotifications;

                var result = await _userManager.UpdateAsync(existing);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateUserAsync");
                throw;
            }
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    throw new KeyNotFoundException($"Role '{roleName}' not found.");

                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
                return usersInRole;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUsersByRoleAsync");
                throw;
            }
        }
        public async Task ResendEmailConfirmationAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // TODO: send `token` via email (e.g. await _emailService.SendEmailConfirmationAsync(user.Email, token))
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ResendEmailConfirmationAsync");
                throw;
            }
        }
        public async Task<string> GeneratePhoneNumberConfirmationTokenAsync(string userId, string phoneNumber)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GeneratePhoneNumberConfirmationTokenAsync");
                throw;
            }
        }
        public async Task ConfirmPhoneNumberAsync(string userId, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber!, token);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ConfirmPhoneNumberAsync");
                throw;
            }
        }
        public async Task ClearExpiredSessionsAsync(string userId)
        {
            try
            {
                var now = DateTime.UtcNow;
                var expired = await _db.Sessions
                    .Where(s => s.UserId == userId && s.ExpiresAt != null && s.ExpiresAt <= now)
                    .ToListAsync();

                if (expired.Any())
                {
                    _db.Sessions.RemoveRange(expired);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ClearExpiredSessionsAsync");
                throw;
            }
        }
      
    }
}
