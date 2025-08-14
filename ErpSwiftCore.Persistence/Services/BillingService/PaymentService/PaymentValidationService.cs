using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IPaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.BillingService.PaymentService
{
    public class PaymentValidationService : IPaymentValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public PaymentValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ValidatePaymentAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            // simple existence check or additional business rules
            return await PaymentExistsAsync(paymentId, cancellationToken);
        }
        public async Task<bool> HasPaymentsForInvoiceAsync(
      Guid invoiceId,
      CancellationToken cancellationToken = default)
        {
            var payments = await _unitOfWork.Payment.GetByInvoiceAsync(invoiceId, cancellationToken);
            return payments.Any();
        }

        public Task<bool> PaymentExistsAsync(Guid paymentId, CancellationToken cancellationToken = default)
            => _unitOfWork.Payment.ExistsAsync(paymentId, cancellationToken);


    }
}
