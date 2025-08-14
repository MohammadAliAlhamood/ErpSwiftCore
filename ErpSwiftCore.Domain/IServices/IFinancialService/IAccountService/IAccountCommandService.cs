using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService
{
    public interface IAccountCommandService
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAccountAsync(Account account, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddAccountsRangeAsync(IEnumerable<Account> accounts, CancellationToken cancellationToken = default);
        Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken = default);
        // -------------------- [Delete/Archive/Restore Operations] --------------------
        Task<bool> DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAccountsRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAccountsAsync(CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAccountsRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllAccountsAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<bool> RestoreAccountsRangeAsync(IEnumerable<Guid> accountIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAccountsAsync(CancellationToken cancellationToken = default);
        

    }
}
