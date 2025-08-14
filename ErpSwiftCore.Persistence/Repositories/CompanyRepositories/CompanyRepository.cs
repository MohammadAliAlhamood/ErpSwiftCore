using System.Linq.Expressions;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ErpSwiftCore.Domain.IRepositories.ICompanyRepositories;
namespace ErpSwiftCore.Persistence.Repositories.CompanyRepositories
{
    /// <summary>
    /// Repository for Company entity (the main tenant).
    /// Implements all advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.
    /// </summary>
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext db, IUserProvider userProvider, 
            IIncludeValidator<Company> includeValidator)
            : base(db, userProvider, includeValidator)
        {
        }

        public async Task<Guid> AddAsync(Company company, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(company, true, cancellationToken);
            return company.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Company> companies, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var c in companies)
                ids.Add(await AddAsync(c, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(Company company, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(company, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(c => c.ID == companyId, cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.RemoveAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        {
            // اجلب كل الكيانات التي تطابق قائمة الـ IDs
            IEnumerable<Company> entities = await base.GetAllAsync(c => companyIds.Contains(c.ID), cancellationToken);
            if (entities.Count() > 0)
            {
                await base.RemoveRangeAsync(entities, true, cancellationToken);
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            return await DeleteRangeAsync(all.Select(c => c.ID), cancellationToken);
        }
        // ----------- [Delete/Archive/Restore] -----------
        public async Task<bool> SoftDeleteAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(c => c.ID == companyId, cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        { 
            IEnumerable<Company> entities = await base.GetAllAsync(c => companyIds.Contains(c.ID), cancellationToken);
            if (entities.Count() > 0)
            {
                await base.SoftDeleteAsync((Company)entities, true, cancellationToken);
                return true;
            }
            return false;
        }


        public async Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            return await SoftDeleteRangeAsync(all.Select(c => c.ID), cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(c => c.ID == companyId, cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in companyIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetSoftDeletedAsync(cancellationToken);
            return await RestoreRangeAsync(all.Select(c => c.ID), cancellationToken);
        }

      
        // ----------- [Get/Query - Single] -----------
        public Task<Company?> GetByIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == companyId, cancellationToken);

        public Task<Company?> GetByCodeAsync(string companyCode, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.TaxID != null && c.TaxID == companyCode, cancellationToken);

        public Task<Company?> GetByNameAsync(string companyName, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.CompanyName == companyName, cancellationToken);

        // ----------- [Get/Query - Bulk/Advanced] -----------
        public async Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(null, cancellationToken);
         
        public async Task<IReadOnlyList<Company>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(null, cancellationToken);

        public async Task<IReadOnlyList<Company>> GetByCountryAsync(string country, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => c.Address != null && c.Address.Country == country, cancellationToken);

        public async Task<IReadOnlyList<Company>> GetByIndustryAsync(string industry, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => c.IndustryType.ToString() == industry, cancellationToken);

        public async Task<IReadOnlyList<Company>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => c.CreatedBy == ownerId, cancellationToken);

        // ----------- [Paging/Filtering/Searching] -----------


        public async Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetPagedByCountryAsync(string country, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<Company, bool>> filter = c => c.Address != null && c.Address.Country == country;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, c => c.CompanyName, true, cancellationToken);
            return (items, total);
        }

        public async Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetPagedByIndustryAsync(string industry, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<Company, bool>> filter = c => c.IndustryType.ToString() == industry;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, c => c.CompanyName, true, cancellationToken);
            return (items, total);
        }

 

        // ----------- [Search] -----------
        public async Task<IReadOnlyList<Company>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => c.CompanyName.Contains(name), cancellationToken);

        public async Task<IReadOnlyList<Company>> SearchByTaxIdAsync(string taxId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => c.TaxID != null && c.TaxID.Contains(taxId), cancellationToken);

        public async Task<IReadOnlyList<Company>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return Array.Empty<Company>();
            var lowered = keyword.ToLower();
            return (await base.GetAllAsync(c => c.CompanyName.ToLower().Contains(lowered) || (c.TaxID != null && c.TaxID.ToLower().Contains(lowered)), cancellationToken));
        }

        // ----------- [Existence & Validation] -----------
        public Task<bool> ExistsAsync(Guid companyId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.ID == companyId, cancellationToken);

        public Task<bool> ExistsByCodeAsync(string companyCode, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.TaxID != null && c.TaxID == companyCode, cancellationToken);

        public async Task<bool> IsNameUniqueAsync(string companyName, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default)
        {
            return !await base.ExistsAsync(c => c.CompanyName == companyName && (!excludeCompanyId.HasValue || c.ID != excludeCompanyId.Value), cancellationToken);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default)
        {
            return !await base.ExistsAsync(c => c.ContactInfo.Email == email && (!excludeCompanyId.HasValue || c.ID != excludeCompanyId.Value), cancellationToken);
        }

        // ----------- [Counts & Stats] -----------
        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken); 

        public Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


    }
}