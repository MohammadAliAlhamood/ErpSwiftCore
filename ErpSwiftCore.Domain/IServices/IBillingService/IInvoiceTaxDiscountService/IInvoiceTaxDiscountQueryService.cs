using ErpSwiftCore.Domain.Entities.EntityBilling;
namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService
{
    public interface IInvoiceTaxDiscountQueryService
    {

        Task<IReadOnlyList<InvoiceTax>> GetTaxesByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<InvoiceDiscount?> GetDiscountByIdAsync(Guid discountId, CancellationToken cancellationToken = default);
        Task<InvoiceTax?> GetTaxByIdAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InvoiceDiscount>> GetDiscountsByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalTaxAmountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalDiscountAmountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<int> GetTaxesCountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<int> GetDiscountsCountAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    }
}
