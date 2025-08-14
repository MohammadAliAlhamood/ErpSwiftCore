using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.QueriesHandler
{
    public class ExportInvoicesToPdfQueryHandler
        : IRequestHandler<ExportInvoicesToPdfQuery, Stream>
    {
        private readonly IInvoiceReportingQueryService _svc;
        public ExportInvoicesToPdfQueryHandler(IInvoiceReportingQueryService svc)
        {
            _svc = svc;
        }

        public Task<Stream> Handle(
            ExportInvoicesToPdfQuery req,
            CancellationToken ct)
        {
            var invoices = req.InvoiceIds.Select(id => new Invoice { ID = id });
            return _svc.ExportInvoicesToPdfAsync(invoices, ct);
        }
    }


}
