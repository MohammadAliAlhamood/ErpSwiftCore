using ErpSwiftCore.Application.Features.Billings.Invoices.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Handlers.QueriesHandler
{
    // 14. InvoiceApproval exists
    public class InvoiceApprovalExistsQueryHandler  : BaseHandler<InvoiceApprovalExistsQuery>
    {
        private readonly IInvoiceValidationService _svc;
        public InvoiceApprovalExistsQueryHandler(IInvoiceValidationService svc, ILogger<BaseHandler<InvoiceApprovalExistsQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(InvoiceApprovalExistsQuery req, CancellationToken ct)
        {
            var ok = await _svc.InvoiceApprovalExistsAsync(req.ApprovalId, ct);
            return new { Exists = ok };
        }
    }

}
