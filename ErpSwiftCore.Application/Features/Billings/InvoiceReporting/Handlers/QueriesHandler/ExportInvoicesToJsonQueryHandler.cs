using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using MediatR;
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.QueriesHandler
{
    public class ExportInvoicesToJsonQueryHandler : IRequestHandler<ExportInvoicesToJsonQuery, Stream>
    {
        private readonly IInvoiceReportingQueryService _svc;
        public ExportInvoicesToJsonQueryHandler(IInvoiceReportingQueryService svc)
        {
            _svc = svc;
        }
        public Task<Stream> Handle(ExportInvoicesToJsonQuery req, CancellationToken ct)
        {
            return _svc.ExportInvoicesToJsonAsync(ct);
        }
    }
}
