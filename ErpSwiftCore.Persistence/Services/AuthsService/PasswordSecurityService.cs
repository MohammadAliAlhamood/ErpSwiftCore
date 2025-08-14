using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.AuthsService
{
    public class PasswordSecurityService : IPasswordSecurityService
    {

        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<PasswordSecurityService> _logger;
        public PasswordSecurityService(
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<PasswordSecurityService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        #region Password & Recovery

        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ChangePasswordAsync");
                throw;
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                return await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GenerateEmailConfirmationTokenAsync");
                throw;
            }
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ConfirmEmailAsync");
                throw;
            }
        }

        public async Task ForgotPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email)
                           ?? throw new KeyNotFoundException($"User with email '{email}' not found.");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                // TODO: send `token` via email or other channel
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ForgotPasswordAsync");
                throw;
            }
        }

        public async Task ResetPasswordAsync(string userId, string newPassword, string resetToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ResetPasswordAsync");
                throw;
            }
        }

        #endregion
    }
}
