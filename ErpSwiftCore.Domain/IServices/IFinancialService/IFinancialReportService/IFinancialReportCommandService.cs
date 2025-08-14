using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService
{
    public interface IFinancialReportCommandService
    {
        Task<Guid> SaveReportAsync(CustomFinancialReportResult report, CancellationToken cancellationToken = default);
        Task<bool> DeleteReportAsync(Guid reportId,   CancellationToken cancellationToken = default);
        Task<bool> RestoreReportAsync(Guid reportId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> SaveReportsRangeAsync(IEnumerable<CustomFinancialReportResult> reports, CancellationToken cancellationToken = default);
        Task<int> DeleteReportsRangeAsync(IEnumerable<Guid> reportIds,  CancellationToken cancellationToken = default);
        Task<IEnumerable<IDictionary<string, object>>> RunCustomQueryAsync(string sqlOrLinqExpression, CancellationToken cancellationToken = default);

    }
}
