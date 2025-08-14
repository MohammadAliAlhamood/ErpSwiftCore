using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Domain.IRepositories.ICompanyRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Repositories.CompanyRepositories
{
    public class CompanySettingsRepository : Repository<CompanySettings>, ICompanySettingsRepository
    {
        public CompanySettingsRepository(AppDbContext db, IUserProvider userProvider, IIncludeValidator<CompanySettings> includeValidator) : base(db, userProvider, includeValidator)
        {
        }

        public async Task<Guid> AddAsync(CompanySettings settings, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(settings, true, cancellationToken);
            return settings.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CompanySettings> settingsList, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var s in settingsList)
                ids.Add(await AddAsync(s, cancellationToken));
            return ids;
        }

        public async Task<bool> UpdateAsync(CompanySettings settings, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(settings, true, cancellationToken);
            return true;
        }

        // ----------- [Delete/Archive/Restore] -----------
        public async Task<bool> SoftDeleteAsync(Guid settingsId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(s => s.ID == settingsId,  cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> settingsIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in settingsIds)
                ok &= await SoftDeleteAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            return await SoftDeleteRangeAsync(all.Select(s => s.ID), cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid settingsId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(s => s.ID == settingsId, cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> settingsIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in settingsIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetSoftDeletedAsync(cancellationToken);
            return await RestoreRangeAsync(all.Select(s => s.ID), cancellationToken);
        }
 
        // ----------- [Get/Query - Single] -----------
        public Task<CompanySettings?> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => base.GetAsync(s => s.CompanyID == companyId,  cancellationToken);

        // ----------- [Get/Query - Bulk/Advanced] -----------
        public async Task<IReadOnlyList<CompanySettings>> GetAllAsync(CancellationToken cancellationToken = default)
            => (await base.GetAllAsync(null, cancellationToken)).ToList().AsReadOnly();


        public async Task<IReadOnlyList<CompanySettings>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
            => (await base.GetAllSoftDeletedAsync(null, cancellationToken)).ToList().AsReadOnly();

        public async Task<IReadOnlyList<CompanySettings>> GetByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default)
            => (await base.GetAllAsync(s => s.CurrencyId == currencyId, cancellationToken)).ToList().AsReadOnly();

        public async Task<IReadOnlyList<CompanySettings>> GetByTimeZoneAsync(string timeZone, CancellationToken cancellationToken = default)
            => (await base.GetAllAsync(s => s.TimeZone == timeZone, cancellationToken)).ToList().AsReadOnly();

        // ----------- [Paging/Filtering/Searching] -----------
        public async Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(null, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, null,  s => s.ID, true, cancellationToken);
            return (items.ToList().AsReadOnly(), total);
        }

        public async Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetPagedByCurrencyAsync(Guid currencyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<CompanySettings, bool>> filter = s => s.CurrencyId == currencyId;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, s => s.ID, true, cancellationToken);
            return (items.ToList().AsReadOnly(), total);
        }

        public async Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetPagedByTimeZoneAsync(string timeZone, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<CompanySettings, bool>> filter = s => s.TimeZone == timeZone;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, s => s.ID, true, cancellationToken);
            return (items.ToList().AsReadOnly(), total);
        }

        // ----------- [Search] -----------
        public async Task<IReadOnlyList<CompanySettings>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Array.Empty<CompanySettings>();
            var lowered = keyword.ToLower();
            return (await base.GetAllAsync(
                s => (s.TimeZone != null && s.TimeZone.ToLower().Contains(lowered)) ||
                     (s.DefaultLanguage != null && s.DefaultLanguage.ToLower().Contains(lowered)) ||
                     (s.PaymentTerms != null && s.PaymentTerms.ToLower().Contains(lowered)),  cancellationToken)).ToList().AsReadOnly();
        }

        // ----------- [Existence & Validation] -----------
        public Task<bool> ExistsForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(s => s.CompanyID == companyId, cancellationToken);

        public async Task<bool> IsUniqueForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return !await base.ExistsAsync(s => s.CompanyID == companyId, cancellationToken);
        }

        // ----------- [Counts & Stats] -----------
        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
         

        // ----------- [Custom Update Methods] -----------
        public async Task<bool> UpdateCurrencyAsync(Guid companyId, Guid currencyId, CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(s => s.CompanyID == companyId, cancellationToken: cancellationToken);
            if (settings is null)
                return false;
            settings.CurrencyId = currencyId;
            await base.UpdateAsync(settings, true, cancellationToken);
            return true;
        }

        public async Task<bool> UpdateTimeZoneAsync(Guid companyId, string timeZone, CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(s => s.CompanyID == companyId, cancellationToken: cancellationToken);
            if (settings is null)
                return false;
            settings.TimeZone = timeZone;
            await base.UpdateAsync(settings, true, cancellationToken);
            return true;
        }

        public async Task<bool> UpdateDefaultARAccountAsync(
              Guid companyId,
              Guid accountId,
              CancellationToken cancellationToken = default)
        {
            // 1. Fetch existing settings for the company
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId, 
                cancellationToken: cancellationToken);

            if (settings is null)
                return false;

            // 2. Update the DefaultARAccountId field
            settings.DefaultARAccountId = accountId;

            // 3. Persist changes
            await base.UpdateAsync(settings, true, cancellationToken);
            return true;
        }

        public async Task<bool> UpdateDefaultRevenueAccountAsync(
            Guid companyId,
            Guid accountId,
            CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId, 
                cancellationToken: cancellationToken);

            if (settings is null)
                return false;

            settings.DefaultRevenueAccountId = accountId;
            await base.UpdateAsync(settings, true, cancellationToken);
            return true;
        }

        public async Task<bool> UpdateDefaultTaxPayableAccountAsync(
            Guid companyId,
            Guid accountId,
            CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId,
                cancellationToken: cancellationToken);

            if (settings is null)
                return false;

            settings.DefaultTaxPayableAccountId = accountId;
            await base.UpdateAsync(settings, true, cancellationToken);
            return true;
        }

        public async Task<bool> UpdateDefaultDiscountsAccountAsync(
            Guid companyId,
            Guid accountId,
            CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId,
                cancellationToken: cancellationToken);

            if (settings is null)
                return false;

            settings.DefaultDiscountsAccountId = accountId;
            await base.UpdateAsync(settings, true, cancellationToken);
            return true;
        }

        public async Task<Guid?> GetDefaultARAccountAsync(
            Guid companyId,
            CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId,
                cancellationToken);

            return settings?.DefaultARAccountId;
        }

        public async Task<Guid?> GetDefaultRevenueAccountAsync(
            Guid companyId,
            CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId,
                cancellationToken);

            return settings?.DefaultRevenueAccountId;
        }

        public async Task<Guid?> GetDefaultTaxPayableAccountAsync(
            Guid companyId,
            CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId,
                cancellationToken);

            return settings?.DefaultTaxPayableAccountId;
        }

        public async Task<Guid?> GetDefaultDiscountsAccountAsync(
            Guid companyId,
            CancellationToken cancellationToken = default)
        {
            var settings = await base.GetAsync(
                s => s.CompanyID == companyId,
                cancellationToken);

            return settings?.DefaultDiscountsAccountId;
        }
    }
}