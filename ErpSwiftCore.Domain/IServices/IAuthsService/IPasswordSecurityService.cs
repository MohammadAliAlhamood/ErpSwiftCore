using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface IPasswordSecurityService
    {
        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task ConfirmEmailAsync(string userId, string token);
        Task ForgotPasswordAsync(string email);
        Task ResetPasswordAsync(string userId, string newPassword, string resetToken);

    }
}
