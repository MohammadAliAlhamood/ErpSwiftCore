using MediatR;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.QueriesHandler
{

    public class ExportInvoicesToExcelQueryHandler : IRequestHandler<ExportInvoicesToExcelQuery, Stream>
    {
        private readonly IInvoiceReportingQueryService _svc;
        public ExportInvoicesToExcelQueryHandler(IInvoiceReportingQueryService svc)
        {
            _svc = svc;
        }
        public Task<Stream> Handle(ExportInvoicesToExcelQuery req, CancellationToken ct)
        {
            return _svc.ExportInvoicesToExcelAsync(ct);
        }
    }
}