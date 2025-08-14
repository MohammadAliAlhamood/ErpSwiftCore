using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService
{
    public interface IAccountValidationService
    {



        // -------------------- [Existence & Validation] --------------------
        Task<bool> AccountExistsByIdAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> AccountExistsByNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
 
        Task<bool> IsValidParentAccountAsync(Guid parentAccountId, CancellationToken cancellationToken = default);




    }
}