using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.PasswordSecurity;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IAuthsService
{
    public interface IPasswordSecurityService
    {
        Task<APIResponseDto?> ChangePasswordAsync(ChangePasswordRequestDto dto);
        Task<APIResponseDto?> ForgotPasswordAsync(ForgotPasswordRequestDto dto);
        Task<APIResponseDto?> ResetPasswordAsync(ResetPasswordRequestDto dto);
    }
}
