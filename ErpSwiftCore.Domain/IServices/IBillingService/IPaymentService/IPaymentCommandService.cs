using ErpSwiftCore.Domain.Entities.EntityBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IBillingService.IPaymentService
{
    public interface IPaymentCommandService
    {

        Task<Payment> AddPaymentAsync(Payment payment, CancellationToken cancellationToken = default);
        Task<IEnumerable<Payment>> AddPaymentsRangeAsync(IEnumerable<Payment> payments, CancellationToken cancellationToken = default);
        Task<Payment> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken = default);
        Task<bool> DeletePaymentAsync(Guid paymentId, CancellationToken cancellationToken = default);
        Task<bool> DeletePaymentsRangeAsync(IEnumerable<Guid> paymentIds, CancellationToken cancellationToken = default);
        Task<bool> MarkAsReconciledAsync(Guid paymentId, CancellationToken cancellationToken = default);
        Task<bool> UnmarkAsReconciledAsync(Guid paymentId, CancellationToken cancellationToken = default);

    }
}
