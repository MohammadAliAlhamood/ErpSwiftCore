using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.BillingService.InvoiceTaxDiscountService
{
    public class InvoiceTaxDiscountQueryService : IInvoiceTaxDiscountQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public InvoiceTaxDiscountQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public Task<InvoiceDiscount?> GetDiscountByIdAsync(Guid discountId, CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceDiscount.GetByIdAsync(discountId, cancellationToken);

        public Task<IReadOnlyList<InvoiceDiscount>> GetDiscountsByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceDiscount.GetByInvoiceAsync(invoiceId, cancellationToken);

        public Task<int> GetDiscountsCountAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceDiscount.CountAsync(invoiceId, cancellationToken);
        public async Task<decimal> GetTotalDiscountAmountAsync(
         Guid invoiceId,
         CancellationToken cancellationToken = default)
        {
            var discounts = await _unitOfWork.InvoiceDiscount.GetByInvoiceAsync(invoiceId, cancellationToken);
            return discounts.Sum(d => d.DiscountAmount);
        }

        public async Task<decimal> GetTotalTaxAmountAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default)
        {
            var taxes = await _unitOfWork.InvoiceTax.GetByInvoiceAsync(invoiceId, cancellationToken);
            return taxes.Sum(t => t.TaxAmount);
        }

 
        public Task<InvoiceTax?> GetTaxByIdAsync(Guid taxId, CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceTax.GetByIdAsync(taxId, cancellationToken);

        public Task<IReadOnlyList<InvoiceTax>> GetTaxesByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceTax.GetByInvoiceAsync(invoiceId, cancellationToken);

        public Task<int> GetTaxesCountAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceTax.CountAsync(invoiceId, null, null, cancellationToken);

   





    }
}
