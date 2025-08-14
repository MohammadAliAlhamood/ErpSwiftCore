using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService
{
    public interface IFinancialReportQueryService
    {
        Task<CustomFinancialReportResult?> GetReportByIdAsync(Guid reportId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<CustomFinancialReportResult>> GetAllReportsAsync(bool includeDeleted = false, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<CustomFinancialReportResult>> GetReportsByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
        Task<byte[]> ExportReportToExcelAsync(Guid reportId, CancellationToken cancellationToken = default);
        Task<string> ExportReportToCsvAsync(Guid reportId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<CustomFinancialReportResult>> GetRecentReportsAsync(int topCount, CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, int>> GetReportsCountByCompanyAsync(CancellationToken cancellationToken = default);
        Task<int> GetReportsCountAsync(CancellationToken cancellationToken = default);

    }
}
