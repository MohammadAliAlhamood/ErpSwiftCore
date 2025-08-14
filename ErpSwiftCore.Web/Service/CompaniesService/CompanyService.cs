using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Service.CompaniesService
{ 
    public class CompanyService : ICompanyService
    {
        private readonly IBaseService _baseService;
        public CompanyService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> CreateCompanyAsync(CompanyCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company/create"
            });
        }
        public async Task<APIResponseDto?> BulkCreateCompaniesAsync(IEnumerable<CompanyCreateDto> dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company/bulk-create"
            });
        }
        public async Task<APIResponseDto?> UpdateCompanyAsync(CompanyUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company/update"
            });
        }
        public async Task<APIResponseDto?> DeleteCompanyAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/company/delete/{companyId}"
            });
        }
        public async Task<APIResponseDto?> DeleteAllCompaniesAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/company/delete-all"
            });
        }
        public async Task<APIResponseDto?> GetAllCompaniesAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company/all"
            }, true);
        }
        public async Task<APIResponseDto?> GetCompanyByIdAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company/{companyId}"
            });
        }
        public async Task<APIResponseDto?> GetCompaniesCountAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company/count"
            });
        }
    }
}
