using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService
{
    public interface IInvoiceReportingQueryService
    {
        Task<Stream> ExportInvoicesToExcelAsync(CancellationToken cancellationToken = default);
        Task<Stream> ExportInvoicesToCsvAsync(CancellationToken cancellationToken = default);
        Task<Stream> ExportInvoicesToJsonAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<Invoice>> GetInvoicesReportAsync(DateTime? fromDate = null, DateTime? toDate = null, InvoiceStatus? status = null, CancellationToken cancellationToken = default);
        Task<IDictionary<DateTime, decimal>> GetInvoiceTotalSummaryAsync(DateGrouping grouping, DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default);
        Task<InvoiceAgingReport> GetInvoiceAgingReportAsync(DateTime asOfDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<InvoiceTaxDiscountSummary>> GetTaxDiscountSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null, CancellationToken cancellationToken = default);
        Task<Stream> ExportInvoicesToPdfAsync(IEnumerable<Invoice> invoices, CancellationToken cancellationToken = default);
        Task<Stream> ExportInvoicesToExcelAsync(IEnumerable<Invoice> invoices, CancellationToken cancellationToken = default);
        Task<Stream> ExportInvoicesToCsvAsync(IEnumerable<Invoice> invoices, CancellationToken cancellationToken = default);
    }

    // إضافات مساعدة للنتائج:
    public enum DateGrouping
    {
        Daily,
        Monthly
    }

    public class InvoiceAgingReport
    {
        public decimal Current { get; set; }
        public decimal Days30 { get; set; }
        public decimal Days60 { get; set; }
        public decimal Days90 { get; set; }
        public decimal Over90 { get; set; }
    }

    public class InvoiceTaxDiscountSummary
    {
        public Guid InvoiceId { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
    }
}