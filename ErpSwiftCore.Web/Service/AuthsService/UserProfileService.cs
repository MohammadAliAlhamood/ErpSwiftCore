using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserManagement;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserProfile;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Service.AuthsService
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IBaseService _baseService;

        public UserProfileService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<APIResponseDto?> GetUserProfileAsync(string userId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/user-profile/{userId}"
            });
        }

        public async Task<APIResponseDto?> GetAllUsersAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/user-profile/users"
            });
        }

        public async Task<APIResponseDto?> UpdateProfileAsync(UpdateProfileRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/user-profile/update"
            });
        }

        public async Task<APIResponseDto?> UpdateMyProfileAsync(UpdateMyProfileRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/user-profile/update-my-profile"
            });
        }

        public async Task<APIResponseDto?> BlockUserAsync(BlockUserRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/user-profile/block"
            });
        }

        public async Task<APIResponseDto?> DeleteUserAsync(DeleteUserRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/user-profile/delete"
            });
        }

        public async Task<APIResponseDto?> UnblockUserAsync(BlockUserRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/user-profile/unblock"
            });
        }
        public async Task<APIResponseDto?> GetMyProfileAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/user-profile/my-profile"
            });
        } 
    }
}
