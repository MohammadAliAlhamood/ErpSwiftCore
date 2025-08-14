using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanySettingsService
{
    /// <summary>
    /// تنفيذ استعلامات إدارة إعدادات الشركة - تشمل جميع الدوال التي تُرجع بيانات دون تعديل،
    /// مثل الحصول على إعدادات واحدة أو قائمة إعدادات، الترميز، الفرز، البحث، والعدّ.
    /// يعتمد على IUnitOfWork فقط.
    /// </summary>
    public class CompanySettingsQueryService : ICompanySettingsQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanySettingsQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // -------------------- [Get/Query Operations - Single] --------------------

        public Task<CompanySettings?> GetCompanySettingsByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------

        public Task<IReadOnlyList<CompanySettings>> GetAllCompanySettingsAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetAllAsync(cancellationToken);

      
        public Task<IReadOnlyList<CompanySettings>> GetSoftDeletedCompanySettingsAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetSoftDeletedAsync(cancellationToken);

        public Task<IReadOnlyList<CompanySettings>> GetCompanySettingsByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetByCurrencyAsync(currencyId, cancellationToken);

        public Task<IReadOnlyList<CompanySettings>> GetCompanySettingsByTimeZoneAsync(string timeZone, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetByTimeZoneAsync(timeZone, cancellationToken);

        // -------------------- [Paging/Filtering/Searching - Specialized] --------------------

        public Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetCompanySettingsPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetPagedAsync(pageIndex, pageSize, cancellationToken);

        public Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetCompanySettingsPagedByCurrencyAsync(Guid currencyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetPagedByCurrencyAsync(currencyId, pageIndex, pageSize, cancellationToken);

        public Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetCompanySettingsPagedByTimeZoneAsync(string timeZone, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetPagedByTimeZoneAsync(timeZone, pageIndex, pageSize, cancellationToken);

        // -------------------- [Search Operations] --------------------

        public Task<IReadOnlyList<CompanySettings>> SearchCompanySettingsByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.SearchByKeywordAsync(keyword, cancellationToken);

        // -------------------- [Counts & Stats] --------------------

        public Task<int> GetCompanySettingsCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetCountAsync(cancellationToken);
 
    }
}