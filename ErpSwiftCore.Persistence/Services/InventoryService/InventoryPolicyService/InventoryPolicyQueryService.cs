using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryPolicyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.InventoryService.InventoryPolicyService
{
    public class InventoryPolicyQueryService : IInventoryPolicyQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public InventoryPolicyQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         
        public async Task<InventoryPolicy?> GetPolicyByIdAsync(Guid policyId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.GetByIdAsync(policyId, cancellationToken);
        }
        public async Task<InventoryPolicy?> GetPolicyByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.GetByInventoryIdAsync(inventoryId, cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetAllPoliciesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.GetAllAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesByTypeAsync(InventoryPolicyType policyType, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.GetPoliciesByTypeAsync(policyType, cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesWithAutoReorderAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.GetPoliciesWithAutoReorderAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesBelowReorderLevelAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.GetPoliciesBelowReorderLevelAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesAboveMaxStockLevelAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.GetPoliciesAboveMaxStockLevelAsync(cancellationToken);
        }
        public async Task<int> GetPoliciesCountAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InventoryPolicy.CountAsync(cancellationToken);
        } 
    }
}
