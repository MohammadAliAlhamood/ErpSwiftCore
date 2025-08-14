using ErpSwiftCore.Domain.Entities.EntityAuth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface ITwoFactorService
    {
        Task EnableTwoFactorAsync(string userId);
        Task DisableTwoFactorAsync(string userId);
        Task<bool> VerifyTwoFactorAsync(string userId, string code);
        Task<(string SharedKey, string QrCodeUri)> GenerateTwoFactorSetupAsync(string userId);
        Task<IEnumerable<string>> GenerateRecoveryCodesAsync(string userId, int numberOfCodes = 10);
        Task<bool> RedeemRecoveryCodeAsync(string userId, string recoveryCode);
        Task SendSecurityAlertAsync(SecurityAlert alert);
        Task SubscribeToSecurityNotificationsAsync(string userId);
        Task UnsubscribeFromSecurityNotificationsAsync(string userId);
    }
}
