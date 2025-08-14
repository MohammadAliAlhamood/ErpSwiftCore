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
    public class SocialSSOService : ISocialSSOService
    {

        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<SocialSSOService> _logger;
        public SocialSSOService(
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<SocialSSOService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        #region SSO & Social

        public async Task<string> GenerateSSOUrlAsync(string provider, string callbackUrl)
        {
            try
            {
                var url = $"{provider}/oauth/authorize?callback={Uri.EscapeDataString(callbackUrl)}";
                return url;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GenerateSSOUrlAsync");
                throw;
            }
        }

        public async Task<(ApplicationUser User, string Token)?> HandleSSOCallbackAsync(string code, string state)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == state);
                if (user == null) return null;

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);
                return (user, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HandleSSOCallbackAsync");
                throw;
            }
        }

        public async Task LinkSocialAccountAsync(SocialAccount socialAccount)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(socialAccount.UserId)
                           ?? throw new KeyNotFoundException($"User '{socialAccount.UserId}' not found.");

                var loginInfo = new UserLoginInfo(
                    socialAccount.Provider,
                    socialAccount.ProviderUserId,
                    socialAccount.Provider);

                var result = await _userManager.AddLoginAsync(user, loginInfo);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LinkSocialAccountAsync");
                throw;
            }
        }

        public async Task UnlinkSocialAccountAsync(string userId, string provider)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var logins = await _userManager.GetLoginsAsync(user);
                var login = logins.FirstOrDefault(l => l.LoginProvider == provider)
                            ?? throw new InvalidOperationException($"Provider '{provider}' not linked.");

                var result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UnlinkSocialAccountAsync");
                throw;
            }
        }

        #endregion


    }
}
