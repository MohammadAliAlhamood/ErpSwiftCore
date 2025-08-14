using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService;
namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanySettingsService
{
    public class CompanySettingsCommandService : ICompanySettingsCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanySettingsValidationService _validationService;
        public CompanySettingsCommandService(
            IUnitOfWork unitOfWork,
            ICompanySettingsValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }
        public async Task<Guid> CreateCompanySettingsAsync(CompanySettings settings, CancellationToken cancellationToken = default)
        {
            if (!await _unitOfWork.Company.ExistsAsync(settings.CompanyID, cancellationToken))
                throw new InvalidOperationException("الشركة غير موجودة.");
            if (!await _validationService.IsCompanySettingsUniqueAsync(settings.CompanyID, cancellationToken))
                throw new InvalidOperationException("إعدادات الشركة موجودة بالفعل.");
            return await _unitOfWork.CompanySettings.AddAsync(settings, cancellationToken);
        }
        public async Task<IEnumerable<Guid>> AddCompanySettingsRangeAsync(IEnumerable<CompanySettings> settingsList, CancellationToken cancellationToken = default)
        {
            var validList = new List<CompanySettings>();
            foreach (var settings in settingsList)
            {
                if (!await _unitOfWork.Company.ExistsAsync(settings.CompanyID, cancellationToken))
                    throw new InvalidOperationException($"الشركة {settings.CompanyID} غير موجودة.");
                if (!await _validationService.IsCompanySettingsUniqueAsync(settings.CompanyID, cancellationToken))
                    throw new InvalidOperationException($"إعدادات الشركة {settings.CompanyID} موجودة بالفعل.");
                validList.Add(settings);
            }
            return await _unitOfWork.CompanySettings.AddRangeAsync(validList, cancellationToken);
        }
        public async Task<bool> UpdateCompanySettingsAsync(CompanySettings settings, CancellationToken cancellationToken = default)
        {
            if (!await _unitOfWork.Company.ExistsAsync(settings.CompanyID, cancellationToken))
                throw new InvalidOperationException("الشركة غير موجودة.");
            if (!await _validationService.SettingsExistAsync(settings.CompanyID, cancellationToken))
                throw new InvalidOperationException("إعدادات الشركة غير موجودة.");
            return await _unitOfWork.CompanySettings.UpdateAsync(settings, cancellationToken);
        }
        public async Task<bool> DeleteCompanySettingsAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (!await _validationService.SettingsExistAsync(companyId, cancellationToken))
                return false;
            var settings = await _unitOfWork.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken);
            if (settings == null)
                return false;
            return await _unitOfWork.CompanySettings.SoftDeleteAsync(settings.ID, cancellationToken);
        }

        public async Task<bool> DeleteCompanySettingsRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var companyId in companyIds)
            {
                var settings = await _unitOfWork.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken);
                if (settings != null)
                    ids.Add(settings.ID);
            }
            return await _unitOfWork.CompanySettings.SoftDeleteRangeAsync(ids, cancellationToken);
        }
        public async Task<bool> DeleteAllCompanySettingsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CompanySettings.SoftDeleteAllAsync(cancellationToken);
        }
        public async Task<bool> RestoreCompanySettingsAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var settings = await _unitOfWork.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken);
            if (settings == null)
                return false;
            return await _unitOfWork.CompanySettings.RestoreAsync(settings.ID, cancellationToken);
        }
        public async Task<bool> RestoreCompanySettingsRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var companyId in companyIds)
            {
                var settings = await _unitOfWork.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken);
                if (settings != null)
                    ids.Add(settings.ID);
            }
            return await _unitOfWork.CompanySettings.RestoreRangeAsync(ids, cancellationToken);
        }
        public async Task<bool> RestoreAllCompanySettingsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CompanySettings.RestoreAllAsync(cancellationToken);
        }
       
        public async Task<bool> UpdateCompanyCurrencyAsync(Guid companyId, Guid currencyId, CancellationToken cancellationToken = default)
        {
            if (!await _validationService.SettingsExistAsync(companyId, cancellationToken))
                throw new InvalidOperationException("إعدادات الشركة غير موجودة.");
            return await _unitOfWork.CompanySettings.UpdateCurrencyAsync(companyId, currencyId, cancellationToken);
        }
        public async Task<bool> UpdateCompanyTimeZoneAsync(Guid companyId, string timeZone, CancellationToken cancellationToken = default)
        {
            if (!await _validationService.SettingsExistAsync(companyId, cancellationToken))
                throw new InvalidOperationException("إعدادات الشركة غير موجودة.");
            return await _unitOfWork.CompanySettings.UpdateTimeZoneAsync(companyId, timeZone, cancellationToken);
        }
    }
}