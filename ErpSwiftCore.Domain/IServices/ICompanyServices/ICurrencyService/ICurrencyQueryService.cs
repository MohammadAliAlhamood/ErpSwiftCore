using ErpSwiftCore.SharedKernel.Entities;
namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService
{
    public interface ICurrencyQueryService
    { 
        Task<Currency?> GetCurrencyByIdAsync(Guid currencyId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Currency>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);
    }
}
