using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.SharedKernel.Entities; 
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
namespace ErpSwiftCore.Persistence.Services.CompanyService.CurrencyService
{
    /// <summary>
    /// تنفيذ استعلامات إدارة العملات - يشمل وجود العملة، الجلب الفردي والجلب الشامل.
    /// يعتمد على IUnitOfWork فقط.
    /// </summary>
    public class CurrencyQueryService : ICurrencyQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Currency?> GetCurrencyByIdAsync(Guid currencyId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.Currency.GetAsync(c => c.ID == currencyId, cancellationToken);
        }

        public Task<IReadOnlyList<Currency>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            return _unitOfWork.Currency.GetAllAsync(cancellationToken);
        }

        public Task<bool> ExistsCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.Currency.ExistsAsync(c => c.ID == currencyId, cancellationToken);
        }
    }
}
