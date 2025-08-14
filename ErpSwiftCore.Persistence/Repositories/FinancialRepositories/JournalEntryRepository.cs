using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.Entities.EntityInventory;
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
    public class JournalEntryRepository : MultiTenantRepository<JournalEntry>, IJournalEntryRepository
    {
        private static readonly Expression<Func<JournalEntry, object>>[] DefaultIncludes = { x => x.Lines };

        public JournalEntryRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<JournalEntry> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        public async Task<Guid> CreateAsync(JournalEntry entry, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(entry, true, cancellationToken);
            return entry.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<JournalEntry> entries, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var entry in entries)
                ids.Add(await CreateAsync(entry, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(JournalEntry entry, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(entry, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid entryId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(e => e.ID == entryId, cancellationToken, DefaultIncludes);
            if (entity is null) return false;

            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in entryIds)
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            return await DeleteRangeAsync(all.Select(e => e.ID), cancellationToken);
        }
        public async Task<bool> RestoreAsync(Guid entryId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(e => e.ID == entryId, cancellationToken, DefaultIncludes);
            if (entity is null) return false;

            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in entryIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
            return await RestoreRangeAsync(SoftDeleted.Select(e => e.ID), cancellationToken);
        }
        public Task<bool> ExistsByIdAsync(Guid entryId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(e => e.ID == entryId, cancellationToken);
        public Task<bool> ExistsByEntryNumberAsync(string entryNumber, CancellationToken cancellationToken = default)
            => base.ExistsAsync(e => e.EntryNumber == entryNumber, cancellationToken);
        public Task<bool> ExistsByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default)
            => base.ExistsAsync(e => e.ReferenceNumber == referenceNumber, cancellationToken);
        public Task<JournalEntry?> GetByIdAsync(Guid entryId, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(e => e.ID == entryId, cancellationToken, DefaultIncludes);
        public Task<JournalEntry?> GetByEntryNumberAsync(string entryNumber, CancellationToken cancellationToken = default)
            => base.GetAsync(e => e.EntryNumber == entryNumber, cancellationToken, DefaultIncludes);
        public Task<JournalEntry?> GetByFilterAsync(Expression<Func<JournalEntry, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetAllAsync(Expression<Func<JournalEntry, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetSoftDeletedAsync(Expression<Func<JournalEntry, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetByIdsAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(e => entryIds.Contains(e.ID), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(e => e.Lines.Any(l => l.AccountId == accountId), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetByDateAsync(DateTime entryDate, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(e => e.EntryDate.Date == entryDate.Date, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(e => e.EntryDate >= fromDate && e.EntryDate <= toDate, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetPostedAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(e => e.IsPosted, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntry>> GetUnpostedAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(e => !e.IsPosted, cancellationToken, DefaultIncludes);
        public async Task<(IReadOnlyList<JournalEntry> Entries, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<JournalEntry, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter
                , orderBy: e => e.EntryDate, ascending: false, cancellationToken,
                DefaultIncludes);
            return (items, total);
        }
        public Task<int> CountByDateAsync(DateTime entryDate, CancellationToken cancellationToken = default)
            => base.CountAsync(e => e.EntryDate.Date == entryDate.Date, cancellationToken);
        public Task<int> CountPostedAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(e => e.IsPosted, cancellationToken);
        public Task<int> CountUnpostedAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(e => !e.IsPosted, cancellationToken);
        public async Task<decimal> SumTotalByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            // جلب جميع القيود التي تحتوي على السطر للحساب المطلوب
            var entries = await base.GetAllAsync(
                e => e.Lines.Any(l => l.AccountId == accountId),
                cancellationToken,
                DefaultIncludes);

            // حساب المجموع: إذا كان IsDebit = true نضيف المبلغ، وإلا نخصم المبلغ
            return entries
                .SelectMany(e => e.Lines)
                .Where(l => l.AccountId == accountId)
                .Sum(l => l.IsDebit ? l.Amount : -l.Amount);
        }
        public async Task<Dictionary<DateTime, int>> GetDailyEntryCountsAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            var entries = await base.GetAllAsync(e => e.EntryDate >= fromDate && e.EntryDate <= toDate, cancellationToken: cancellationToken);
            return entries
                .GroupBy(e => e.EntryDate.Date)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Task<JournalEntry?> GetByIdAsync(Guid entryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<JournalEntry?> GetByFilterAsync(Expression<Func<JournalEntry, bool>> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}