using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Service.AuthsService
{
    public class RoleService : IRoleService
    {
        private readonly IBaseService _baseService;

        public RoleService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> AssignRoleAsync(AssignRoleRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/role/assign"
            });
        }
        public async Task<APIResponseDto?> AssignRoleByEmailAsync(AssignRoleByEmailRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/role/assign-by-email"
            });
        }
         
        public async Task<APIResponseDto?> UpdateRolePermissionsAsync(UpdateRolePermissionsRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/role/permissions/update"
            });
        }

        public async Task<APIResponseDto?> GetAllRolesAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/role/roles"
            });
        }
         
    }
}
