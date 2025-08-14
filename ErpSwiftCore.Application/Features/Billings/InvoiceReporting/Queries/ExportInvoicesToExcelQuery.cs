using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>1. Export all invoices to Excel</summary>
    public class ExportInvoicesToExcelQuery : IRequest<Stream> { }
     
}