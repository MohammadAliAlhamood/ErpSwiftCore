using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Persistence.Services.AuthsService
{
    public class AuthenticationLocService : IAuthenticationLocService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthenticationLocService> _logger;
        public AuthenticationLocService(
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthenticationLocService> logger)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public async Task<string> RegisterAsync(ApplicationUser user, string password, string? roleName = null)
        {
            try
            {
                user.UserName = user.Email;
                user.NormalizedEmail = user.Email?.ToUpper();
                user.NormalizedUserName = user.Email?.ToUpper();

                var createResult = await _userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                    throw new InvalidOperationException(createResult.Errors.First().Description);

                if (!string.IsNullOrWhiteSpace(roleName))
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        var roleCreate = await _roleManager.CreateAsync(new IdentityRole(roleName));
                        if (!roleCreate.Succeeded)
                            throw new InvalidOperationException(roleCreate.Errors.First().Description);
                    }

                    var roleAssign = await _userManager.AddToRoleAsync(user, roleName);
                    if (!roleAssign.Succeeded)
                        throw new InvalidOperationException(roleAssign.Errors.First().Description);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegisterAsync");
                throw;
            }
        }
        public async Task<(ApplicationUser User, string Token)?> LoginAsync(string userName, string password)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName!.ToLower() == userName.ToLower());
                if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                    return null;
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);
                return (user, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LoginAsync");
                throw;
            }
        }
        public async Task LogOutAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId) ?? throw new KeyNotFoundException($"User '{userId}' not found.");
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LogOutAsync");
                throw;
            }
        }
        public async Task LogOutFromAllSessionsAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId) ?? throw new KeyNotFoundException($"User '{userId}' not found.");
                await _userManager.UpdateSecurityStampAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LogOutFromAllSessionsAsync");
                throw;
            }
        }

    }
}
