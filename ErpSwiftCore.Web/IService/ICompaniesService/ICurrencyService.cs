using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.ICompaniesService
{
    public interface ICurrencyService
    {
        Task<APIResponseDto?> CreateCurrencyAsync(CurrencyCreateDto dto);
        Task<APIResponseDto?> UpdateCurrencyAsync(CurrencyUpdateDto dto);
        Task<APIResponseDto?> DeleteCurrencyAsync(Guid currencyId);
        Task<APIResponseDto?> DeleteCurrenciesRangeAsync(IEnumerable<Guid> currencyIds);
        Task<APIResponseDto?> DeleteAllCurrenciesAsync();
        Task<APIResponseDto?> GetCurrencyByIdAsync(Guid currencyId);
        Task<APIResponseDto?> GetAllCurrenciesAsync();
    }
}