using ErpSwiftCore.Domain.Entities.EntityBilling; 
namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService
{
    public interface IInvoiceTaxDiscountCommandService
    {
        Task<(IEnumerable<InvoiceTax> Taxes, IEnumerable<InvoiceDiscount> Discounts)> CreateTaxesAndDiscountsAsync(
            Guid invoiceId,
            IEnumerable<InvoiceTax> taxes,
            IEnumerable<InvoiceDiscount> discounts,
            CancellationToken cancellationToken = default);
        Task<(IEnumerable<InvoiceTax> UpdatedTaxes, IEnumerable<InvoiceDiscount> UpdatedDiscounts)> UpdateTaxesAndDiscountsAsync(
                Guid invoiceId,
                IEnumerable<InvoiceTax>? taxesToAdd = null,
                IEnumerable<InvoiceTax>? taxesToUpdate = null,
                IEnumerable<Guid>? taxIdsToDelete = null,
                IEnumerable<InvoiceDiscount>? discountsToAdd = null,
                IEnumerable<InvoiceDiscount>? discountsToUpdate = null,
                IEnumerable<Guid>? discountIdsToDelete = null,
                CancellationToken cancellationToken = default);
        Task<InvoiceTax> AddTaxAsync(Guid invoiceId, InvoiceTax tax, CancellationToken cancellationToken = default);
        Task<IEnumerable<InvoiceTax>> AddTaxesAsync(Guid invoiceId, IEnumerable<InvoiceTax> taxes, CancellationToken cancellationToken = default);
        Task<InvoiceTax> UpdateTaxAsync(InvoiceTax tax, CancellationToken cancellationToken = default);
        Task<bool> DeleteTaxAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllTaxesOfInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<InvoiceDiscount> AddDiscountAsync(Guid invoiceId, InvoiceDiscount discount, CancellationToken cancellationToken = default);
        Task<IEnumerable<InvoiceDiscount>> AddDiscountsAsync(Guid invoiceId, IEnumerable<InvoiceDiscount> discounts, CancellationToken cancellationToken = default);
        Task<InvoiceDiscount> UpdateDiscountAsync(InvoiceDiscount discount, CancellationToken cancellationToken = default);
        Task<bool> DeleteDiscountAsync(Guid discountId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllDiscountsOfInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    }
}
