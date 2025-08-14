using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IBillingService.IPaymentService
{
    public interface IPaymentValidationService
    {


        Task<bool> PaymentExistsAsync(Guid paymentId, CancellationToken cancellationToken = default);
        Task<bool> HasPaymentsForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<bool> ValidatePaymentAsync(Guid paymentId, CancellationToken cancellationToken = default);
    }
}