using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService
{
    public interface IAccountQueryService
    {
        Task<Account?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<Account?> GetSoftDeletedAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Account>> GetAllAccountsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Account>> GetAccountsByTransactionTypeAsync(TransactionType type, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Account>> GetAccountsByIdsAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default);
              // -------------------- [Counts & Stats] --------------------
        Task<int> GetAccountsCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetAccountsCountByTypeAsync(TransactionType type, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalBalanceByTypeAsync(TransactionType type, CancellationToken cancellationToken = default);
        // -------------------- [Relations: Parent/Hierarchy] --------------------
    }
}
