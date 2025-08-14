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
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RoleService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }

       
        public async Task AssignRoleAsync(string userId, string roleName)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var roleCreate = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!roleCreate.Succeeded)
                        throw new InvalidOperationException(roleCreate.Errors.First().Description);
                }

                var addToRole = await _userManager.AddToRoleAsync(user, roleName);
                if (!addToRole.Succeeded)
                    throw new InvalidOperationException(addToRole.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AssignRoleAsync");
                throw;
            }
        }

        public async Task AssignRoleByEmailAsync(string email, string roleName)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email)
                           ?? throw new KeyNotFoundException($"User with email '{email}' not found.");

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var roleCreate = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!roleCreate.Succeeded)
                        throw new InvalidOperationException(roleCreate.Errors.First().Description);
                }

                var addToRole = await _userManager.AddToRoleAsync(user, roleName);
                if (!addToRole.Succeeded)
                    throw new InvalidOperationException(addToRole.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AssignRoleByEmailAsync");
                throw;
            }
        }

        public async Task CreateRoleAsync(string roleName)
        {
            try
            {
                if (await _roleManager.RoleExistsAsync(roleName))
                    throw new InvalidOperationException($"Role '{roleName}' already exists.");

                var createResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!createResult.Succeeded)
                    throw new InvalidOperationException(createResult.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateRoleAsync");
                throw;
            }
        }

        public async Task DeleteRoleAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName)
                           ?? throw new KeyNotFoundException($"Role '{roleName}' not found.");

                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                    throw new InvalidOperationException(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteRoleAsync");
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            try
            {
                return await _roleManager.Roles
                    .AsNoTracking()
                    .Select(r => r.Name!)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllRolesAsync");
                throw;
            }
        }
         
        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId)
                           ?? throw new KeyNotFoundException($"User '{userId}' not found.");

                var roles = await _userManager.GetRolesAsync(user);
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUserRolesAsync");
                throw;
            }
        }
    }
}
