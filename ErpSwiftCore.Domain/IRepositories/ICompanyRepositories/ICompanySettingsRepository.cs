using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.ICompanyRepositories
{
    /// <summary>
    /// مستودع إعدادات الشركة - يدعم جميع السيناريوهات الاحترافية (CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, إلخ)
    /// </summary>
    public interface ICompanySettingsRepository : IRepository<CompanySettings>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> AddAsync(CompanySettings settings, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CompanySettings> settingsList, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(CompanySettings settings, CancellationToken cancellationToken = default);

        // ----------- [Delete/Archive/Restore] -----------
        Task<bool> SoftDeleteAsync(Guid settingsId, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> settingsIds, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid settingsId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> settingsIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
         

        // ----------- [Get/Query - Single] -----------
        Task<CompanySettings?> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        // ----------- [Get/Query - Bulk/Advanced] -----------
        Task<IReadOnlyList<CompanySettings>> GetAllAsync(CancellationToken cancellationToken = default);


        Task<IReadOnlyList<CompanySettings>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CompanySettings>> GetByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CompanySettings>> GetByTimeZoneAsync(string timeZone, CancellationToken cancellationToken = default);

        // ----------- [Paging/Filtering/Searching] -----------
        Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetPagedByCurrencyAsync(Guid currencyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetPagedByTimeZoneAsync(string timeZone, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        // ----------- [Search] -----------
        Task<IReadOnlyList<CompanySettings>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default);

        // ----------- [Existence & Validation] -----------
        Task<bool> ExistsForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<bool> IsUniqueForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        // ----------- [Update Specific Fields] -----------
        Task<bool> UpdateCurrencyAsync(Guid companyId, Guid currencyId, CancellationToken cancellationToken = default);

        Task<bool> UpdateTimeZoneAsync(Guid companyId, string timeZone, CancellationToken cancellationToken = default);

        // ----------- [Financial Account Settings] -----------
        Task<bool> UpdateDefaultARAccountAsync(Guid companyId, Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> UpdateDefaultRevenueAccountAsync(Guid companyId, Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> UpdateDefaultTaxPayableAccountAsync(Guid companyId, Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> UpdateDefaultDiscountsAccountAsync(Guid companyId, Guid accountId, CancellationToken cancellationToken = default);

        Task<Guid?> GetDefaultARAccountAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<Guid?> GetDefaultRevenueAccountAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<Guid?> GetDefaultTaxPayableAccountAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<Guid?> GetDefaultDiscountsAccountAsync(Guid companyId, CancellationToken cancellationToken = default);

        // ----------- [Counts & Stats] -----------
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
         
    }
}