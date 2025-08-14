using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService; 
namespace ErpSwiftCore.Persistence.Services.FinancialService.AccountService
{
    public class AccountQueryService : IAccountQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork; 
        public AccountQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        public Task<Account?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.GetByIdAsync(accountId, cancellationToken: cancellationToken); 
        public Task<IReadOnlyList<Account>> GetAllAccountsAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.Account.GetAllAsync(cancellationToken: cancellationToken);
        public Task<Account?> GetSoftDeletedAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.GetByFilterAsync(a => a.ID == accountId && a.IsDeleted, false, cancellationToken);
          public Task<IReadOnlyList<Account>> GetAccountsByIdsAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.GetByIdsAsync(accountIds, cancellationToken);
        public Task<IReadOnlyList<Account>> GetAccountsByTransactionTypeAsync(TransactionType type, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.GetAllAsync(a => a.TransactionType == type, cancellationToken);
 
        public Task<int> GetAccountsCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.Account.GetAllAsync(cancellationToken: cancellationToken)
                .ContinueWith(t => t.Result.Count, TaskContinuationOptions.OnlyOnRanToCompletion);
        public Task<int> GetAccountsCountByTypeAsync(TransactionType type, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.GetAllAsync(a => a.TransactionType == type, cancellationToken)
                .ContinueWith(t => t.Result.Count, TaskContinuationOptions.OnlyOnRanToCompletion);
        public Task<decimal> GetTotalBalanceByTypeAsync(TransactionType type, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.SumBalanceByTypeAsync(type.ToString(), cancellationToken);
    }
}
