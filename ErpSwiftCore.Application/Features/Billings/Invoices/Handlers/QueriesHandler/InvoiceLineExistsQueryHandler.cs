using ErpSwiftCore.Application.Features.Billings.Invoices.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Handlers.QueriesHandler
{
    // 13. InvoiceLine exists
    public class InvoiceLineExistsQueryHandler
        : BaseHandler<InvoiceLineExistsQuery>
    {
        private readonly IInvoiceValidationService _svc;

        public InvoiceLineExistsQueryHandler(
            IInvoiceValidationService svc,
            ILogger<BaseHandler<InvoiceLineExistsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            InvoiceLineExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.InvoiceLineExistsAsync(req.LineId, ct);
            return new { Exists = ok };
        }
    }

}
