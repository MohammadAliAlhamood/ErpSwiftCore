using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService; 
namespace ErpSwiftCore.Persistence.Services.FinancialService.AccountService
{
    public class AccountValidationService : IAccountValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public AccountValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         public Task<bool> AccountExistsByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
           => _unitOfWork.Account.ExistsByIdAsync(accountId, cancellationToken);
        public Task<bool> AccountExistsByNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.ExistsByCodeAsync(accountNumber, cancellationToken);
        
        public Task<bool> IsValidParentAccountAsync(Guid parentAccountId, CancellationToken cancellationToken = default)
            => _unitOfWork.Account.ExistsByIdAsync(parentAccountId, cancellationToken);
    }
}
