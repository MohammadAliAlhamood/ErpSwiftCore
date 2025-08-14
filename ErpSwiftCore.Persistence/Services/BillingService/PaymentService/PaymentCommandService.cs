using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
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
    public class PaymentCommandService : IPaymentCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IPaymentValidationService _validation;
        public PaymentCommandService(IMultiTenantUnitOfWork unitOfWork, IPaymentValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }
        public async Task<Payment> AddPaymentAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            if (payment.PaymentAmount <= 0)
                throw new InvalidOperationException("Payment amount must be positive.");

            // ensure invoice exists
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(payment.InvoiceId, cancellationToken);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice '{payment.InvoiceId}' not found.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // 1. create payment
                var newId = await _unitOfWork.Payment.CreateAsync(payment, cancellationToken);
                payment.ID = newId;

                // 2. update invoice paid amount and status
                invoice.PaidAmount += payment.PaymentAmount;
                if (invoice.PaidAmount >= invoice.TotalAmount)
                    invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.Paid;
                await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);

                // 3. optionally: create journal entry (simplified)
                var journal = new JournalEntry
                {
                    EntryDate = DateTime.UtcNow,
                    EntryNumber = $"PMT-{newId}",
                    Description = $"Payment received for Invoice {invoice.ID}",
                    IsPosted = true,
                    PostedDate = DateTime.UtcNow
                };
                var journalId = await _unitOfWork.JournalEntry.CreateAsync(journal, cancellationToken);

                var line = new JournalEntryLine
                {
                    JournalEntryId = journalId,
                    AccountId = invoice.CurrencyId, // placeholder: assume account equals currency for demo
                    Amount = payment.PaymentAmount,
                    IsDebit = true
                };
                await _unitOfWork.JournalEntryLine.CreateAsync(line, cancellationToken);
 
                await tx.CommitAsync(cancellationToken);
                return payment;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<IEnumerable<Payment>> AddPaymentsRangeAsync(IEnumerable<Payment> payments, CancellationToken cancellationToken = default)
        {
            if (payments == null) throw new ArgumentNullException(nameof(payments));

            var toAdd = payments.ToList();
            var createdPayments = new List<Payment>();
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var payment in toAdd)
                {
                    if (payment.PaymentAmount <= 0)
                        throw new InvalidOperationException("Payment amount must be positive.");

                    var invoice = await _unitOfWork.Invoice.GetByIdAsync(payment.InvoiceId, cancellationToken);
                    if (invoice == null)
                        throw new InvalidOperationException($"Invoice '{payment.InvoiceId}' not found.");

                    // create each payment
                    var newId = await _unitOfWork.Payment.CreateAsync(payment, cancellationToken);
                    payment.ID = newId;
                    createdPayments.Add(payment);

                    // update invoice
                    invoice.PaidAmount += payment.PaymentAmount;
                    if (invoice.PaidAmount >= invoice.TotalAmount)
                        invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.Paid;
                    await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);

                    // journal entry (simplified)
                    var journal = new JournalEntry
                    {
                        EntryDate = DateTime.UtcNow,
                        EntryNumber = $"PMT-{newId}",
                        Description = $"Payment received for Invoice {invoice.ID}",
                        IsPosted = true,
                        PostedDate = DateTime.UtcNow
                    };
                    var journalId = await _unitOfWork.JournalEntry.CreateAsync(journal, cancellationToken);

                    var line = new JournalEntryLine
                    {
                        JournalEntryId = journalId,
                        AccountId = invoice.CurrencyId,
                        Amount = payment.PaymentAmount,
                        IsDebit = true
                    };
                    await _unitOfWork.JournalEntryLine.CreateAsync(line, cancellationToken);
 
                }
                 
                await tx.CommitAsync(cancellationToken);
                return createdPayments;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeletePaymentAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.PaymentExistsAsync(paymentId, cancellationToken))
                return false;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // fetch existing payment
                var payment = await _unitOfWork.Payment.GetByIdAsync(paymentId, cancellationToken);
                if (payment == null) return false;

                // fetch invoice and adjust paid amount
                var invoice = await _unitOfWork.Invoice.GetByIdAsync(payment.InvoiceId, cancellationToken);
                var oldJsonInvoice = invoice != null ? JsonSerializer.Serialize(invoice) : null;
                if (invoice != null)
                {
                    invoice.PaidAmount -= payment.PaymentAmount;
                    if (invoice.PaidAmount < invoice.TotalAmount)
                        invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.PartiallyPaid;
                    await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                }

                // delete payment
                var success = await _unitOfWork.Payment.DeleteAsync(paymentId, cancellationToken);
 
                await tx.CommitAsync(cancellationToken);
                return success;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeletePaymentsRangeAsync(IEnumerable<Guid> paymentIds, CancellationToken cancellationToken = default)
        {
            if (paymentIds == null) throw new ArgumentNullException(nameof(paymentIds));

            var ids = paymentIds.ToList();
            var deletedCount = 0;
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var id in ids)
                {
                    if (!await _validation.PaymentExistsAsync(id, cancellationToken))
                        continue;

                    var payment = await _unitOfWork.Payment.GetByIdAsync(id, cancellationToken);
                    if (payment == null) continue;

                    var invoice = await _unitOfWork.Invoice.GetByIdAsync(payment.InvoiceId, cancellationToken);
                    if (invoice != null)
                    {
                        invoice.PaidAmount -= payment.PaymentAmount;
                        if (invoice.PaidAmount < invoice.TotalAmount)
                            invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.PartiallyPaid;
                        await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                    }

                    var success = await _unitOfWork.Payment.DeleteAsync(id, cancellationToken);
                    if (!success) continue;

                
                    deletedCount++;
                }

                await _unitOfWork.SaveAsync();
                await tx.CommitAsync(cancellationToken);
                return deletedCount > 0;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }


        // -------------------- [Reconciliation] --------------------

        public async Task<bool> MarkAsReconciledAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default)
        {
            var payment = await _unitOfWork.Payment.GetByIdAsync(paymentId, cancellationToken);
            if (payment == null) return false;

            // assume a boolean flag IsReconciled exists on Payment
            payment.IsReconciled = true; // using IsRead as placeholder
            var success = await _unitOfWork.Payment.UpdateAsync(payment, cancellationToken);
 
            return success;
        }

        public async Task<bool> UnmarkAsReconciledAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default)
        {
            var payment = await _unitOfWork.Payment.GetByIdAsync(paymentId, cancellationToken);
            if (payment == null) return false;

            payment.IsReconciled = false; // using IsRead as placeholder
            var success = await _unitOfWork.Payment.UpdateAsync(payment, cancellationToken);

           
            return success;
        }

        // -------------------- [Update] --------------------

        public async Task<Payment> UpdatePaymentAsync(
            Payment payment,
            CancellationToken cancellationToken = default)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            if (!await _validation.PaymentExistsAsync(payment.ID, cancellationToken))
                throw new InvalidOperationException("Payment not found.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // fetch existing for audit
                var existing = await _unitOfWork.Payment.GetByIdAsync(payment.ID, cancellationToken);
                if (existing == null) throw new InvalidOperationException("Payment not found.");
                var oldJson = JsonSerializer.Serialize(existing);

                // update
                var success = await _unitOfWork.Payment.UpdateAsync(payment, cancellationToken);

                // if amount changed, adjust invoice
                if (existing.PaymentAmount != payment.PaymentAmount)
                {
                    var invoice = await _unitOfWork.Invoice.GetByIdAsync(payment.InvoiceId, cancellationToken);
                    if (invoice != null)
                    {
                        invoice.PaidAmount += payment.PaymentAmount - existing.PaymentAmount;
                        if (invoice.PaidAmount >= invoice.TotalAmount)
                            invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.Paid;
                        else if (invoice.PaidAmount <= 0)
                            invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.Draft;
                        else
                            invoice.InvoiceStatus = Domain.Enums.InvoiceStatus.PartiallyPaid;
                        await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                    }
                }

                
                await tx.CommitAsync(cancellationToken);
                return payment;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        // -------------------- [Validation & Refund] --------------------

    }
}