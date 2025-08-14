using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Billings.Invoices.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Handlers.QueriesHandler
{
    // 15. Payment exists
    public class PaymentExistsQueryHandler
        : BaseHandler<PaymentExistsQuery>
    {
        private readonly IInvoiceValidationService _svc;

        public PaymentExistsQueryHandler(
            IInvoiceValidationService svc,
            ILogger<BaseHandler<PaymentExistsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            PaymentExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.PaymentExistsAsync(req.PaymentId, ct);
            return new { Exists = ok };
        }
    }

}
