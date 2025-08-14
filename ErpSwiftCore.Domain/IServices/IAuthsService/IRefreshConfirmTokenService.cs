using ErpSwiftCore.Domain.Entities.EntityAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface IRefreshConfirmTokenService
    {

        Task<string> RefreshTokenAsync(string expiredToken, string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string userId);
        Task UpdateUserAsync(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName);
        Task ResendEmailConfirmationAsync(string userId);
        Task<string> GeneratePhoneNumberConfirmationTokenAsync(string userId, string phoneNumber);
        Task ConfirmPhoneNumberAsync(string userId, string token);
        Task ClearExpiredSessionsAsync(string userId);



    }
}
