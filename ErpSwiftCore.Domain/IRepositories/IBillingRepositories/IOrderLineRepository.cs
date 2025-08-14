using ErpSwiftCore.Domain.Entities.EntityBilling;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع أسطر الطلبات (OrderLine) بدقة واحترافية:
    /// • CRUD & Bulk
    /// • Soft-Delete / Restore / Hard-Delete
    /// • Paging / Filtering / Search
    /// • Existence & Counts
    /// • Multi-Tenancy Support
    /// </summary>
    public interface IOrderLineRepository : IMultiTenantRepository<OrderLine>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> CreateAsync(
            OrderLine line,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> CreateRangeAsync(
            IEnumerable<OrderLine> lines,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            OrderLine line,
            CancellationToken cancellationToken = default);

        // ----------- [Soft-Delete / Restore] -----------
        Task<bool> SoftDeleteAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> lineIds,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteByOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> lineIds,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreByOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        // ----------- [Hard-Delete] -----------
        Task<bool> DeleteAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> lineIds,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteByOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------
        Task<OrderLine?> GetByIdAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<OrderLine?> GetByIdWithDetailsAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<OrderLine>> GetByOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<OrderLine>> GetByProductAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------
        Task<IReadOnlyList<OrderLine>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<OrderLine>> GetByQuantityRangeAsync(
            int minQuantity,
            int maxQuantity,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<OrderLine>> GetBySubTotalRangeAsync(
            decimal minSubTotal,
            decimal maxSubTotal,
            CancellationToken cancellationToken = default);

 

        // ----------- [Existence & Counts] -----------
        Task<bool> ExistsAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForProductAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Guid? orderId = null,
            Guid? productId = null,
            CancellationToken cancellationToken = default);
    }
}