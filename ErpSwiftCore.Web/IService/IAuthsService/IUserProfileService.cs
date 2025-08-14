using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserProfile;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserManagement;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IAuthsService
{
    public interface IUserProfileService
    {
        Task<APIResponseDto?> GetUserProfileAsync(string userId);

        Task<APIResponseDto?> GetAllUsersAsync();

        Task<APIResponseDto?> UpdateProfileAsync(UpdateProfileRequestDto dto);

        Task<APIResponseDto?> UpdateMyProfileAsync(UpdateMyProfileRequestDto dto);

        Task<APIResponseDto?> BlockUserAsync(BlockUserRequestDto dto);

        Task<APIResponseDto?> DeleteUserAsync(DeleteUserRequestDto dto);

        Task<APIResponseDto?> UnblockUserAsync(BlockUserRequestDto dto);
        Task<APIResponseDto?> GetMyProfileAsync();
    }
}
