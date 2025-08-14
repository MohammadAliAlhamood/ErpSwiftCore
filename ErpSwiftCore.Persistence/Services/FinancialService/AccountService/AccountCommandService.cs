using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
namespace ErpSwiftCore.Persistence.Services.FinancialService.AccountService
{
    public class AccountCommandService : IAccountCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IAccountValidationService _validation;
        public AccountCommandService(IMultiTenantUnitOfWork unitOfWork, IAccountValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }
        public async Task<Guid> CreateAccountAsync(Account account, CancellationToken cancellationToken = default)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (string.IsNullOrWhiteSpace(account.AccountNumber))
                throw new InvalidOperationException("AccountNumber is required.");
            if (await _unitOfWork.Account.ExistsByCodeAsync(account.AccountNumber, cancellationToken))
                throw new InvalidOperationException($"Account number '{account.AccountNumber}' already exists.");
            var id = await _unitOfWork.Account.CreateAsync(account, cancellationToken);
            return id;
        }
        public async Task<IEnumerable<Guid>> AddAccountsRangeAsync(IEnumerable<Account> accounts, CancellationToken cancellationToken = default)
        {
            if (accounts == null) throw new ArgumentNullException(nameof(accounts));
            return await _unitOfWork.Account.AddRangeAsync(accounts, cancellationToken);
        }
        public async Task<bool> DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.AccountExistsByIdAsync(accountId, cancellationToken)) return false;
            return await _unitOfWork.Account.DeleteAsync(accountId, cancellationToken);
        }
        public async Task<bool> DeleteAccountsRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Account.DeleteRangeAsync(accountIds, cancellationToken);
        }
        public async Task<bool> DeleteAllAccountsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Account.DeleteAllAsync(cancellationToken);
        }
        public async Task<bool> RestoreAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Account.RestoreAsync(accountId, cancellationToken);
        }
        public async Task<bool> RestoreAccountsRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Account.RestoreRangeAsync(accountIds, cancellationToken);
        }
        public async Task<bool> RestoreAllAccountsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Account.RestoreAllAsync(cancellationToken);
        }
        public Task<bool> SoftDeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
            => DeleteAccountAsync(accountId, cancellationToken);
        public Task<bool> SoftDeleteAccountsRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default)
            => DeleteAccountsRangeAsync(accountIds, cancellationToken);
        public Task<bool> SoftDeleteAllAccountsAsync(CancellationToken cancellationToken = default)
            => DeleteAllAccountsAsync(cancellationToken);
        public async Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken = default)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (!await _validation.AccountExistsByIdAsync(account.ID, cancellationToken)) return false;
            return await _unitOfWork.Account.UpdateAsync(account, cancellationToken);
        }
    }
}