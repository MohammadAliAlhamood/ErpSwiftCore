using ErpSwiftCore.Domain.ICore;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using System.Linq.Expressions;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع خصومات الفواتير (InvoiceDiscount) بدقة واحترافية:
    /// • CRUD & Bulk
    /// • Soft-Delete / Restore / Hard-Delete
    /// • Paging / Filtering / Search
    /// • Existence & Counts
    /// • Multi-Tenancy Support
    /// </summary>
    public interface IInvoiceDiscountRepository : IMultiTenantRepository<InvoiceDiscount>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> CreateAsync(
            InvoiceDiscount discount,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(
            IEnumerable<InvoiceDiscount> discounts,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            InvoiceDiscount discount,
            CancellationToken cancellationToken = default);

        // ----------- [Soft-Delete / Restore] -----------
        Task<bool> SoftDeleteAsync(
            Guid discountId,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> discountIds,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(
            Guid discountId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> discountIds,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        // ----------- [Hard-Delete] -----------
        Task<bool> DeleteAsync(
            Guid discountId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> discountIds,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------
        Task<InvoiceDiscount?> GetByIdAsync(
            Guid discountId,
            CancellationToken cancellationToken = default);

        Task<InvoiceDiscount?> GetByNameAsync(
            Guid invoiceId,
            string discountName,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------
        Task<IReadOnlyList<InvoiceDiscount>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceDiscount>> GetByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceDiscount>> GetByRateRangeAsync(
            decimal minRate,
            decimal maxRate,
            CancellationToken cancellationToken = default);
         

        // ----------- [Existence & Counts] -----------
        Task<bool> ExistsAsync(
            Guid discountId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Guid? invoiceId = null,
            CancellationToken cancellationToken = default);
    }
}