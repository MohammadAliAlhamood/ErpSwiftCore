using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.QueriesHandler
{
    // 6. Export selected invoices to CSV
    public class ExportSelectedInvoicesToCsvQueryHandler
        : IRequestHandler<ExportSelectedInvoicesToCsvQuery, Stream>
    {
        private readonly IInvoiceReportingQueryService _svc;
        public ExportSelectedInvoicesToCsvQueryHandler(IInvoiceReportingQueryService svc)
        {
            _svc = svc;
        }

        public Task<Stream> Handle(
            ExportSelectedInvoicesToCsvQuery req,
            CancellationToken ct)
        {
            var invoices = req.InvoiceIds.Select(id => new Invoice { ID = id });
            return _svc.ExportInvoicesToCsvAsync(invoices, ct);
        }
    }

}
