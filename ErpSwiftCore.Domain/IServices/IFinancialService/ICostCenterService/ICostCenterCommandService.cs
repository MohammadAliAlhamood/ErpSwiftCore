using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService
{
    public interface ICostCenterCommandService
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateCostCenterAsync(CostCenter center, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddCostCentersRangeAsync(IEnumerable<CostCenter> centers, CancellationToken cancellationToken = default);
        Task<bool> UpdateCostCenterAsync(CostCenter center, CancellationToken cancellationToken = default);
        Task<bool> DeleteCostCenterAsync(Guid centerId, CancellationToken cancellationToken = default);
        Task<int> DeleteCostCentersRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteCostCenterAsync(Guid centerId, CancellationToken cancellationToken = default);
        Task<int> SoftDeleteCostCentersRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllCostCentersAsync(CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllCostCentersAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreCostCenterAsync(Guid centerId, CancellationToken cancellationToken = default);
        Task<int> RestoreCostCentersRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllCostCentersAsync(CancellationToken cancellationToken = default); 
        Task<int> BulkImportCostCentersAsync(IEnumerable<CostCenter> centers, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteCostCentersAsync(IEnumerable<Guid> centerIds,  CancellationToken cancellationToken = default);

    }
}
