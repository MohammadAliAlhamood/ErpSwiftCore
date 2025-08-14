using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IInventoryRepositories
{
    /// <summary>
    /// Repository interface for managing InventoryPolicy records.
    /// Covers CRUD, retrieval, search, analytics, paging, and validation scenarios for inventory policies.
    /// </summary>
    public interface IInventoryPolicyRepository : IMultiTenantRepository<InventoryPolicy>
    {





        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAsync(InventoryPolicy policy, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(InventoryPolicy policy, CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Single] --------------------
        Task<InventoryPolicy?> GetByIdAsync(Guid policyId, CancellationToken cancellationToken = default);

        Task<InventoryPolicy?> GetByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<InventoryPolicy>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesByTypeAsync(InventoryPolicyType policyType, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesWithAutoReorderAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesBelowReorderLevelAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryPolicy>> GetPoliciesAboveMaxStockLevelAsync(CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering/Searching] --------------------
        Task<(IReadOnlyList<InventoryPolicy> Policies, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryPolicy>> SearchByNotesAsync(string noteTerm, CancellationToken cancellationToken = default);

        // -------------------- [Stats & Analytics] --------------------
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        // -------------------- [Policy Settings] --------------------
        Task<bool> SetAutoReorderAsync(Guid inventoryId, bool enabled, int? reorderQuantity, CancellationToken cancellationToken = default);

        Task<bool> UpdateReorderLevelAsync(Guid policyId, int reorderLevel, CancellationToken cancellationToken = default);

        Task<bool> UpdateMaxStockLevelAsync(Guid policyId, int maxStockLevel, CancellationToken cancellationToken = default);

        // -------------------- [Validation] --------------------
        Task<bool> ExistsByIdAsync(Guid policyId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid policyId, CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid policyId, CancellationToken cancellationToken = default);
        Task<bool> EnableAutoReorderAsync(Guid inventoryId, int reorderQuantity, CancellationToken cancellationToken);
        Task<bool> DisableAutoReorderAsync(Guid inventoryId, CancellationToken cancellationToken);
        Task<bool> UpdateRangeAsync(IEnumerable<InventoryPolicy> policies, CancellationToken cancellationToken);
    }
}