using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompanyBranchs;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
namespace ErpSwiftCore.Web.Service.CompaniesService
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyBranchService : ICompanyBranchService
    {
        private readonly IBaseService _baseService;
        public CompanyBranchService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> CreateBranchAsync(CompanyBranchCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-branch/create"
            });
        }
        public async Task<APIResponseDto?> UpdateBranchAsync(CompanyBranchUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-branch/update"
            });
        }
        public async Task<APIResponseDto?> DeleteBranchAsync(Guid branchId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/company-branch/delete/{branchId}"
            });
        }
        public async Task<APIResponseDto?> DeleteAllBranchesAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/company-branch/delete-all/{companyId}"
            });
        }
        public async Task<APIResponseDto?> GetAllBranchesAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company-branch/all"
            });
        }
        public async Task<APIResponseDto?> GetBranchByIdAsync(Guid branchId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/{branchId}"
            });
        }
        public async Task<APIResponseDto?> GetBranchWithCompanyAsync(Guid branchId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/with-company/{branchId}"
            });
        }
        public async Task<APIResponseDto?> GetBranchesByCompanyIdAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/by-company/{companyId}"
            });
        }
        public async Task<APIResponseDto?> BranchExistsAsync(Guid branchId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/exists/{branchId}"
            });
        }
        public async Task<APIResponseDto?> BranchExistsByCodeAsync(Guid companyId, string branchCode)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/exists/code/{companyId}/{branchCode}"
            });
        }
        public async Task<APIResponseDto?> IsBranchNameUniqueAsync(Guid companyId, string branchName, Guid? excludeBranchId)
        {
            var queryString = excludeBranchId.HasValue
                ? $"?excludeBranchId={excludeBranchId.Value}"
                : string.Empty;

            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/exists/name-unique/{companyId}/{branchName}{queryString}"
            });
        }
        public async Task<APIResponseDto?> HasBranchesAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/has/company/{companyId}"
            });
        }
        public async Task<APIResponseDto?> GetBranchesCountAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-branch/count/company/{companyId}"
            });
        }
     }
}
