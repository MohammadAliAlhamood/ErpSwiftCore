using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.PasswordSecurity;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IAuthsService;
namespace ErpSwiftCore.Web.Service.AuthsService
{
    public class PasswordSecurityService : IPasswordSecurityService
    {
        private readonly IBaseService _baseService;
        public PasswordSecurityService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> ChangePasswordAsync(ChangePasswordRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/password-security/password/change"
            });
        }
        public async Task<APIResponseDto?> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/password-security/password/forgot"
            });
        }
        public async Task<APIResponseDto?> ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/password-security/password/reset"
            });
        }
    }
}
