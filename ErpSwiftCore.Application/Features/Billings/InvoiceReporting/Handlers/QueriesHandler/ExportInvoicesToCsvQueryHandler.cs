using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.QueriesHandler
{

    public class ExportInvoicesToCsvQueryHandler  : IRequestHandler<ExportInvoicesToCsvQuery, Stream>
    {
        private readonly IInvoiceReportingQueryService _svc;
        public ExportInvoicesToCsvQueryHandler(IInvoiceReportingQueryService svc)
        {
            _svc = svc;
        } 
        public Task<Stream> Handle(  ExportInvoicesToCsvQuery req, CancellationToken ct)
        {
            return _svc.ExportInvoicesToCsvAsync(ct);
        }
    } 
}
