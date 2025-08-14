using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompaniesSettings;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
namespace ErpSwiftCore.Web.Service.CompaniesService
{
    public class CompanySettingService : ICompanySettingService
    {
        private readonly IBaseService _baseService;

        public CompanySettingService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        #region ──────────── Command Methods ────────────

        public async Task<APIResponseDto?> CreateCompanySettingsAsync(CompanySettingsCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-setting/create"
            });
        }

        public async Task<APIResponseDto?> BulkCreateCompanySettingsAsync(CompanySettingsBulkCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-setting/bulk-create"
            });
        }

        public async Task<APIResponseDto?> UpdateCompanySettingsAsync(CompanySettingsUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-setting/update"
            });
        }

        public async Task<APIResponseDto?> UpdateCompanySettingsCurrencyAsync(CompanySettingsCurrencyUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-setting/update-currency"
            });
        }

        public async Task<APIResponseDto?> UpdateCompanySettingsTimeZoneAsync(CompanySettingsTimeZoneUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-setting/update-timezone"
            });
        }

        public async Task<APIResponseDto?> DeleteCompanySettingsAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/company-setting/delete/{companyId}"
            });
        }

        public async Task<APIResponseDto?> BulkDeleteCompanySettingsAsync(CompanySettingsBulkDeleteDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-setting/bulk-delete"
            });
        }

        public async Task<APIResponseDto?> DeleteAllCompanySettingsAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/company-setting/delete-all"
            });
        }

        public async Task<APIResponseDto?> RestoreCompanySettingsAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ErpAPIBase + $"/api/company-setting/restore/{companyId}"
            });
        }

        public async Task<APIResponseDto?> BulkRestoreCompanySettingsAsync(CompanySettingsBulkRestoreDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/company-setting/bulk-restore"
            });
        }

        public async Task<APIResponseDto?> RestoreAllCompanySettingsAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ErpAPIBase + "/api/company-setting/restore-all"
            });
        }

 

        #endregion

        #region ──────────── Query Methods ────────────

        public async Task<APIResponseDto?> GetAllCompanySettingsAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company-setting/all"
            });
        }

        public async Task<APIResponseDto?> GetActiveCompanySettingsAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company-setting/active"
            });
        }

        public async Task<APIResponseDto?> GetSoftDeletedCompanySettingsAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company-setting/SoftDeleted"
            });
        }

        public async Task<APIResponseDto?> GetCompanySettingsByCompanyIdAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-setting/{companyId}"
            });
        }

        public async Task<APIResponseDto?> GetCompanySettingsByCurrencyAsync(Guid currencyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-setting/by-currency/{currencyId}"
            });
        }

        public async Task<APIResponseDto?> GetCompanySettingsByTimeZoneAsync(string timeZone)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-setting/by-timezone/{timeZone}"
            });
        }

        public async Task<APIResponseDto?> GetCompanySettingsPagedAsync(int pageIndex, int pageSize)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/company-setting/paged?pageIndex={pageIndex}&pageSize={pageSize}"
            });
        }

        public async Task<APIResponseDto?> GetCompanySettingsPagedByCurrencyAsync(Guid currencyId, int pageIndex, int pageSize)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/company-setting/paged/by-currency/{currencyId}?pageIndex={pageIndex}&pageSize={pageSize}"
            });
        }

        public async Task<APIResponseDto?> GetCompanySettingsPagedByTimeZoneAsync(string timeZone, int pageIndex, int pageSize)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/company-setting/paged/by-timezone/{timeZone}?pageIndex={pageIndex}&pageSize={pageSize}"
            });
        }

        public async Task<APIResponseDto?> SearchCompanySettingsByKeywordAsync(string keyword)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-setting/search/keyword/{keyword}"
            });
        }

        public async Task<APIResponseDto?> CompanySettingsExistAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-setting/exists/{companyId}"
            });
        }

        public async Task<APIResponseDto?> IsCompanySettingsUniqueAsync(Guid companyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/company-setting/exists/unique/{companyId}"
            });
        }

        public async Task<APIResponseDto?> GetCompanySettingsCountAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company-setting/count"
            });
        }

        public async Task<APIResponseDto?> GetActiveCompanySettingsCountAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/company-setting/count/active"
            });
        }

        


        #endregion
    }
}
