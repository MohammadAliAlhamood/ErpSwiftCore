using ErpSwiftCore.SharedKernel.Entities; 
namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService
{
    /// <summary>
    /// واجهة الأوامر الخاصة بإدارة العملات.
    /// </summary>
    public interface ICurrencyCommandService
    {
        Task<Guid> CreateCurrencyAsync(Currency currency, CancellationToken cancellationToken = default);
         Task<bool> UpdateCurrencyAsync(Currency currency, CancellationToken cancellationToken = default);
        Task<bool> DeleteCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);
        Task<bool> DeleteCurrenciesRangeAsync(IEnumerable<Guid> currencyIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllCurrenciesAsync(CancellationToken cancellationToken = default);
    }
}
