using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    public class ExportInvoicesToJsonQuery : IRequest<Stream> { }
}
