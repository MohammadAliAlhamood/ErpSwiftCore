using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع الفواتير (Invoice aggregate) بدقة واحترافية:
    /// • CRUD & Bulk
    /// • Soft-Delete / Restore / Hard-Delete
    /// • Paging / Filtering / Search
    /// • استعلامات متقدمة (Overdue, ByOrder…)
    /// • Existence & Counts
    /// • Multi-Tenancy Support
    /// </summary>
    public interface IInvoiceRepository : IMultiTenantRepository<Invoice>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> CreateAsync(
            Invoice invoice,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(
            IEnumerable<Invoice> invoices,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            Invoice invoice,
            CancellationToken cancellationToken = default);

        // ----------- [Soft-Delete / Restore] -----------
        Task<bool> SoftDeleteAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> invoiceIds,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteByOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> invoiceIds,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreByOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        // ----------- [Hard-Delete] -----------
        Task<bool> DeleteAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> invoiceIds,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------
        Task<Invoice?> GetByIdAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<Invoice?> GetWithDetailsAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Invoice>> GetByOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------
        Task<IReadOnlyList<Invoice>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Invoice>> GetByDateRangeAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Invoice>> GetByStatusAsync(
            InvoiceStatus status,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Invoice>> GetOverdueAsync(
            DateTime asOfDate,
            CancellationToken cancellationToken = default);

         
        // ----------- [Existence & Counts] -----------
        Task<bool> ExistsAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Guid? orderId = null,
            InvoiceStatus? status = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default);

        Task<int> CountOverdueAsync(
            DateTime asOfDate,
            CancellationToken cancellationToken = default);
    }
}