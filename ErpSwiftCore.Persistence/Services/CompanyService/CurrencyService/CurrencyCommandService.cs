using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
namespace ErpSwiftCore.Persistence.Services.CompanyService.CurrencyService
{
    public class CurrencyCommandService : ICurrencyCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CurrencyCommandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> CreateCurrencyAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Currency.AddAsync(currency, cancellationToken);
        }
        public async Task<IEnumerable<Guid>> AddCurrenciesRangeAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var curr in currencies)
            {
                ids.Add(await _unitOfWork.Currency.AddAsync(curr, cancellationToken));
            }
            return ids;
        }
        public async Task<bool> UpdateCurrencyAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Currency.UpdateAsync(currency, cancellationToken);
        }
        public async Task<bool> DeleteCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Currency.DeleteAsync(currencyId, cancellationToken);
        }
        public async Task<bool> DeleteCurrenciesRangeAsync(IEnumerable<Guid> currencyIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Currency.DeleteRangeAsync(currencyIds, cancellationToken);
        }
        public async Task<bool> DeleteAllCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Currency.DeleteAllAsync(cancellationToken);
        }
    }
}
