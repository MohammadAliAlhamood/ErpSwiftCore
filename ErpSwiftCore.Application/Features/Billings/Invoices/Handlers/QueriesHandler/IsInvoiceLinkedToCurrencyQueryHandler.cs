using ErpSwiftCore.Application.Features.Billings.Invoices.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Handlers.QueriesHandler
{
    // 16. Is invoice linked to currency
    public class IsInvoiceLinkedToCurrencyQueryHandler
        : BaseHandler<IsInvoiceLinkedToCurrencyQuery>
    {
        private readonly IInvoiceValidationService _svc;

        public IsInvoiceLinkedToCurrencyQueryHandler(
            IInvoiceValidationService svc,
            ILogger<BaseHandler<IsInvoiceLinkedToCurrencyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            IsInvoiceLinkedToCurrencyQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.IsInvoiceLinkedToCurrencyAsync(req.InvoiceId, req.CurrencyId, ct);
            return new { Linked = ok };
        }
    } 
}
