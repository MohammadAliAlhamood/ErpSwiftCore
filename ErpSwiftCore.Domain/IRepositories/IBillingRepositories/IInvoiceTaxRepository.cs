using ErpSwiftCore.Domain.Entities.EntityBilling;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع ضرائب الفواتير (InvoiceTax) بدقة واحترافية:
    /// • CRUD & Bulk
    /// • Soft-Delete / Restore / Hard-Delete
    /// • Paging / Filtering / Search
    /// • Existence & Counts
    /// • Multi-Tenancy Support
    /// </summary>
    public interface IInvoiceTaxRepository : IMultiTenantRepository<InvoiceTax>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> CreateAsync(
            InvoiceTax tax,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(
            IEnumerable<InvoiceTax> taxes,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            InvoiceTax tax,
            CancellationToken cancellationToken = default);

        // ----------- [Soft-Delete / Restore] -----------
        Task<bool> SoftDeleteAsync(
            Guid taxId,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> taxIds,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(
            Guid taxId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> taxIds,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        // ----------- [Hard-Delete] -----------
        Task<bool> DeleteAsync(
            Guid taxId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> taxIds,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------
        Task<InvoiceTax?> GetByIdAsync(
            Guid taxId,
            CancellationToken cancellationToken = default);

        Task<InvoiceTax?> GetByNameAsync(
            Guid invoiceId,
            string taxName,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------
        Task<IReadOnlyList<InvoiceTax>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceTax>> GetByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceTax>> GetByRateRangeAsync(
            decimal minRate,
            decimal maxRate,
            CancellationToken cancellationToken = default);

 

        // ----------- [Existence & Counts] -----------
        Task<bool> ExistsAsync(
            Guid taxId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Guid? invoiceId = null,
            decimal? minRate = null,
            decimal? maxRate = null,
            CancellationToken cancellationToken = default);
    }
}