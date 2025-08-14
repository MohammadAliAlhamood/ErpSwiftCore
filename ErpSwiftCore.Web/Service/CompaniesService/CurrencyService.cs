using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
namespace ErpSwiftCore.Web.Service.CompaniesService
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IBaseService _baseService;
        public CurrencyService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> CreateCurrencyAsync(CurrencyCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/currency/create"
            });
        }
        public async Task<APIResponseDto?> UpdateCurrencyAsync(CurrencyUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/currency/update"
            });
        }
        public async Task<APIResponseDto?> DeleteCurrencyAsync(Guid currencyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/currency/delete/{currencyId}"
            });
        }
        public async Task<APIResponseDto?> DeleteCurrenciesRangeAsync(IEnumerable<Guid> currencyIds)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = currencyIds,
                Url = SD.ErpAPIBase + "/api/currency/delete-range"
            });
        }
        public async Task<APIResponseDto?> DeleteAllCurrenciesAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/currency/delete-all"
            });
        }
        public async Task<APIResponseDto?> GetCurrencyByIdAsync(Guid currencyId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/currency/{currencyId}"
            });
        }
        public async Task<APIResponseDto?> GetAllCurrenciesAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/currency/all"
            },
            true);
        }
    }
}
