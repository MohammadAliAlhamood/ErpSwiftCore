using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.ICore;
using System.Linq.Expressions;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع موافقات الفواتير (InvoiceApproval) بدقة واحترافية:
    /// • CRUD & Bulk
    /// • Soft-Delete / Restore / Hard-Delete
    /// • Paging / Filtering / Search
    /// • State Management (Status)
    /// • Existence & Counts
    /// • Multi-Tenancy Support
    /// </summary>
    public interface IInvoiceApprovalRepository : IMultiTenantRepository<InvoiceApproval>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> CreateAsync(
            InvoiceApproval approval,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> CreateRangeAsync(
            IEnumerable<InvoiceApproval> approvals,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            InvoiceApproval approval,
            CancellationToken cancellationToken = default);

        // ----------- [Soft-Delete / Restore] -----------
        Task<bool> SoftDeleteAsync(
            Guid approvalId,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> approvalIds,
            CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(
            Guid approvalId,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> approvalIds,
            CancellationToken cancellationToken = default);

        Task<bool> RestoreByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        // ----------- [Hard-Delete] -----------
        Task<bool> DeleteAsync(
            Guid approvalId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> approvalIds,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------
        Task<InvoiceApproval?> GetByIdAsync(
            Guid approvalId,
            CancellationToken cancellationToken = default);

        Task<InvoiceApproval?> GetByIdWithDetailsAsync(
            Guid approvalId,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------
        Task<IReadOnlyList<InvoiceApproval>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceApproval>> GetByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceApproval>> GetByApproverAsync(
            Guid approverId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceApproval>> GetByDateRangeAsync(
            DateTime from,
            DateTime to,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceApproval>> GetByStatusAsync(
            InvoiceApprovalStatus status,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InvoiceApproval>> GetPendingAsync(
            CancellationToken cancellationToken = default);

        // ----------- [Existence & Integrity] -----------
        Task<bool> ExistsAsync(
            Guid approvalId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> AnyForApproverAsync(
            Guid approverId,
            CancellationToken cancellationToken = default);

        // ----------- [Counts & Statistics] -----------
        Task<int> CountAsync(
            Guid? invoiceId = null,
            Guid? approverId = null,
            InvoiceApprovalStatus? status = null,
            CancellationToken cancellationToken = default);

        Task<int> CountByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        Task<int> CountByApproverAsync(
            Guid approverId,
            CancellationToken cancellationToken = default);
  
    
    
    
    
    
    
    }
}