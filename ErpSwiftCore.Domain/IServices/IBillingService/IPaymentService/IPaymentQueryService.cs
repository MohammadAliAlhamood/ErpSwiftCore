using ErpSwiftCore.Domain.Entities.EntityBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IBillingService.IPaymentService
{
    public interface IPaymentQueryService
    {


        Task<Payment> RefundPaymentAsync(Guid originalPaymentId, decimal refundAmount, string reason, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Payment>> GetPaymentsByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Payment>> GetPaymentsByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
        Task<int> GetPaymentsCountAsync(DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalPaymentsAmountAsync(DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default);
        Task<Payment?> GetPaymentByIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

    }
}
