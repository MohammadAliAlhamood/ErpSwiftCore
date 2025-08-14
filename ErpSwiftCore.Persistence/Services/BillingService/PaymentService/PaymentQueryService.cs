using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IPaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.BillingService.PaymentService
{
    public class PaymentQueryService : IPaymentQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public PaymentQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // -------------------- [Get & Exists] --------------------
        public Task<Payment?> GetPaymentByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
            => _unitOfWork.Payment.GetByIdAsync(paymentId, cancellationToken);
        public Task<IReadOnlyList<Payment>> GetPaymentsByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
            => _unitOfWork.Payment.GetByDateRangeAsync(fromDate, toDate, cancellationToken);
        public Task<IReadOnlyList<Payment>> GetPaymentsByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => _unitOfWork.Payment.GetByInvoiceAsync(invoiceId, cancellationToken);
        public async Task<int> GetPaymentsCountAsync(DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Payment.CountAsync(
                invoiceId: null,
                fromDate: fromDate,
                toDate: toDate,
                minAmount: null,
                maxAmount: null,
                cancellationToken: cancellationToken);
        }
       
        public async Task<decimal> GetTotalPaymentsAmountAsync(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.Payment.GetByDateRangeAsync(
                fromDate ?? DateTime.MinValue,
                toDate ?? DateTime.MaxValue,
                cancellationToken);
            return list.Sum(p => p.PaymentAmount);
        }

        public async Task<Payment> RefundPaymentAsync(Guid originalPaymentId, decimal refundAmount, string reason, CancellationToken cancellationToken = default)
        {
            if (refundAmount <= 0)
                throw new InvalidOperationException("Refund amount must be positive.");

            var original = await _unitOfWork.Payment.GetByIdAsync(originalPaymentId, cancellationToken);
            if (original == null)
                throw new InvalidOperationException("Original payment not found.");
            if (refundAmount > original.PaymentAmount)
                throw new InvalidOperationException("Refund amount cannot exceed original payment.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // create refund as negative payment
                var refund = new Payment
                {
                    InvoiceId = original.InvoiceId,
                    PaymentAmount = -refundAmount,
                    PaymentDate = DateTime.UtcNow
                };
                var newId = await _unitOfWork.Payment.CreateAsync(refund, cancellationToken);
                refund.ID = newId;

                // adjust invoice
                var invoice = await _unitOfWork.Invoice.GetByIdAsync(original.InvoiceId, cancellationToken);
                if (invoice != null)
                {
                    invoice.PaidAmount -= refundAmount;
                    if (invoice.PaidAmount < invoice.TotalAmount)
                        invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.PartiallyPaid;
                    await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                }

          
                await tx.CommitAsync(cancellationToken);
                return refund;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

    }
}
