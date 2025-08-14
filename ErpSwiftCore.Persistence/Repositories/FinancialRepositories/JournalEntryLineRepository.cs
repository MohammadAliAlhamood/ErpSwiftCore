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
    public class JournalEntryLineRepository : MultiTenantRepository<JournalEntryLine>, IJournalEntryLineRepository
    {
        private static readonly Expression<Func<JournalEntryLine, object>>[] DefaultIncludes =  {  x => x.Account,   x => x.JournalEntry, x => x.CostCenter };

        public JournalEntryLineRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<JournalEntryLine> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        public async Task<Guid> CreateAsync(JournalEntryLine line, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(line, true, cancellationToken);
            return line.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<JournalEntryLine> lines, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var line in lines)
                ids.Add(await CreateAsync(line, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(JournalEntryLine line, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(line, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(l => l.ID == lineId,  cancellationToken, DefaultIncludes);
            if (entity is null) return false;

            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in lineIds)
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            return await DeleteRangeAsync(all.Select(l => l.ID), cancellationToken);
        }
        public async Task<bool> RestoreAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(l => l.ID == lineId,   cancellationToken, DefaultIncludes);
            if (entity is null) return false;

            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in lineIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
            return await RestoreRangeAsync(SoftDeleted.Select(l => l.ID), cancellationToken);
        }
        public Task<bool> ExistsByIdAsync(Guid lineId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(l => l.ID == lineId, cancellationToken);
        public Task<bool> ExistsByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(l => l.AccountId == accountId, cancellationToken);
        public Task<bool> ExistsByCostCenterAsync(Guid costCenterId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(l => l.CostCenterId == costCenterId, cancellationToken);
        public Task<JournalEntryLine?> GetByIdAsync(Guid lineId, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(l => l.ID == lineId, cancellationToken, DefaultIncludes);
        public Task<JournalEntryLine?> GetByFilterAsync(Expression<Func<JournalEntryLine, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntryLine>> GetAllAsync(Expression<Func<JournalEntryLine, bool>>? filter = null, CancellationToken cancellationToken = default)
            =>  await base.GetAllAsync(filter, cancellationToken, DefaultIncludes) ;
        public async Task<IReadOnlyList<JournalEntryLine>> GetSoftDeletedAsync(Expression<Func<JournalEntryLine, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntryLine>> GetByIdsAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(l => lineIds.Contains(l.ID), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntryLine>> GetByJournalEntryIdAsync(Guid journalEntryId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(l => l.JournalEntryId == journalEntryId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<JournalEntryLine>> SearchByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
            =>  await base.GetAllAsync(l => l.JournalEntry.EntryDate >= from && l.JournalEntry.EntryDate <= to, cancellationToken, DefaultIncludes) 
               ;
        public async Task<(IReadOnlyList<JournalEntryLine> Lines, int TotalCount)> GetPagedAsync(int pageIndex,int pageSize,Expression<Func<JournalEntryLine, bool>>? filter = null,CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter,
                orderBy: l => l.JournalEntry.EntryDate,
                ascending: false,
                cancellationToken,
                DefaultIncludes);
            return (items.ToList().AsReadOnly(), total);
        }
        public async Task<decimal> SumDebitByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var lines = await base.GetAllAsync(l => l.AccountId == accountId && l.IsDebit, cancellationToken, DefaultIncludes);
            return lines.Sum(l => l.Amount);
        }
        public async Task<decimal> SumCreditByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var lines = await base.GetAllAsync(l => l.AccountId == accountId && !l.IsDebit, cancellationToken, DefaultIncludes);
            return lines.Sum(l => l.Amount);
        }
        public async Task<decimal> GetBalanceChangeByAccountAsync(Guid accountId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var lines = await base.GetAllAsync(
                l => l.AccountId == accountId
                     && l.JournalEntry.EntryDate >= from
                     && l.JournalEntry.EntryDate <= to,
                     cancellationToken, DefaultIncludes);

            // balance change = debits minus credits
            var debitSum = lines.Where(l => l.IsDebit).Sum(l => l.Amount);
            var creditSum = lines.Where(l => !l.IsDebit).Sum(l => l.Amount);
            return debitSum - creditSum;
        }
    }
}