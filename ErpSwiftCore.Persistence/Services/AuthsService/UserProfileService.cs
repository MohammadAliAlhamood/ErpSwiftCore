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
    public class UserProfileService : IUserProfileService

    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<UserProfileService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            try
            {
                return await _userManager.Users.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllUsersAsync");
                throw;
            }
        }

        public async Task<ApplicationUser?> GetUserProfileAsync(string userId)
        {
            try
            {
                return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUserProfileAsync");
                throw;
            }
        }
        public async Task UpdateProfileAsync(ApplicationUser updatedUser)
        {
            try
            {
                var existing = await _userManager.FindByIdAsync(updatedUser.Id) ?? throw new KeyNotFoundException($"User '{updatedUser.Id}' not found.");

                existing.Name = updatedUser.Name;
                existing.PhoneNumber = updatedUser.PhoneNumber;
                existing.ProfilePictureUrl = updatedUser.ProfilePictureUrl;
                existing.Address = updatedUser.Address;

                var result = await _userManager.UpdateAsync(existing);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProfileAsync");
                throw;
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)   ?? throw new KeyNotFoundException($"User '{userId}' not found.");
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)  throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteUserAsync");
                throw;
            }
        }

        public async Task BlockUserAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.MaxValue;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BlockUserAsync");
                throw;
            }
        }

        public async Task UnblockUserAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                user.LockoutEnabled = false;
                user.LockoutEnd = null;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UnblockUserAsync");
                throw;
            }
        }

    }
}
