using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IFinancialRepositories
{
    public interface IAccountRepository : IMultiTenantRepository<Account>
    {
        Task<Guid> CreateAsync(Account account, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Account> accounts, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(Account account, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

        
        Task<Account?> GetByIdAsync(Guid accountId, bool tracked = false, CancellationToken cancellationToken = default);

        Task<Account?> GetByFilterAsync(Expression<Func<Account, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Account>> GetAllAsync(Expression<Func<Account, bool>>? filter = null, CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<Account>> GetSoftDeletedAsync(Expression<Func<Account, bool>>? filter = null, CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<Account>> GetByIdsAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Account> Accounts, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<Account, bool>>? filter = null, CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<Account>> SearchByNameAsync(string nameKeyword, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Account>> SearchByNumberAsync(string accountNumberKeyword, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCodeAsync(string accountCode, CancellationToken cancellationToken = default);

    
        Task<decimal> SumBalanceByTypeAsync(string transactionType, CancellationToken cancellationToken = default);

    }
}