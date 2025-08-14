using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.Enums;
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
    public class AccountRepository : MultiTenantRepository<Account>, IAccountRepository
    {
        private static readonly Expression<Func<Account, object>>[] DefaultIncludes = { 
            x => x.Currency
        
        };
        public AccountRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<Account> includeValidator) : base(db, tenantProvider, userProvider, includeValidator) { }
        public async Task<Guid> CreateAsync(Account account, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(account, true, cancellationToken);
            return account.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Account> accounts, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var acct in accounts)
                ids.Add(await CreateAsync(acct, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(account, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(a => a.ID == accountId, cancellationToken);
            if (entity is null) return false;
            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in accountIds)
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(null, cancellationToken);
            return await DeleteRangeAsync(all.Select(a => a.ID), cancellationToken);
        }
        public async Task<bool> RestoreAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(a => a.ID == accountId, cancellationToken);
            if (entity is null) return false;
            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in accountIds)
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await base.GetAllSoftDeletedAsync(null, cancellationToken);
            return await RestoreRangeAsync(SoftDeleted.Select(a => a.ID), cancellationToken);
        }
        public Task<bool> ExistsByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(a => a.ID == accountId, cancellationToken);
        public Task<bool> ExistsByCodeAsync(string accountCode, CancellationToken cancellationToken = default)
            => base.ExistsAsync(a => a.AccountNumber == accountCode, cancellationToken);
        
        public Task<Account?> GetByIdAsync(Guid accountId, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(a => a.ID == accountId, cancellationToken, DefaultIncludes);
        public Task<Account?> GetByFilterAsync(Expression<Func<Account, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default)
            => base.GetAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Account>> GetAllAsync(Expression<Func<Account, bool>>? filter = null, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Account>> GetSoftDeletedAsync(Expression<Func<Account, bool>>? filter = null, CancellationToken cancellationToken = default)
             => await base.GetAllSoftDeletedAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Account>> GetByIdsAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(a => accountIds.Contains(a.ID), cancellationToken, DefaultIncludes);
       
        
        
        public async Task<IReadOnlyList<Account>> SearchByNameAsync(string nameKeyword, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(a => a.Name.Contains(nameKeyword), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<Account>> SearchByNumberAsync(string accountNumberKeyword, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(a => a.AccountNumber.Contains(accountNumberKeyword), cancellationToken, DefaultIncludes);
        public async Task<(IReadOnlyList<Account> Accounts, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<Account, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, filter, orderBy: a => a.Name, ascending: true, cancellationToken, DefaultIncludes);
            return (items, total);
        }
        public async Task<decimal> SumBalanceByTypeAsync(string transactionType, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<TransactionType>(transactionType, true, out var tt))
                return 0m;
            var accounts = await base.GetAllAsync(a => a.TransactionType == tt, cancellationToken: cancellationToken);
            // compute each account balance as of now via its JournalEntryLines
            return accounts.Sum(a => a.JournalEntryLines
                                        .Sum(l => l.IsDebit ? l.Amount : -l.Amount));
        }
     
    }
}