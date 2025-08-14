using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Authentication;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.IAuthsService
{
    public interface IAuthenticationLocService
    {
        Task<APIResponseDto?> RegisterAsync(RegisterRequestDto dto);
        Task<APIResponseDto?> LoginAsync(LoginRequestDto dto);
        Task<APIResponseDto?> LogoutAsync(LogoutRequestDto dto);
        Task<APIResponseDto?> LogoutAllSessionsAsync(LogoutRequestDto dto);
    }
}