using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.BillingService.InvoiceTaxDiscountService
{
    public class InvoiceTaxDiscountValidationService : IInvoiceTaxDiscountValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public InvoiceTaxDiscountValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> HasDiscountsAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InvoiceDiscount.ExistsAsync(
                d => d.InvoiceId == invoiceId,
                cancellationToken);
        }
        public Task<bool> TaxExistsAsync(Guid taxId, CancellationToken cancellationToken = default)
           => _unitOfWork.InvoiceTax.ExistsAsync(taxId, cancellationToken);
        public Task<bool> DiscountExistsAsync(Guid discountId, CancellationToken cancellationToken = default)
    => _unitOfWork.InvoiceDiscount.ExistsAsync(discountId, cancellationToken);
        public async Task<bool> HasTaxesAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InvoiceTax.ExistsAsync(
                t => t.InvoiceId == invoiceId,
                cancellationToken);
        }
        public async Task<bool> IsInvoiceLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId, CancellationToken cancellationToken = default)
        {
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            return invoice != null && invoice.CurrencyId == currencyId;
        }
        public async Task<bool> ValidateTaxAndDiscountAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            // Example rule: total taxes + discounts cannot exceed invoice total
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null) return false;

            var taxes = await _unitOfWork.InvoiceTax.GetByInvoiceAsync(invoiceId, cancellationToken);
            var discounts = await _unitOfWork.InvoiceDiscount.GetByInvoiceAsync(invoiceId, cancellationToken);

            var totalTaxes = taxes.Sum(t => t.TaxAmount);
            var totalDiscounts = discounts.Sum(d => d.DiscountAmount);

            return totalTaxes + totalDiscounts <= invoice.TotalAmount;
        }

    }
}
