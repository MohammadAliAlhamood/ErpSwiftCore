using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace ErpSwiftCore.Persistence.Services.BillingService.InvoiceReportingService
{
    public class InvoiceImportExportCommandService : IInvoiceImportExportCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public InvoiceImportExportCommandService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ImportResult> ImportInvoicesFromCsvAsync(Stream csvStream, CancellationToken cancellationToken = default)
        {
            if (csvStream == null) throw new ArgumentNullException(nameof(csvStream));
            var result = new ImportResult();
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                using var reader = new StreamReader(csvStream, Encoding.UTF8, leaveOpen: true);
                string? headerLine = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(headerLine))
                    throw new InvalidOperationException("CSV has no header.");

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    result.TotalRows++;
                    var cols = ParseCsvLine(line);
                    try
                    {
                        var inv = new Invoice
                        {
                            InvoiceNumber = cols[1],
                            InvoiceDate = DateTime.ParseExact(cols[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            DueDate = string.IsNullOrWhiteSpace(cols[3]) ? null :  DateTime.ParseExact(cols[3], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            TotalAmount = decimal.Parse(cols[4], CultureInfo.InvariantCulture),
                            PaidAmount = decimal.Parse(cols[5], CultureInfo.InvariantCulture),
                            InvoiceStatus = Enum.Parse<InvoiceStatus>(cols[6]),
                            CurrencyId = Guid.Parse(cols[7])
                        };
                        var newId = await _unitOfWork.Invoice.CreateAsync(inv, cancellationToken);
                        inv.ID = newId;

                       

                        result.Successful++;
                    }
                    catch (Exception ex)
                    {
                        result.Failed++;
                        result.ErrorMessages.Add($"Line '{line}': {ex.Message}");
                    }
                }
                await tx.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task<ImportResult> ImportInvoicesFromExcelAsync(Stream excelStream, CancellationToken cancellationToken = default)
        {
            // Placeholder: treat as CSV
            return await ImportInvoicesFromCsvAsync(excelStream, cancellationToken);
        }

        public async Task<ImportResult> ImportInvoicesFromJsonAsync(Stream jsonStream, CancellationToken cancellationToken = default)
        {
            if (jsonStream == null) throw new ArgumentNullException(nameof(jsonStream));

            var result = new ImportResult();
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var invoices = await JsonSerializer.DeserializeAsync<List<Invoice>>(jsonStream, cancellationToken: cancellationToken)
                    ?? new List<Invoice>();

                foreach (var inv in invoices)
                {
                    result.TotalRows++;
                    try
                    {
                        inv.ID = Guid.Empty; // ensure EF creates new
                        var newId = await _unitOfWork.Invoice.CreateAsync(inv, cancellationToken);
                        inv.ID = newId;

                   

                        result.Successful++;
                    }
                    catch (Exception ex)
                    {
                        result.Failed++;
                        result.ErrorMessages.Add($"Invoice '{inv.InvoiceNumber}': {ex.Message}");
                    }
                }
                await tx.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
        private static string[] ParseCsvLine(string line)
        {
            var parts = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (c == '"' && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote
                    sb.Append('"');
                    i++;
                }
                else if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    parts.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }
            parts.Add(sb.ToString());
            return parts.ToArray();
        }

    }
}
