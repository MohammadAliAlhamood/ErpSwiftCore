namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService
{
    public interface IInvoiceTaxDiscountValidationService
    { 
        Task<bool> ValidateTaxAndDiscountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<bool> TaxExistsAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> DiscountExistsAsync(Guid discountId, CancellationToken cancellationToken = default);
        Task<bool> HasTaxesAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<bool> HasDiscountsAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<bool> IsInvoiceLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId, CancellationToken cancellationToken = default);
    }
}