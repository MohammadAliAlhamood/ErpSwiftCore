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
    // 5. Export selected invoices to Excel
    public class ExportSelectedInvoicesToExcelQueryHandler
        : IRequestHandler<ExportSelectedInvoicesToExcelQuery, Stream>
    {
        private readonly IInvoiceReportingQueryService _svc;
        public ExportSelectedInvoicesToExcelQueryHandler(IInvoiceReportingQueryService svc)
        {
            _svc = svc;
        }

        public Task<Stream> Handle(
            ExportSelectedInvoicesToExcelQuery req,
            CancellationToken ct)
        {
            var invoices = req.InvoiceIds.Select(id => new Invoice { ID = id });
            return _svc.ExportInvoicesToExcelAsync(invoices, ct);
        }
    }

}
