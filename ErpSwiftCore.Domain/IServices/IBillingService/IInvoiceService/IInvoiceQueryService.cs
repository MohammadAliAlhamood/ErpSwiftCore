using ErpSwiftCore.Domain.Enums; 
using ErpSwiftCore.Domain.Entities.EntityBilling;
namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService
{
    public interface IInvoiceQueryService
    {
        Task<IReadOnlyList<Payment>> GetPaymentsByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InvoiceApproval>> GetInvoiceApprovalsAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InvoiceLine>> GetInvoiceLinesAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<InvoiceApproval?> GetInvoiceApprovalByIdAsync(Guid approvalId, CancellationToken cancellationToken = default);
        Task<Payment?> GetPaymentByIdAsync(Guid paymentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetInvoicesByIdsAsync(IEnumerable<Guid> invoiceIds, CancellationToken cancellationToken = default);
        Task<int> GetPaymentsCountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<int> GetInvoiceApprovalsCountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<int> GetInvoicesCountAsync(InvoiceStatus? status, CancellationToken cancellationToken = default);
        Task<int> GetInvoiceLinesCountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    }
}
