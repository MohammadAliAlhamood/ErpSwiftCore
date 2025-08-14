using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.FinancialService.FinancialReportService
{
    public class FinancialReportQueryService : IFinancialReportQueryService
    {
        public Task<string> ExportReportToCsvAsync(Guid reportId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ExportReportToExcelAsync(Guid reportId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CustomFinancialReportResult>> GetAllReportsAsync(bool includeDeleted = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<(IReadOnlyList<CustomFinancialReportResult> Reports, int TotalCount)> GetPagedReportsByCompanyAsync(Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CustomFinancialReportResult>> GetRecentReportsAsync(int topCount, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CustomFinancialReportResult?> GetReportByIdAsync(Guid reportId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CustomFinancialReportResult>> GetReportsByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CustomFinancialReportResult>> GetReportsByFilterAsync(Expression<Func<CustomFinancialReportResult, bool>> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetReportsCountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<Guid, int>> GetReportsCountByCompanyAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CustomFinancialReportResult>> SearchReportsAsync(string keyword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
