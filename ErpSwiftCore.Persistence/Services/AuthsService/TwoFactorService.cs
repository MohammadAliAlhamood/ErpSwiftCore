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
    public class TwoFactorService : ITwoFactorService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TwoFactorService> _logger;
        public TwoFactorService(
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<TwoFactorService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #region Two-Factor & Security

        public async Task EnableTwoFactorAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                user.TwoFactorEnabled = true;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EnableTwoFactorAsync");
                throw;
            }
        }

        public async Task DisableTwoFactorAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                user.TwoFactorEnabled = false;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DisableTwoFactorAsync");
                throw;
            }
        }

        public async Task<bool> VerifyTwoFactorAsync(string userId, string code)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null || !user.TwoFactorEnabled)
                    return false;

                return await _userManager.VerifyTwoFactorTokenAsync(
                    user,
                    TokenOptions.DefaultAuthenticatorProvider,
                    code
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyTwoFactorAsync");
                throw;
            }
        }

        public async Task<(string SharedKey, string QrCodeUri)> GenerateTwoFactorSetupAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
                if (string.IsNullOrEmpty(sharedKey))
                {
                    await _userManager.ResetAuthenticatorKeyAsync(user);
                    sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
                }

                var uri = $"otpauth://totp/ERP-App:{user.Email}?secret={sharedKey}&issuer=ERP-App";
                return (sharedKey, uri);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GenerateTwoFactorSetupAsync");
                throw;
            }
        }

        public async Task<IEnumerable<string>> GenerateRecoveryCodesAsync(string userId, int numberOfCodes = 10)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var codes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, numberOfCodes);
                return codes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GenerateRecoveryCodesAsync");
                throw;
            }
        }

        public async Task<bool> RedeemRecoveryCodeAsync(string userId, string recoveryCode)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var result = await _userManager.RedeemTwoFactorRecoveryCodeAsync(user, recoveryCode);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RedeemRecoveryCodeAsync");
                throw;
            }
        }

        public async Task SendSecurityAlertAsync(SecurityAlert alert)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(alert.UserId!)
                           ?? throw new KeyNotFoundException($"User '{alert.UserId}' not found.");

                alert.AlertTime = DateTime.UtcNow;
                _db.SecurityAlerts.Add(alert);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendSecurityAlertAsync");
                throw;
            }
        }

        public async Task SubscribeToSecurityNotificationsAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                user.IsSubscribedToSecurityNotifications = true;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubscribeToSecurityNotificationsAsync");
                throw;
            }
        }

        public async Task UnsubscribeFromSecurityNotificationsAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                user.IsSubscribedToSecurityNotifications = false;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UnsubscribeFromSecurityNotificationsAsync");
                throw;
            }
        }

        #endregion



    }
}
