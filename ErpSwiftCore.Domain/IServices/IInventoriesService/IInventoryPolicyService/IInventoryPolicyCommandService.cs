using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryPolicyService
{
    public interface IInventoryPolicyCommandService
    {
       Task<bool> EnableAutoReorderAsync(Guid inventoryId, int reorderQuantity, CancellationToken cancellationToken = default);
       Task<bool> DisableAutoReorderAsync(Guid inventoryId, CancellationToken cancellationToken = default);
       Task<bool> UpdatePolicyAsync(InventoryPolicy policy, CancellationToken cancellationToken = default);
       Task<bool> UpdateReorderLevelAsync(Guid policyId, int reorderLevel, CancellationToken cancellationToken = default);
       Task<bool> UpdateMaxStockLevelAsync(Guid policyId, int maxStockLevel, CancellationToken cancellationToken = default);
       Task<bool> UpdatePoliciesAsync(IEnumerable<InventoryPolicy> policies, CancellationToken cancellationToken = default);
    }
}
