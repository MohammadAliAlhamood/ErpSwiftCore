using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IRepositories.ICompanyRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Repositories.CompanyRepositories
{
    /// <summary>
    /// Repository for CompanyBranch entity.
    /// Implements all advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.
    /// </summary>
    public class CompanyBranchRepository : Repository<CompanyBranch>, ICompanyBranchRepository
    { 
        private static readonly Expression<Func<CompanyBranch, object>>[] DefaultIncludes =
         {  x => x.Company };

        public CompanyBranchRepository(AppDbContext db, IUserProvider userProvider, IIncludeValidator<CompanyBranch> includeValidator) : base(db, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------
        public async Task<Guid> AddAsync(CompanyBranch branch, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(branch, true, cancellationToken);
            return branch.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CompanyBranch> branches, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var b in branches)
                ids.Add(await AddAsync(b, cancellationToken));
            return ids;
        }

        public async Task<bool> UpdateAsync(CompanyBranch branch, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(branch, true, cancellationToken);
            return true;
        }

        // ----------- [Delete/Archive/Restore] -----------
        public async Task<bool> SoftDeleteAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(b => b.ID == branchId,   cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in branchIds)
                ok &= await SoftDeleteAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> SoftDeleteAllByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var all = await GetByCompanyIdAsync(companyId, cancellationToken);
            return await SoftDeleteRangeAsync(all.Select(b => b.ID), cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(b => b.ID == branchId, cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in branchIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> RestoreAllByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var all = await GetSoftDeletedByCompanyIdAsync(companyId, cancellationToken);
            return await RestoreRangeAsync(all.Select(b => b.ID), cancellationToken);
        }

  

        // ----------- [Get/Query - Single] -----------
        public Task<CompanyBranch?> GetByIdAsync(Guid branchId, CancellationToken cancellationToken = default)
            => base.GetAsync(b => b.ID == branchId, cancellationToken, DefaultIncludes);
        public Task<CompanyBranch?> GetWithCompanyAsync(Guid branchId, CancellationToken cancellationToken = default)
            => base.GetAsync(b => b.ID == branchId, cancellationToken, DefaultIncludes);
        // ----------- [Get/Query - Bulk/Advanced] -----------
        public async Task<IReadOnlyList<CompanyBranch>> GetAllAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(null, cancellationToken, DefaultIncludes);



        public async Task<IReadOnlyList<CompanyBranch>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(b => b.CompanyID == companyId, cancellationToken, DefaultIncludes);
  
        
        public async Task<IReadOnlyList<CompanyBranch>> GetSoftDeletedByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(b => b.CompanyID == companyId, cancellationToken, DefaultIncludes);
        // ----------- [Paging/Filtering/Searching] -----------
        public async Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetPagedAsync(Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<CompanyBranch, bool>> filter = b => b.CompanyID == companyId;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, b => b.BranchName, true, cancellationToken, DefaultIncludes);
            return (items , total);
        }

 

    
        // ----------- [Search] -----------
        public async Task<IReadOnlyList<CompanyBranch>> SearchByNameAsync(Guid companyId, string name, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(b => b.CompanyID == companyId && b.BranchName != null && b.BranchName.Contains(name), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<CompanyBranch>> SearchByCodeAsync(Guid companyId, string code, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(b => b.CompanyID == companyId && b.BranchCode != null && b.BranchCode.Contains(code), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<CompanyBranch>> SearchByKeywordAsync(Guid companyId, string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Array.Empty<CompanyBranch>();
            var lowered = keyword.ToLower();
            return await base.GetAllAsync(
                b => b.CompanyID == companyId &&
                    ((b.BranchName != null && b.BranchName.ToLower().Contains(lowered)) ||
                     (b.BranchCode != null && b.BranchCode.ToLower().Contains(lowered))),
cancellationToken, DefaultIncludes);
        }

        // ----------- [Existence & Validation] -----------
        public Task<bool> ExistsAsync(Guid branchId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(b => b.ID == branchId, cancellationToken);

        public Task<bool> ExistsByCodeAsync(Guid companyId, string branchCode, CancellationToken cancellationToken = default)
            => base.ExistsAsync(b => b.CompanyID == companyId && b.BranchCode == branchCode, cancellationToken);

        public async Task<bool> IsNameUniqueAsync(Guid companyId, string branchName, Guid? excludeBranchId = null, CancellationToken cancellationToken = default)
        {
            return !await base.ExistsAsync(
                b => b.CompanyID == companyId && b.BranchName == branchName && (!excludeBranchId.HasValue || b.ID != excludeBranchId.Value),
                cancellationToken);
        }

        // ----------- [Integrity/Dependency] -----------
        public Task<bool> HasBranchesAsync(Guid companyId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(b => b.CompanyID == companyId, cancellationToken);

        public Task<bool> IsLinkedToCompanyAsync(Guid branchId, Guid companyId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(b => b.ID == branchId && b.CompanyID == companyId, cancellationToken);

        // ----------- [Counts & Stats] -----------
        public Task<int> GetCountAsync(Guid companyId, CancellationToken cancellationToken = default)
            => base.CountAsync(b => b.CompanyID == companyId, cancellationToken);
 
        public Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetSoftDeletedPagedAsync(Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}