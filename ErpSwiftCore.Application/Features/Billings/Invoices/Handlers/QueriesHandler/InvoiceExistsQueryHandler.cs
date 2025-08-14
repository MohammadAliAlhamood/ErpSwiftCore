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
    // 12. Invoice exists
    public class InvoiceExistsQueryHandler
        : BaseHandler<InvoiceExistsQuery>
    {
        private readonly IInvoiceValidationService _svc;

        public InvoiceExistsQueryHandler(
            IInvoiceValidationService svc,
            ILogger<BaseHandler<InvoiceExistsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            InvoiceExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.InvoiceExistsAsync(req.InvoiceId, ct);
            return new { Exists = ok };
        }
    }


}
