using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>8. Export given invoices to PDF</summary>
    public class ExportInvoicesToPdfQuery : IRequest<Stream>
    {
        public IEnumerable<Guid> InvoiceIds { get; }
        public ExportInvoicesToPdfQuery(IEnumerable<Guid> invoiceIds) => InvoiceIds = invoiceIds;
    }


}
