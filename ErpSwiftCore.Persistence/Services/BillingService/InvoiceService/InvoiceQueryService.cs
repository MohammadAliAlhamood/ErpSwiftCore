using ErpSwiftCore.Domain.Entities.EntityBilling;
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
    public class InvoiceQueryService : IInvoiceQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public InvoiceQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);

        public Task<IReadOnlyList<InvoiceLine>> GetInvoiceLinesAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceLine.GetByInvoiceAsync(invoiceId, cancellationToken);

        public async Task<int> GetInvoiceLinesCountAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InvoiceLine.CountAsync(invoiceId, null, cancellationToken);
        }

        public Task<IReadOnlyList<InvoiceApproval>> GetInvoiceApprovalsAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => _unitOfWork.InvoiceApproval.GetByInvoiceAsync(invoiceId, cancellationToken);

        public async Task<int> GetInvoiceApprovalsCountAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InvoiceApproval.CountAsync(invoiceId, null, null, cancellationToken);
        }

        public Task<IReadOnlyList<Payment>> GetPaymentsByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => _unitOfWork.Payment.GetByInvoiceAsync(invoiceId, cancellationToken);

        public async Task<int> GetPaymentsCountAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return (await _unitOfWork.Payment.GetByInvoiceAsync(invoiceId, cancellationToken)).Count;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByIdsAsync(IEnumerable<Guid> invoiceIds, CancellationToken cancellationToken = default)
        {
            var ids = invoiceIds.ToList();
            var result = new List<Invoice>();
            foreach (var id in ids)
            {
                var inv = await _unitOfWork.Invoice.GetByIdAsync(id, cancellationToken);
                if (inv != null) result.Add(inv);
            }
            return result;
        }

        public async Task<int> GetInvoicesCountAsync(InvoiceStatus? status, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Invoice.CountAsync(
                orderId: null,
                status: status,
                fromDate: null,
                toDate: null,
                cancellationToken: cancellationToken);
        }

     

        public async Task<InvoiceApproval?> GetInvoiceApprovalByIdAsync(Guid approvalId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.InvoiceApproval.GetByIdAsync(approvalId, cancellationToken);
        }

        public async Task<Payment?> GetPaymentByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Payment.GetByIdAsync(paymentId, cancellationToken);
        }

       

    
    }
}