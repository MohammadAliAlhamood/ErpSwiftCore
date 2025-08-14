namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService
{
    public interface IInvoiceValidationService
    {
        Task<bool> ValidateInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<bool> InvoiceExistsAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<bool> InvoiceLineExistsAsync(Guid lineId, CancellationToken cancellationToken = default);
        Task<bool> InvoiceApprovalExistsAsync(Guid approvalId, CancellationToken cancellationToken = default);
        Task<bool> PaymentExistsAsync(Guid paymentId, CancellationToken cancellationToken = default);
        Task<bool> HasInvoiceLinesAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<bool> IsInvoiceLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId, CancellationToken cancellationToken = default);
        Task<decimal> CalculateInvoiceTotalAsync(Guid invoiceId, CancellationToken cancellationToken = default);


        Task<bool> CanModifyInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    }
}
