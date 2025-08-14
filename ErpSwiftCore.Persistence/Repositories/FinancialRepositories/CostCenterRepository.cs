using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IRepositories.IFinancialRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Repositories.FinancialRepositories
{
    public class CostCenterRepository : MultiTenantRepository<CostCenter>, ICostCenterRepository
    {
        private static readonly Expression<Func<CostCenter, object>>[] DefaultIncludes = {   };
        public CostCenterRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<CostCenter> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }
        public async Task<Guid> CreateAsync(CostCenter center, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(center, true, cancellationToken);
            return center.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CostCenter> centers, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var c in centers)
                ids.Add(await CreateAsync(c, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(CostCenter center, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(center, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid centerId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(c => c.ID == centerId, cancellationToken);
            if (entity is null) return false;

            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in centerIds)
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(null, cancellationToken);
            return await DeleteRangeAsync(all.Select(c => c.ID), cancellationToken);
        }
        public async Task<bool> RestoreAsync(Guid centerId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(c => c.ID == centerId, cancellationToken);
            if (entity is null) return false;

            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in centerIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await base.GetAllSoftDeletedAsync(null, cancellationToken);
            return await RestoreRangeAsync(SoftDeleted.Select(c => c.ID), cancellationToken);
        }
        public Task<bool> ExistsByIdAsync(Guid centerId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.ID == centerId, cancellationToken);
        public Task<bool> ExistsByNameAsync(string centerName, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.CenterName == centerName, cancellationToken);
        public Task<bool> ExistsByCodeAsync(string costCenterCode, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.Code == costCenterCode, cancellationToken);
        public Task<CostCenter?> GetByIdAsync(Guid centerId, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == centerId, cancellationToken, DefaultIncludes);
        public Task<CostCenter?> GetByFilterAsync(Expression<Func<CostCenter, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<CostCenter>> GetAllAsync(Expression<Func<CostCenter, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<CostCenter>> GetSoftDeletedAsync(Expression<Func<CostCenter, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<CostCenter>> GetByIdsAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => centerIds.Contains(c.ID), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<CostCenter>> GetByNameAsync(string centerName, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(c => c.CenterName.Contains(centerName), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<CostCenter>> SearchByNameAsync(string partialName, CancellationToken cancellationToken = default)
            => await GetByNameAsync(partialName, cancellationToken); 
        public async Task<(IReadOnlyList<CostCenter> Centers, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<CostCenter, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter,
                orderBy: c => c.CenterName,
                ascending: true,
 cancellationToken, DefaultIncludes); return (items, total);
        }
        public Task<int> CountAllAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
        public async Task<Dictionary<char, int>> GetCountByInitialAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(null, cancellationToken: cancellationToken);
            return all
                .Where(c => !string.IsNullOrEmpty(c.CenterName))
                .GroupBy(c => char.ToUpperInvariant(c.CenterName[0]))
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}