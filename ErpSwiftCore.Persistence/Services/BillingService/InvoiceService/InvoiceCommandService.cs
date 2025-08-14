using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using ErpSwiftCore.TenantManagement.Interfaces;

namespace ErpSwiftCore.Persistence.Services.BillingService.InvoiceService
{
    public class InvoiceCommandService : IInvoiceCommandService
    {
        private readonly IUnitOfWork _uok;
        private readonly ITenantProvider _tenantProvider;
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private static Guid _TenantID;
        private readonly IInvoiceValidationService _validation;
        public InvoiceCommandService(IMultiTenantUnitOfWork unitOfWork, IUnitOfWork uok, ITenantProvider tenantProvider, IInvoiceValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _uok = uok;
            _tenantProvider = tenantProvider;
            _TenantID = _tenantProvider.GetTenantId();
            _validation = validation;
        }
        public async Task<Payment> AddPaymentAsync(Guid invoiceId, Payment payment, CancellationToken cancellationToken = default)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            if (!await _validation.InvoiceExistsAsync(invoiceId, cancellationToken))
                throw new InvalidOperationException("Invoice not found.");

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken)
                          ?? throw new InvalidOperationException("Invoice not found.");

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // 1. Persist payment
                payment.InvoiceId = invoiceId;
                payment.ID = await _unitOfWork.Payment.CreateAsync(payment, cancellationToken);

                // 2. Update invoice status
                invoice.PaidAmount += payment.PaymentAmount;
                invoice.InvoiceStatus = invoice.PaidAmount >= invoice.TotalAmount
                    ? InvoiceStatus.Paid
                    : InvoiceStatus.PartiallyPaid;
                await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);

                // 3. Create and post journal
                var entryNumber = $"PMT-{payment.ID:D8}";
                var description = $"Payment received for Invoice {invoice.InvoiceNumber}";
                var journalId = await CreateJournalAsync(entryNumber, description, cancellationToken);

                // 4a. Debit Cash/Bank
                var cashAccountId = await GetCashAccountIdAsync(cancellationToken);
                await AddJournalLineAsync(journalId, cashAccountId, payment.PaymentAmount, true, cancellationToken);

                // 4b. Credit AR
                var arAccountId = await GetAccountsReceivableAccountIdAsync(invoice, cancellationToken);
                await AddJournalLineAsync(journalId, arAccountId, payment.PaymentAmount, false, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return payment;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<InvoiceApproval> AddInvoiceApprovalAsync(Guid invoiceId, InvoiceApproval approval, CancellationToken cancellationToken = default)
        {
            if (approval == null) throw new ArgumentNullException(nameof(approval));
            if (!await _validation.InvoiceExistsAsync(invoiceId, cancellationToken))
                throw new InvalidOperationException("Invoice not found.");

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                approval.InvoiceId = invoiceId;
                approval.ID = await _unitOfWork.InvoiceApproval.CreateAsync(approval, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return approval;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeleteInvoiceApprovalAsync(Guid approvalId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.InvoiceApprovalExistsAsync(approvalId, cancellationToken))
                return false;

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.InvoiceApproval.GetByIdAsync(approvalId, cancellationToken);
                JsonSerializer.Serialize(existing);

                var success = await _unitOfWork.InvoiceApproval.DeleteAsync(approvalId, cancellationToken);
                if (!success) throw new InvalidOperationException("Failed to delete InvoiceApproval.");

                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeleteInvoiceLineAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.InvoiceLineExistsAsync(lineId, cancellationToken))
                return false;

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existingLine = await _unitOfWork.InvoiceLine.GetByIdAsync(lineId, cancellationToken)
                                     ?? throw new InvalidOperationException("InvoiceLine not found");
                var invoiceId = existingLine.InvoiceId;

                var deleted = await _unitOfWork.InvoiceLine.DeleteAsync(lineId, cancellationToken);
                if (!deleted) throw new InvalidOperationException("Failed to delete InvoiceLine.");

                var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
                if (invoice != null)
                {
                    var lineAmount = (existingLine.Quantity * existingLine.UnitPrice) - existingLine.Discount;
                    invoice.TotalAmount -= lineAmount;
                    invoice.PaidAmount = Math.Min(invoice.PaidAmount, invoice.TotalAmount);
                    invoice.InvoiceStatus = invoice.PaidAmount >= invoice.TotalAmount
                        ? InvoiceStatus.Paid
                        : invoice.PaidAmount > 0
                            ? InvoiceStatus.PartiallyPaid
                            : InvoiceStatus.Issued;
                    await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                }

                var entryNumber = $"REV-LINE-{lineId:D8}";
                var description = $"Reversal of InvoiceLine {lineId} deletion";
                var journalId = await CreateJournalAsync(entryNumber, description, cancellationToken);

                var revAccId = await GetRevenueAccountIdAsync(cancellationToken);
                await AddJournalLineAsync(journalId, revAccId, existingLine.Quantity * existingLine.UnitPrice, false, cancellationToken);

                if (existingLine.Discount > 0)
                {
                    var discAccId = await GetDiscountsAccountIdAsync(cancellationToken);
                    await AddJournalLineAsync(journalId, discAccId, existingLine.Discount, false, cancellationToken);
                }

                var arAccId = await GetAccountsReceivableAccountIdAsync(invoice, cancellationToken);
                await AddJournalLineAsync(journalId, arAccId, (existingLine.Quantity * existingLine.UnitPrice) - existingLine.Discount, true, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeleteAllLinesOfInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.InvoiceExistsAsync(invoiceId, cancellationToken))
                return false;

            var lines = await _unitOfWork.InvoiceLine.GetByInvoiceAsync(invoiceId, cancellationToken);
            if (!lines.Any()) return true;

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var line in lines)
                {
                    await _unitOfWork.InvoiceLine.DeleteAsync(line.ID, cancellationToken);

                    var inv = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
                    if (inv != null)
                    {
                        var amt = (line.Quantity * line.UnitPrice) - line.Discount;
                        inv.TotalAmount -= amt;
                        inv.PaidAmount = Math.Min(inv.PaidAmount, inv.TotalAmount);
                        inv.InvoiceStatus = inv.PaidAmount >= inv.TotalAmount
                            ? InvoiceStatus.Paid
                            : inv.PaidAmount > 0
                                ? InvoiceStatus.PartiallyPaid
                                : InvoiceStatus.Issued;
                        await _unitOfWork.Invoice.UpdateAsync(inv, cancellationToken);
                    }

                    var entryNumber = $"REV-LINE-{line.ID:D8}";
                    var description = $"Reversal of InvoiceLine {line.ID} deletion (bulk)";
                    var journalId = await CreateJournalAsync(entryNumber, description, cancellationToken);

                    var revAccId = await GetRevenueAccountIdAsync(cancellationToken);
                    await AddJournalLineAsync(journalId, revAccId, line.Quantity * line.UnitPrice, false, cancellationToken);

                    if (line.Discount > 0)
                    {
                        var discAccId = await GetDiscountsAccountIdAsync(cancellationToken);
                        await AddJournalLineAsync(journalId, discAccId, line.Discount, false, cancellationToken);
                    }

                    var arAccId = await GetAccountsReceivableAccountIdAsync(inv, cancellationToken);
                    await AddJournalLineAsync(journalId, arAccId, (line.Quantity * line.UnitPrice) - line.Discount, true, cancellationToken);
                }

                await tx.CommitAsync(cancellationToken);
                return true;
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

            var payment = await _unitOfWork.Payment.GetByIdAsync(paymentId, cancellationToken);
            if (payment == null) return false;

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(payment.InvoiceId, cancellationToken);
            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                if (invoice != null)
                {
                    invoice.PaidAmount -= payment.PaymentAmount;
                    invoice.InvoiceStatus = invoice.PaidAmount <= 0
                        ? InvoiceStatus.Issued
                        : invoice.PaidAmount < invoice.TotalAmount
                            ? InvoiceStatus.PartiallyPaid
                            : InvoiceStatus.Paid;
                    await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                }

                var deleted = await _unitOfWork.Payment.DeleteAsync(paymentId, cancellationToken);
                if (!deleted) throw new InvalidOperationException("Failed to delete Payment.");

                var entryNumber = $"REV-PMT-{paymentId:D8}";
                var description = $"Reversal of Payment {paymentId} deletion";
                var journalId = await CreateJournalAsync(entryNumber, description, cancellationToken);

                var cashAccId = await GetCashAccountIdAsync(cancellationToken);
                await AddJournalLineAsync(journalId, cashAccId, payment.PaymentAmount, false, cancellationToken);

                var arAccId = await GetAccountsReceivableAccountIdAsync(invoice, cancellationToken);
                await AddJournalLineAsync(journalId, arAccId, payment.PaymentAmount, true, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeleteInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.InvoiceExistsAsync(invoiceId, cancellationToken))
                return false;

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
                var lines = await _unitOfWork.InvoiceLine.GetByInvoiceAsync(invoiceId, cancellationToken);
                var approvals = await _unitOfWork.InvoiceApproval.GetByInvoiceAsync(invoiceId, cancellationToken);
                var payments = await _unitOfWork.Payment.GetByInvoiceAsync(invoiceId, cancellationToken);

                var totalLines = lines.Sum(l => (l.Quantity * l.UnitPrice) - l.Discount);
                var totalTax = (await _unitOfWork.InvoiceTax.GetByInvoiceAsync(invoiceId, cancellationToken)).Sum(t => t.TaxAmount);
                var totalDisc = (await _unitOfWork.InvoiceDiscount.GetByInvoiceAsync(invoiceId, cancellationToken)).Sum(d => d.DiscountAmount);
                var totalPmt = payments.Sum(p => p.PaymentAmount);

                var entryNumber = $"REV-INV-{invoiceId:D8}";
                var description = $"Reversal of Invoice {invoice?.InvoiceNumber} deletion";
                var journalId = await CreateJournalAsync(entryNumber, description, cancellationToken);

                if (totalLines > 0) await AddJournalLineAsync(journalId, await GetRevenueAccountIdAsync(cancellationToken), totalLines, true, cancellationToken);
                if (totalTax > 0) await AddJournalLineAsync(journalId, await GetTaxPayableAccountIdAsync(cancellationToken), totalTax, true, cancellationToken);
                if (totalDisc > 0) await AddJournalLineAsync(journalId, await GetDiscountsAccountIdAsync(cancellationToken), totalDisc, false, cancellationToken);

                var arAcc = await GetAccountsReceivableAccountIdAsync(invoice, cancellationToken);
                var netRec = totalLines + totalTax - totalDisc;
                if (netRec > 0) await AddJournalLineAsync(journalId, arAcc, netRec, false, cancellationToken);

                if (totalPmt > 0)
                {
                    var cashAcc = await GetCashAccountIdAsync(cancellationToken);
                    await AddJournalLineAsync(journalId, cashAcc, totalPmt, false, cancellationToken);
                    await AddJournalLineAsync(journalId, arAcc, totalPmt, true, cancellationToken);
                }

                foreach (var l in lines) await _unitOfWork.InvoiceLine.DeleteAsync(l.ID, cancellationToken);
                foreach (var a in approvals) await _unitOfWork.InvoiceApproval.DeleteAsync(a.ID, cancellationToken);
                foreach (var p in payments) await _unitOfWork.Payment.DeleteAsync(p.ID, cancellationToken);

                var success = await _unitOfWork.Invoice.DeleteAsync(invoiceId, cancellationToken);
                if (!success) throw new InvalidOperationException("Failed to delete Invoice.");

                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeleteInvoicesRangeAsync(IEnumerable<Guid> invoiceIds, CancellationToken cancellationToken = default)
        {
            if (invoiceIds == null) throw new ArgumentNullException(nameof(invoiceIds));
            var ids = invoiceIds.ToList();
            var anyDeleted = false;

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var id in ids)
                {
                    if (!await _validation.InvoiceExistsAsync(id, cancellationToken)) continue;
                    if (await DeleteInvoiceAsync(id, cancellationToken)) anyDeleted = true;
                }
                await tx.CommitAsync(cancellationToken);
                return anyDeleted;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> ChangeInvoiceStatusAsync(Guid invoiceId, InvoiceStatus newStatus, CancellationToken cancellationToken = default)
        {
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null) return false;

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                invoice.InvoiceStatus = newStatus;
                var success = await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                await tx.CommitAsync(cancellationToken);
                return success;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<InvoiceLine> UpdateInvoiceLineAsync(InvoiceLine line, CancellationToken cancellationToken = default)
        {
            if (line == null) throw new ArgumentNullException(nameof(line));
            if (!await _validation.InvoiceLineExistsAsync(line.ID, cancellationToken))
                throw new InvalidOperationException("InvoiceLine not found.");

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.InvoiceLine.GetByIdAsync(line.ID, cancellationToken)
                                  ?? throw new InvalidOperationException("InvoiceLine not found.");
                var oldAmount = (existing.Quantity * existing.UnitPrice) - existing.Discount;
                var newAmount = (line.Quantity * line.UnitPrice) - line.Discount;
                var delta = newAmount - oldAmount;

                await _unitOfWork.InvoiceLine.UpdateAsync(line, cancellationToken);

                var invoice = await _unitOfWork.Invoice.GetByIdAsync(existing.InvoiceId, cancellationToken);
                if (invoice != null && delta != 0)
                {
                    invoice.TotalAmount += delta;
                    invoice.PaidAmount = Math.Min(invoice.PaidAmount, invoice.TotalAmount);
                    invoice.InvoiceStatus = invoice.PaidAmount >= invoice.TotalAmount
                        ? InvoiceStatus.Paid
                        : invoice.PaidAmount > 0
                            ? InvoiceStatus.PartiallyPaid
                            : InvoiceStatus.Issued;
                    await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);

                    var entryNumber = $"ADJ-LINE-{line.ID:D8}";
                    var description = $"Adjustment for InvoiceLine {line.ID}";
                    var journalId = await CreateJournalAsync(entryNumber, description, cancellationToken);

                    var arAcc = await GetAccountsReceivableAccountIdAsync(invoice, cancellationToken);
                    var revAcc = await GetRevenueAccountIdAsync(cancellationToken);
                    var absDelta = Math.Abs(delta);

                    if (delta > 0)
                    {
                        await AddJournalLineAsync(journalId, arAcc, absDelta, true, cancellationToken);
                        await AddJournalLineAsync(journalId, revAcc, absDelta, false, cancellationToken);
                    }
                    else
                    {
                        await AddJournalLineAsync(journalId, revAcc, absDelta, true, cancellationToken);
                        await AddJournalLineAsync(journalId, arAcc, absDelta, false, cancellationToken);
                    }
                }

                await tx.CommitAsync(cancellationToken);
                return line;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));
            if (!await _validation.InvoiceExistsAsync(invoice.ID, cancellationToken))
                throw new InvalidOperationException("Invoice not found.");

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);
                await tx.CommitAsync(cancellationToken);
                return invoice;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<InvoiceApproval> UpdateInvoiceApprovalAsync(InvoiceApproval approval, CancellationToken cancellationToken = default)
        {
            if (approval == null) throw new ArgumentNullException(nameof(approval));
            if (!await _validation.InvoiceApprovalExistsAsync(approval.ID, cancellationToken))
                throw new InvalidOperationException("Approval not found.");

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.InvoiceApproval.UpdateAsync(approval, cancellationToken);
                await tx.CommitAsync(cancellationToken);
                return approval;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<Payment> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            if (!await _validation.PaymentExistsAsync(payment.ID, cancellationToken))
                throw new InvalidOperationException("Payment not found.");

            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.Payment.GetByIdAsync(payment.ID, cancellationToken)
                               ?? throw new InvalidOperationException("Payment not found.");

                await _unitOfWork.Payment.UpdateAsync(payment, cancellationToken);

                if (existing.PaymentAmount != payment.PaymentAmount)
                {
                    var invoice = await _unitOfWork.Invoice.GetByIdAsync(payment.InvoiceId, cancellationToken);
                    if (invoice != null)
                    {
                        var delta = payment.PaymentAmount - existing.PaymentAmount;
                        invoice.PaidAmount += delta;
                        invoice.InvoiceStatus = invoice.PaidAmount >= invoice.TotalAmount
                            ? InvoiceStatus.Paid
                            : invoice.PaidAmount <= 0
                                ? InvoiceStatus.Issued
                                : InvoiceStatus.PartiallyPaid;
                        await _unitOfWork.Invoice.UpdateAsync(invoice, cancellationToken);

                        var entryNumber = $"ADJ-PMT-{payment.ID:D8}";
                        var description = $"Adjustment for Payment {payment.ID}";
                        var journalId = await CreateJournalAsync(entryNumber, description, cancellationToken);

                        var cashAccId = await GetCashAccountIdAsync(cancellationToken);
                        var arAccId = await GetAccountsReceivableAccountIdAsync(invoice, cancellationToken);
                        var absDelta = Math.Abs(delta);

                        if (delta > 0)
                        {
                            await AddJournalLineAsync(journalId, cashAccId, absDelta, true, cancellationToken);
                            await AddJournalLineAsync(journalId, arAccId, absDelta, false, cancellationToken);
                        }
                        else
                        {
                            await AddJournalLineAsync(journalId, cashAccId, absDelta, false, cancellationToken);
                            await AddJournalLineAsync(journalId, arAccId, absDelta, true, cancellationToken);
                        }
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

        #region helper gets method
        private async Task<Guid> GetAccountsReceivableAccountIdAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            if (invoice.TenantID == null)
                throw new InvalidOperationException("Invoice is not associated with a tenant.");

            var companyId = invoice.TenantID;
            var settings = await _uok.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken)
                             ?? throw new InvalidOperationException($"CompanySettings not found for CompanyID {companyId}.");

            return settings.DefaultARAccountId != Guid.Empty
                ? settings.DefaultARAccountId
                : throw new InvalidOperationException("Default AR account is not configured for this company.");
        }
        private async Task<Guid> GetRevenueAccountIdAsync(CancellationToken cancellationToken)
        {
            if (_TenantID == Guid.Empty)
                throw new InvalidOperationException("No tenant context available.");

            var settings = await _uok.CompanySettings.GetByCompanyIdAsync(_TenantID, cancellationToken)
                            ?? throw new InvalidOperationException($"CompanySettings not found for CompanyID {_TenantID}.");

            return settings.DefaultRevenueAccountId != Guid.Empty
                ? settings.DefaultRevenueAccountId
                : throw new InvalidOperationException("Default Revenue account is not configured for this company.");
        }
        private async Task<Guid> GetTaxPayableAccountIdAsync(CancellationToken cancellationToken)
        {
            var settings = await _uok.CompanySettings.GetByCompanyIdAsync(_TenantID, cancellationToken)
                            ?? throw new InvalidOperationException($"CompanySettings not found for CompanyID {_TenantID}.");

            return settings.DefaultTaxPayableAccountId != Guid.Empty
                ? settings.DefaultTaxPayableAccountId
                : throw new InvalidOperationException("Default Tax Payable account is not configured for this company.");
        }
        private async Task<Guid> GetDiscountsAccountIdAsync(CancellationToken cancellationToken)
        {
            var settings = await _uok.CompanySettings.GetByCompanyIdAsync(_TenantID, cancellationToken)
                            ?? throw new InvalidOperationException($"CompanySettings not found for CompanyID {_TenantID}.");

            return settings.DefaultDiscountsAccountId != Guid.Empty
                ? settings.DefaultDiscountsAccountId
                : throw new InvalidOperationException("Default Discounts account is not configured for this company.");
        }
        private async Task<Guid> GetCashAccountIdAsync(CancellationToken cancellationToken)
        {
            var settings = await _uok.CompanySettings.GetByCompanyIdAsync(_TenantID, cancellationToken)
                            ?? throw new InvalidOperationException($"CompanySettings not found for CompanyID {_TenantID}.");

            return settings.DefaultCashAccountId != Guid.Empty
                ? settings.DefaultCashAccountId
                : await GetAccountsReceivableAccountIdAsync(
                    await _unitOfWork.Invoice.GetByIdAsync(Guid.Empty, cancellationToken),
                    cancellationToken);
        }
        private async Task<Guid> CreateJournalAsync(string entryNumber, string description, CancellationToken cancellationToken)
        {
            var journal = new JournalEntry
            {
                EntryDate = DateTime.UtcNow,
                EntryNumber = entryNumber,
                Description = description,
                IsPosted = true,
                PostedDate = DateTime.UtcNow
            };
            return await _unitOfWork.JournalEntry.CreateAsync(journal, cancellationToken);
        }
        private async Task AddJournalLineAsync(Guid journalId, Guid accountId, decimal amount, bool isDebit, CancellationToken cancellationToken)
        {
            var line = new JournalEntryLine
            {
                JournalEntryId = journalId,
                AccountId = accountId,
                Amount = amount,
                IsDebit = isDebit,
                CostCenterId = null
            };
            await _unitOfWork.JournalEntryLine.CreateAsync(line, cancellationToken);
        }

        #endregion

    }
}
