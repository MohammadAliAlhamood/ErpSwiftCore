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
    public class CustomFinancialReportResultRepository : MultiTenantRepository<CustomFinancialReportResult>, ICustomFinancialReportResultRepository
    {
        public CustomFinancialReportResultRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<CustomFinancialReportResult> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        public async Task<Guid> CreateAsync(CustomFinancialReportResult result, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(result, true, cancellationToken);
            return result.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CustomFinancialReportResult> results, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var r in results)
                ids.Add(await CreateAsync(r, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(CustomFinancialReportResult result, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(result, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid resultId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(r => r.ID == resultId, cancellationToken);
            if (entity is null) return false;

            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> resultIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in resultIds)
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(null, cancellationToken);
            return await DeleteRangeAsync(all.Select(r => r.ID), cancellationToken);
        }
        public async Task<bool> RestoreAsync(Guid resultId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(r => r.ID == resultId, cancellationToken);
            if (entity is null) return false;

            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> resultIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in resultIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await base.GetAllSoftDeletedAsync(null, cancellationToken);
            return await RestoreRangeAsync(SoftDeleted.Select(r => r.ID), cancellationToken);
        }
        public Task<bool> ExistsByIdAsync(Guid resultId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(r => r.ID == resultId, cancellationToken);
        public Task<bool> ExistsByReportNameAsync(string reportName, CancellationToken cancellationToken = default)
            => base.ExistsAsync(r => r.ReportName == reportName, cancellationToken);
        public Task<CustomFinancialReportResult?> GetByIdAsync(Guid resultId, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(r => r.ID == resultId, cancellationToken);
        public Task<CustomFinancialReportResult?> GetByFilterAsync(Expression<Func<CustomFinancialReportResult, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(filter, cancellationToken);
        public async Task<IReadOnlyList<CustomFinancialReportResult>> GetAllAsync(Expression<Func<CustomFinancialReportResult, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(filter, cancellationToken);
        public async Task<IReadOnlyList<CustomFinancialReportResult>> GetSoftDeletedAsync(Expression<Func<CustomFinancialReportResult, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(filter, cancellationToken);
        public async Task<IReadOnlyList<CustomFinancialReportResult>> GetByIdsAsync(IEnumerable<Guid> resultIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(r => resultIds.Contains(r.ID), cancellationToken);
        public async Task<IReadOnlyList<CustomFinancialReportResult>> SearchByReportNameAsync(string reportNameKeyword, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(r => r.ReportName.Contains(reportNameKeyword), cancellationToken);
        public async Task<(IReadOnlyList<CustomFinancialReportResult> Results, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<CustomFinancialReportResult, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter,
                 orderBy: r => r.CreatedAt,
                ascending: false,
                cancellationToken);
            return (items, total);
        }
        public Task<int> CountAllAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
        public async Task<Dictionary<string, int>> GetResultCountByReportNameAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(null, cancellationToken);
            return all.GroupBy(r => r.ReportName).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}