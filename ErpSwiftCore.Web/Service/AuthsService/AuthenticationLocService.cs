using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Authentication;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.AuthsService
{
    public class AuthenticationLocService : IAuthenticationLocService
    {
        private readonly IBaseService _baseService;

        public AuthenticationLocService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<APIResponseDto?> RegisterAsync(RegisterRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/authentication/register"
            }, withBearer: false);
        }

        public async Task<APIResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/authentication/login"
            }, withBearer: false);
        }

        public async Task<APIResponseDto?> LogoutAsync(LogoutRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/authentication/logout"
            }, withBearer: true);
        }

        public async Task<APIResponseDto?> LogoutAllSessionsAsync(LogoutRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/authentication/logout-all-sessions"
            }, withBearer: true);
        }
    }
}
