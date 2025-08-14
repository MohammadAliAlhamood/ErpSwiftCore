using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories.IInventoryRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;
namespace ErpSwiftCore.Persistence.Repositories.InventoryRepositories
{
    public class InventoryPolicyRepository
        : MultiTenantRepository<InventoryPolicy>, IInventoryPolicyRepository
    {
        public InventoryPolicyRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<InventoryPolicy> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }
        #region Create / Update
        public async Task<Guid> CreateAsync(InventoryPolicy policy, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(policy, autoSave: true, cancellationToken);
            return policy.ID;
        }
        public async Task<bool> UpdateAsync(InventoryPolicy policy, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(policy, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> UpdateMaxStockLevelAsync(Guid policyId, int maxStockLevel, CancellationToken cancellationToken = default)
        {
            var policy = await GetByIdAsync(policyId, cancellationToken);
            if (policy == null) return false;
            policy.MaxStockLevel = maxStockLevel;
            await base.UpdateAsync(policy, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> UpdateReorderLevelAsync(Guid policyId, int reorderLevel, CancellationToken cancellationToken = default)
        {
            var policy = await GetByIdAsync(policyId, cancellationToken);
            if (policy == null) return false;

            policy.ReorderLevel = reorderLevel;
            await base.UpdateAsync(policy, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> SetAutoReorderAsync(Guid inventoryId, bool enabled, int? reorderQuantity, CancellationToken cancellationToken = default)
        {
            var policy = await GetByInventoryIdAsync(inventoryId, cancellationToken);
            if (policy == null) return false;

            policy.IsAutoReorderEnabled = enabled;
            policy.AutoReorderQuantity = reorderQuantity;
            await base.UpdateAsync(policy, autoSave: true, cancellationToken);
            return true;
        }
        #endregion Create / Update
        #region Retrieval
        public async Task<IReadOnlyList<InventoryPolicy>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(null, cancellationToken: cancellationToken);
            return list.ToList().AsReadOnly();
        }
        public async Task<InventoryPolicy?> GetByIdAsync(Guid policyId, CancellationToken cancellationToken = default)
        {
            return await base.GetAsync(n => n.ID == policyId, cancellationToken: cancellationToken);
        }
        public async Task<InventoryPolicy?> GetByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
        {
            return await base.GetAsync(n => n.InventoryID == inventoryId, cancellationToken: cancellationToken);
        }
        public async Task<(IReadOnlyList<InventoryPolicy> Policies, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var data = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: null,


                orderBy: n => n.CreatedAt,
                ascending: false,
                cancellationToken: cancellationToken);

            var total = await base.CountAsync(null, cancellationToken);
            return (data.ToList().AsReadOnly(), total);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesByTypeAsync(InventoryPolicyType policyType, CancellationToken cancellationToken = default)
        {
            return await GetFilteredAsync(n => n.PolicyType == policyType, cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesAboveMaxStockLevelAsync(CancellationToken cancellationToken = default)
        {
            return await GetFilteredAsync(n => n.CurrentStock > n.MaxStockLevel, cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesBelowReorderLevelAsync(CancellationToken cancellationToken = default)
        {
            return await GetFilteredAsync(n => n.CurrentStock < n.ReorderLevel, cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> GetPoliciesWithAutoReorderAsync(CancellationToken cancellationToken = default)
        {
            return await GetFilteredAsync(n => n.IsAutoReorderEnabled, cancellationToken);
        }
        public async Task<IReadOnlyList<InventoryPolicy>> SearchByNotesAsync(string noteTerm, CancellationToken cancellationToken = default)
        {
            return await GetFilteredAsync(n => n.Notes != null && n.Notes.Contains(noteTerm), cancellationToken);
        }
        #endregion Retrieval
        #region Helpers & Checks
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await base.CountAsync(null, cancellationToken);
        }
        public async Task<bool> ExistsByIdAsync(Guid policyId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(n => n.ID == policyId, cancellationToken);
        }
        private async Task<IReadOnlyList<InventoryPolicy>> GetFilteredAsync(Expression<Func<InventoryPolicy, bool>> filter, CancellationToken cancellationToken)
        {
            var list = await base.GetAllAsync(filter, cancellationToken: cancellationToken);
            return list.ToList().AsReadOnly();
        }
        public async Task<bool> DeleteAsync(Guid policyId, CancellationToken cancellationToken = default)
        {
            var policy = await GetByIdAsync(policyId, cancellationToken);
            if (policy == null) return false;

            await base.SoftDeleteAsync(policy, autoSave: true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAsync(Guid policyId, CancellationToken cancellationToken = default)
        {
            var policy = await base.GetOneSoftDeletedAsync(n => n.ID == policyId, cancellationToken: cancellationToken);
            if (policy == null) return false;

            await base.RestoreAsync(policy, autoSave: true, cancellationToken);
            return true;
        }

        public Task<bool> EnableAutoReorderAsync(Guid inventoryId, int reorderQuantity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAutoReorderAsync(Guid inventoryId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRangeAsync(IEnumerable<InventoryPolicy> policies, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion Helpers & Checks
    }
}