using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using System.Globalization;
using System.Text;
using System.Text.Json;
namespace ErpSwiftCore.Persistence.Services.BillingService.InvoiceReportingService
{
    public class InvoiceReportingQueryService : IInvoiceReportingQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public InvoiceReportingQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // -------------------- [Export to CSV] --------------------
        public async Task<Stream> ExportInvoicesToCsvAsync(
            IEnumerable<Invoice> invoices,
            CancellationToken cancellationToken = default)
        {
            if (invoices == null) throw new ArgumentNullException(nameof(invoices));

            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            {
                writer.WriteLine("ID,InvoiceNumber,InvoiceDate,DueDate,TotalAmount,PaidAmount,Status,CurrencyId");
                foreach (var inv in invoices)
                {
                    var line = string.Join(",",
                        inv.ID,
                        EscapeCsv(inv.InvoiceNumber),
                        inv.InvoiceDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        inv.DueDate.HasValue ? inv.DueDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "",
                        inv.TotalAmount.ToString(CultureInfo.InvariantCulture),
                        inv.PaidAmount.ToString(CultureInfo.InvariantCulture),
                        inv.InvoiceStatus,
                        inv.CurrencyId);
                    writer.WriteLine(line);
                }
                await writer.FlushAsync();
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        // -------------------- [Export to Excel] --------------------
        public async Task<Stream> ExportInvoicesToExcelAsync(
            IEnumerable<Invoice> invoices,
            CancellationToken cancellationToken = default)
        {
            // Placeholder: reuse CSV logic; consumer can open as .xlsx
            return await ExportInvoicesToCsvAsync(invoices, cancellationToken);
        }

        // -------------------- [Export to PDF] --------------------
        public Task<Stream> ExportInvoicesToPdfAsync(
            IEnumerable<Invoice> invoices,
            CancellationToken cancellationToken = default)
        {
            // Placeholder: return empty PDF stream
            return Task.FromResult<Stream>(new MemoryStream());
        }

        // -------------------- [Aging Report] --------------------
        public async Task<InvoiceAgingReport> GetInvoiceAgingReportAsync(
            DateTime asOfDate,
            CancellationToken cancellationToken = default)
        {
            var allInvoices = await _unitOfWork.Invoice.GetAllAsync(cancellationToken);

            decimal current = 0m, days30 = 0m, days60 = 0m, days90 = 0m, over90 = 0m;

            foreach (var inv in allInvoices)
            {
                var outstanding = inv.TotalAmount - inv.PaidAmount;
                if (outstanding <= 0) continue;

                if (!inv.DueDate.HasValue || inv.DueDate.Value.Date >= asOfDate.Date)
                {
                    current += outstanding;
                }
                else
                {
                    var daysOverdue = (asOfDate.Date - inv.DueDate.Value.Date).Days;
                    if (daysOverdue <= 30)
                        days30 += outstanding;
                    else if (daysOverdue <= 60)
                        days60 += outstanding;
                    else if (daysOverdue <= 90)
                        days90 += outstanding;
                    else
                        over90 += outstanding;
                }
            }

            return new InvoiceAgingReport
            {
                Current = current,
                Days30 = days30,
                Days60 = days60,
                Days90 = days90,
                Over90 = over90
            };
        }

        // -------------------- [Invoices Report] --------------------
        public async Task<IEnumerable<Invoice>> GetInvoicesReportAsync(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            InvoiceStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            var all = await _unitOfWork.Invoice.GetAllAsync(cancellationToken);
            var query = all.AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date >= fromDate.Value.Date);
            if (toDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date <= toDate.Value.Date);
            if (status.HasValue)
                query = query.Where(i => i.InvoiceStatus == status.Value);

            return query ;
        }

        // -------------------- [Total Summary] --------------------
        public async Task<IDictionary<DateTime, decimal>> GetInvoiceTotalSummaryAsync(
            DateGrouping grouping,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            var all = await _unitOfWork.Invoice.GetAllAsync(cancellationToken);
            var query = all.AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date >= fromDate.Value.Date);
            if (toDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date <= toDate.Value.Date);

            var grouped = grouping switch
            {
                DateGrouping.Daily => query
                    .GroupBy(i => i.InvoiceDate.Date)
                    .Select(g => new { Key = g.Key, Total = g.Sum(i => i.TotalAmount) }),

                DateGrouping.Monthly => query
                    .GroupBy(i => new DateTime(i.InvoiceDate.Year, i.InvoiceDate.Month, 1))
                    .Select(g => new { Key = g.Key, Total = g.Sum(i => i.TotalAmount) }),

                _ => throw new ArgumentOutOfRangeException(nameof(grouping))
            };

            return grouped.ToDictionary(x => x.Key, x => x.Total);
        }

        // -------------------- [Tax & Discount Summary] --------------------
        public async Task<IEnumerable<InvoiceTaxDiscountSummary>> GetTaxDiscountSummaryAsync(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            var invoices = await _unitOfWork.Invoice.GetAllAsync(cancellationToken);
            var query = invoices.AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date >= fromDate.Value.Date);
            if (toDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date <= toDate.Value.Date);

            var result = new List<InvoiceTaxDiscountSummary>();
            foreach (var inv in query)
            {
                var taxes = await _unitOfWork.InvoiceTax.GetByInvoiceAsync(inv.ID, cancellationToken);
                var discounts = await _unitOfWork.InvoiceDiscount.GetByInvoiceAsync(inv.ID, cancellationToken);

                var totalTax = taxes.Sum(t => t.TaxAmount);
                var totalDiscount = discounts.Sum(d => d.DiscountAmount);
                var netAmount = totalTax - totalDiscount;

                result.Add(new InvoiceTaxDiscountSummary
                {
                    InvoiceId = inv.ID,
                    TotalTax = totalTax,
                    TotalDiscount = totalDiscount,
                    NetAmount = netAmount
                });
            }

            return result;
        }

        // -------------------- [Export to CSV] --------------------
        public async Task<Stream> ExportInvoicesToCsvAsync(CancellationToken cancellationToken = default)
        {
            var invoices = await _unitOfWork.Invoice.GetAllAsync(cancellationToken);
            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            {
                writer.WriteLine("ID,InvoiceNumber,InvoiceDate,DueDate,TotalAmount,PaidAmount,Status,CurrencyId");
                foreach (var inv in invoices)
                {
                    var line = string.Join(",",
                        inv.ID,
                        EscapeCsv(inv.InvoiceNumber),
                        inv.InvoiceDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        inv.DueDate.HasValue ? inv.DueDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "",
                        inv.TotalAmount.ToString(CultureInfo.InvariantCulture),
                        inv.PaidAmount.ToString(CultureInfo.InvariantCulture),
                        inv.InvoiceStatus,
                        inv.CurrencyId);
                    writer.WriteLine(line);
                }
                await writer.FlushAsync();
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        // -------------------- [Export to Excel] --------------------
        public async Task<Stream> ExportInvoicesToExcelAsync(CancellationToken cancellationToken = default)
        {
            // Placeholder: reuse CSV logic; consumer opens as .xlsx
            return await ExportInvoicesToCsvAsync(cancellationToken);
        }

        // -------------------- [Export to JSON] --------------------
        public async Task<Stream> ExportInvoicesToJsonAsync(CancellationToken cancellationToken = default)
        {
            var invoices = await _unitOfWork.Invoice.GetAllAsync(cancellationToken);
            var memoryStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memoryStream, invoices,
                new JsonSerializerOptions { WriteIndented = true }, cancellationToken);
            memoryStream.Position = 0;
            return memoryStream;
        }
        // -------------------- [Helpers] --------------------
        private static string EscapeCsv(string? value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
    }
}