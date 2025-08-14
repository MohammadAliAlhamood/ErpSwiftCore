using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>10. Export given invoices to CSV</summary>
    public class ExportSelectedInvoicesToCsvQuery : IRequest<Stream>
    {
        public IEnumerable<Guid> InvoiceIds { get; }
        public ExportSelectedInvoicesToCsvQuery(IEnumerable<Guid> invoiceIds) => InvoiceIds = invoiceIds;
    } 
}
