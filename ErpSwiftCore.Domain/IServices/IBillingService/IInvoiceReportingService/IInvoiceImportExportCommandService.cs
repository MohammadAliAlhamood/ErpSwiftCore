using ErpSwiftCore.Domain.Entities.EntityBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService
{
    public interface IInvoiceImportExportCommandService
    {
        Task<ImportResult> ImportInvoicesFromExcelAsync(Stream excelStream, CancellationToken cancellationToken = default);
        Task<ImportResult> ImportInvoicesFromCsvAsync(Stream csvStream, CancellationToken cancellationToken = default);
        Task<ImportResult> ImportInvoicesFromJsonAsync(Stream jsonStream, CancellationToken cancellationToken = default);
    }
}