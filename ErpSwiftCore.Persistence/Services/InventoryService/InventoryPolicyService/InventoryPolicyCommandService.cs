using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryPolicyService;

namespace ErpSwiftCore.Persistence.Services.InventoryService.InventoryPolicyService
{
    public class InventoryPolicyCommandService : IInventoryPolicyCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public InventoryPolicyCommandService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // 1. تفعيل إعادة الطلب التلقائي مع تحديد الكمية
        public async Task<bool> EnableAutoReorderAsync(Guid inventoryId, int reorderQuantity, CancellationToken cancellationToken = default)
        {
            var policy = await _unitOfWork.InventoryPolicy.GetByInventoryIdAsync(inventoryId, cancellationToken);
            if (policy is null) return false;

            return await _unitOfWork.InventoryPolicy.EnableAutoReorderAsync(inventoryId, reorderQuantity, cancellationToken);
        }

        // 2. تعطيل إعادة الطلب التلقائي
        public async Task<bool> DisableAutoReorderAsync(Guid inventoryId, CancellationToken cancellationToken = default)
        {
            var policy = await _unitOfWork.InventoryPolicy.GetByInventoryIdAsync(inventoryId, cancellationToken);
            if (policy is null) return false;

            return await _unitOfWork.InventoryPolicy.DisableAutoReorderAsync(inventoryId, cancellationToken);
        }

        // 3. تحديث السياسة كاملة
        public async Task<bool> UpdatePolicyAsync(InventoryPolicy policy, CancellationToken cancellationToken = default)
        {
            if (!await _unitOfWork.InventoryPolicy.ExistsByIdAsync(policy.ID, cancellationToken))
                return false;

            return await _unitOfWork.InventoryPolicy.UpdateAsync(policy, cancellationToken);
        }

        // 4. تحديث حد إعادة الطلب
        public async Task<bool> UpdateReorderLevelAsync(Guid policyId, int reorderLevel, CancellationToken cancellationToken = default)
        {
            if (!await _unitOfWork.InventoryPolicy.ExistsByIdAsync(policyId, cancellationToken))
                return false;

            return await _unitOfWork.InventoryPolicy.UpdateReorderLevelAsync(policyId, reorderLevel, cancellationToken);
        }

        // 5. تحديث الحد الأقصى للمخزون
        public async Task<bool> UpdateMaxStockLevelAsync(Guid policyId, int maxStockLevel, CancellationToken cancellationToken = default)
        {
            if (!await _unitOfWork.InventoryPolicy.ExistsByIdAsync(policyId, cancellationToken))
                return false;

            return await _unitOfWork.InventoryPolicy.UpdateMaxStockLevelAsync(policyId, maxStockLevel, cancellationToken);
        }

        // 6. مثال على باتش لتحديث عدة سياسات دفعة واحدة
        public async Task<bool> UpdatePoliciesAsync(IEnumerable<InventoryPolicy> policies, CancellationToken cancellationToken = default)
        {
            // التحقق من وجود كل سياسة
            foreach (var p in policies)
            {
                if (!await _unitOfWork.InventoryPolicy.ExistsByIdAsync(p.ID, cancellationToken))
                    return false;
            }

            // استخدام دالة UpdateRangeAsync في المستودع
            return await _unitOfWork.InventoryPolicy.UpdateRangeAsync(policies, cancellationToken);
        }
 
    
    
    }
}
