using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService
{
    public interface IInvoiceCommandService
    {
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default);
        Task<bool> DeleteInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<InvoiceLine> UpdateInvoiceLineAsync(InvoiceLine line, CancellationToken cancellationToken = default);
        Task<bool> DeleteInvoiceLineAsync(Guid lineId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllLinesOfInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<InvoiceApproval> AddInvoiceApprovalAsync(Guid invoiceId, InvoiceApproval approval, CancellationToken cancellationToken = default);
        Task<InvoiceApproval> UpdateInvoiceApprovalAsync(InvoiceApproval approval, CancellationToken cancellationToken = default);
        Task<bool> DeleteInvoiceApprovalAsync(Guid approvalId, CancellationToken cancellationToken = default);
        Task<Payment> AddPaymentAsync(Guid invoiceId, Payment payment, CancellationToken cancellationToken = default);
        Task<Payment> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken = default);
        Task<bool> DeletePaymentAsync(Guid paymentId, CancellationToken cancellationToken = default);
        Task<bool> ChangeInvoiceStatusAsync(Guid invoiceId, InvoiceStatus newStatus, CancellationToken cancellationToken = default);
        Task<bool> DeleteInvoicesRangeAsync(IEnumerable<Guid> invoiceIds, CancellationToken cancellationToken = default);
    }
}
