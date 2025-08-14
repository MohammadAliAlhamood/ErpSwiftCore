using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService
{
    public interface ICostCenterQueryService
    { 
         Task<CostCenter?> GetCostCenterByIdAsync(Guid centerId, CancellationToken cancellationToken = default);
         Task<IReadOnlyList<CostCenter>> GetAllCostCentersAsync(CancellationToken cancellationToken = default);
         Task<IReadOnlyList<CostCenter>> GetCostCentersByNameAsync(string name, CancellationToken cancellationToken = default);
         Task<IReadOnlyList<CostCenter>> GetCostCentersByIdsAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default);
          Task<int> GetCostCentersCountAsync(CancellationToken cancellationToken = default);
         Task<int> GetCostCentersCountByParentAsync(Guid? parentId, CancellationToken cancellationToken = default);


    }
}
