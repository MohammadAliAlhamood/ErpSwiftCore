using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService
{
    public interface ICostCenterValidationService
    {


        // -------------------- [Existence & Validation] --------------------
        Task<bool> CostCenterExistsByIdAsync(Guid centerId, CancellationToken cancellationToken = default);
        Task<bool> CostCenterExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> CostCenterExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
       


    }
}