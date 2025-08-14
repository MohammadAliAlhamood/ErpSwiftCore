using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IFinancialRepositories
{
    public interface ICustomFinancialReportResultRepository : IMultiTenantRepository<CustomFinancialReportResult>
    {
        Task<Guid> CreateAsync(CustomFinancialReportResult result, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CustomFinancialReportResult> results, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(CustomFinancialReportResult result, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid resultId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> resultIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid resultId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> resultIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
         
        Task<CustomFinancialReportResult?> GetByIdAsync(Guid resultId, bool tracked = false, CancellationToken cancellationToken = default);

        Task<CustomFinancialReportResult?> GetByFilterAsync(Expression<Func<CustomFinancialReportResult, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CustomFinancialReportResult>> GetAllAsync(Expression<Func<CustomFinancialReportResult, bool>>? filter = null, CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<CustomFinancialReportResult>> GetByIdsAsync(IEnumerable<Guid> resultIds, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<CustomFinancialReportResult> Results, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<CustomFinancialReportResult, bool>>? filter = null, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CustomFinancialReportResult>> SearchByReportNameAsync(string reportNameKeyword, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(Guid resultId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByReportNameAsync(string reportName, CancellationToken cancellationToken = default);

        Task<Dictionary<string, int>> GetResultCountByReportNameAsync(CancellationToken cancellationToken = default);

        Task<int> CountAllAsync(CancellationToken cancellationToken = default);
    }
}