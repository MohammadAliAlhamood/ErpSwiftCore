using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using ErpSwiftCore.TenantManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.BillingService.InvoiceService
{
    public class InvoiceValidationService : IInvoiceValidationService
    {
 
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public InvoiceValidationService(IMultiTenantUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<bool> HasInvoiceLinesAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InvoiceLine.ExistsAsync(l => l.InvoiceId == invoiceId, cancellationToken);
        }
        public Task<bool> InvoiceApprovalExistsAsync(Guid approvalId, CancellationToken cancellationToken = default)
                 => _unitOfWork.InvoiceApproval.ExistsAsync(approvalId, cancellationToken);
        public async Task<bool> IsInvoiceLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId, CancellationToken cancellationToken = default)
        {
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            return invoice != null && invoice.CurrencyId == currencyId;
        }
        public Task<bool> PaymentExistsAsync(Guid paymentId, CancellationToken cancellationToken = default)
            => _unitOfWork.Payment.ExistsAsync(paymentId, cancellationToken);
        public async Task<bool> ValidateInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            if (!await InvoiceExistsAsync(invoiceId, cancellationToken))
                return false;

            var lines = await _unitOfWork.InvoiceLine.GetByInvoiceAsync(invoiceId, cancellationToken);
            if (!lines.Any()) return false;

            var totalCalculated = await CalculateInvoiceTotalAsync(invoiceId, cancellationToken);
            return totalCalculated > 0;
        }
        public async Task<decimal> CalculateInvoiceTotalAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            if (!await InvoiceExistsAsync(invoiceId, cancellationToken))
                throw new InvalidOperationException("Invoice not found.");

            // 1. Sum line subtotals
            var lines = await _unitOfWork.InvoiceLine.GetByInvoiceAsync(invoiceId, cancellationToken);
            var lineTotal = lines.Sum(l => (l.Quantity * l.UnitPrice) - l.Discount);

            // 2. Sum tax amounts
            var taxes = await _unitOfWork.InvoiceTax.GetByInvoiceAsync(invoiceId, cancellationToken);
            var taxTotal = taxes.Sum(t => t.TaxAmount);

            // 3. Sum discount amounts
            var discounts = await _unitOfWork.InvoiceDiscount.GetByInvoiceAsync(invoiceId, cancellationToken);
            var discountTotal = discounts.Sum(d => d.DiscountAmount);

            // 4. Return net total
            return lineTotal + taxTotal - discountTotal;
        }
        public Task<bool> InvoiceExistsAsync(Guid invoiceId, CancellationToken cancellationToken = default)
          => _unitOfWork.Invoice.ExistsAsync(invoiceId, cancellationToken);
        public Task<bool> InvoiceLineExistsAsync(Guid lineId, CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceLine.ExistsAsync(lineId, cancellationToken);

        public async Task<bool> CanModifyInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            // 1. Ensure the invoice exists
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken)
                          ?? throw new InvalidOperationException("Invoice not found.");

            // 2. Only allow modifications if:
            //    - Status is Draft (not yet issued), OR
            //    - Status is Issued but no payments have been applied
            if (invoice.InvoiceStatus == InvoiceStatus.Draft)
                return true;

            if (invoice.InvoiceStatus == InvoiceStatus.Issued && invoice.PaidAmount == 0m)
                return true;

            // 3. In any other status (PartiallyPaid, Paid, Overdue, Cancelled), disallow modifications
            return false;
        }

    }
}
