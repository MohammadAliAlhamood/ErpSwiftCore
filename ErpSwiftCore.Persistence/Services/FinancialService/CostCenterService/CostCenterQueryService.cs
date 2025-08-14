using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.FinancialService.CostCenterService
{
    public class CostCenterQueryService : ICostCenterQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public CostCenterQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<CostCenter?> GetCostCenterByIdAsync(Guid centerId, CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.GetByIdAsync(centerId, cancellationToken: cancellationToken);
         public async Task<IReadOnlyList<CostCenter>> GetAllCostCentersAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CostCenter.GetAllAsync(cancellationToken: cancellationToken);

         } 
        public Task<IReadOnlyList<CostCenter>> GetCostCentersByNameAsync(string name, CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.GetByNameAsync(name, cancellationToken);
        public Task<IReadOnlyList<CostCenter>> GetCostCentersByIdsAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.GetByIdsAsync(centerIds, cancellationToken);
      
        
        public Task<int> GetCostCentersCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.CountAllAsync(cancellationToken);
        public Task<int> GetCostCentersCountByParentAsync(Guid? parentId, CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.GetCountByInitialAsync(cancellationToken)
                .ContinueWith(t => t.Result.TryGetValue(parentId?.ToString()[0] ?? '\0', out var c) ? c : 0);
    }
}
