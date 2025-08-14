using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IAuthsService
{
    public interface IRoleService
    {
        Task<APIResponseDto?> AssignRoleByEmailAsync(AssignRoleByEmailRequestDto dto);
        Task<APIResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto);
        Task<APIResponseDto?> UpdateRolePermissionsAsync(UpdateRolePermissionsRequestDto dto);
        Task<APIResponseDto?> GetAllRolesAsync();
    }
}
