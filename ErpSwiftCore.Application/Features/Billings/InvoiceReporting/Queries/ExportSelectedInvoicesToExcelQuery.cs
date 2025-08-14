using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>9. Export given invoices to Excel</summary>
    public class ExportSelectedInvoicesToExcelQuery : IRequest<Stream>
    {
        public IEnumerable<Guid> InvoiceIds { get; }
        public ExportSelectedInvoicesToExcelQuery(IEnumerable<Guid> invoiceIds) => InvoiceIds = invoiceIds;
    }


}
