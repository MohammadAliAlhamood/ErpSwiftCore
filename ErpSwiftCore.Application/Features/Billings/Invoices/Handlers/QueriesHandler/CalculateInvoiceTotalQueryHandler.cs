using ErpSwiftCore.Application.Features.Billings.Invoices.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Handlers.QueriesHandler
{
    // 17. Calculate invoice total
    public class CalculateInvoiceTotalQueryHandler
        : BaseHandler<CalculateInvoiceTotalQuery>
    {
        private readonly IInvoiceValidationService _svc;

        public CalculateInvoiceTotalQueryHandler(
            IInvoiceValidationService svc,
            ILogger<BaseHandler<CalculateInvoiceTotalQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            CalculateInvoiceTotalQuery req,
            CancellationToken ct)
        {
            var total = await _svc.CalculateInvoiceTotalAsync(req.InvoiceId, ct);
            return new { Total = total };
        }
    }


}
