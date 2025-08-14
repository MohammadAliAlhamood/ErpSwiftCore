using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    public class ExportInvoicesToCsvQuery : IRequest<Stream> { }
}
