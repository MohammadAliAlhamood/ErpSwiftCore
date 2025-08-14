using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.FinancialService.FinancialReportService
{
    public class FinancialReportCommandService : IFinancialReportCommandService
    {
        public Task<bool> DeleteReportAsync(Guid reportId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteReportsRangeAsync(IEnumerable<Guid> reportIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreReportAsync(Guid reportId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDictionary<string, object>>> RunCustomQueryAsync(string sqlOrLinqExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveReportAsync(CustomFinancialReportResult report, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guid>> SaveReportsRangeAsync(IEnumerable<CustomFinancialReportResult> reports, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
