using ErpSwiftCore.Domain.Entities.EntityBilling;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع أسطر الفواتير (InvoiceLine) بدقة واحترافية:
    /// • CRUD & Bulk
    /// • Soft-Delete / Restore / Hard-Delete
    /// • Paging / Filtering / Search
    /// • Existence & Counts
    /// • Multi-Tenancy Support
    /// </summary>
    public interface IInvoiceLineRepository : IMultiTenantRepository<InvoiceLine>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> CreateAsync(
            InvoiceLine line,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(
            IEnumerable<InvoiceLine> lines,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            InvoiceLine line,
            CancellationToken cancellationToken = default);

        // ----------- [Soft-Delete / Restore] -----------
        Task<bool> SoftDeleteAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> lineIds,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> lineIds,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        // ----------- [Hard-Delete] -----------
        Task<bool> DeleteAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> lineIds,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------
        Task<InvoiceLine?> GetByIdAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<InvoiceLine?> GetByIdWithDetailsAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceLine>> GetByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceLine>> GetByProductAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------
        Task<IReadOnlyList<InvoiceLine>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceLine>> GetByQuantityRangeAsync(
            int minQuantity,
            int maxQuantity,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceLine>> GetBySubTotalRangeAsync(
            decimal minSubTotal,
            decimal maxSubTotal,
            CancellationToken cancellationToken = default);

      
        // ----------- [Existence & Counts] -----------
        Task<bool> ExistsAsync(
            Guid lineId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForProductAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Guid? invoiceId = null,
            Guid? productId = null,
            CancellationToken cancellationToken = default);
    }
}