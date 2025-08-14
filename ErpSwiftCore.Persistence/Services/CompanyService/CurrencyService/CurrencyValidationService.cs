using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CompanyService.CurrencyService
{
    public class CurrencyValidationService : ICurrencyValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsCurrencyCodeUniqueAsync(string code, Guid iD, CancellationToken ct)
        {
            return !await _unitOfWork.Currency.ExistsAsync(c => c.CurrencyCode.ToLower() == code.ToLower() && c.ID != iD, ct);
        }

        public async Task<bool> IsCurrencyCodeUniqueAsync(string code, CancellationToken ct)
        {
            return !await _unitOfWork.Currency.ExistsAsync(c => c.CurrencyCode.ToLower() == code.ToLower(), ct);
        }

    }
}
