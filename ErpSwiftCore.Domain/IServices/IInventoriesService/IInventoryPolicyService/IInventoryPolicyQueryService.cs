using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums; 

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryPolicyService
{
    public interface IInventoryPolicyQueryService
    {  // [Queries]
        Task<InventoryPolicy?> GetPolicyByIdAsync(Guid policyId, CancellationToken cancellationToken = default);
        Task<InventoryPolicy?> GetPolicyByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryPolicy>> GetAllPoliciesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesByTypeAsync(InventoryPolicyType policyType, CancellationToken cancellationToken = default);
        Task<int> GetPoliciesCountAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesBelowReorderLevelAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesAboveMaxStockLevelAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesWithAutoReorderAsync(CancellationToken cancellationToken = default);
    }
}