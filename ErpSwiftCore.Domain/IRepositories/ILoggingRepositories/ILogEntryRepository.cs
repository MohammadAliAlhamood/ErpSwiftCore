using ErpSwiftCore.Domain.Entities.EntityLogging;
using ErpSwiftCore.Domain.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IRepositories.ILoggingRepositories
{
    /// <summary>
    /// Repository interface for managing LogEntry entities.
    /// يدعم تعدد المستأجرين، الحذف الناعم، الاسترجاع، الاستعلام المتقدم، التصفية، التصفح (paging)، والإحصاءات.
    /// </summary>
    public interface ILogEntryRepository : IMultiTenantRepository<LogEntry>
    {
        #region Soft Delete Operations

        Task<bool> SoftDeleteAsync(Guid logEntryId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default);

        #endregion

        #region CRUD Operations

        Task<Guid> CreateAsync(LogEntry logEntry, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<LogEntry> logEntries, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(LogEntry logEntry, CancellationToken cancellationToken = default);

        #endregion

        #region Delete / Archive / Restore

        Task<bool> DeleteAsync(Guid logEntryId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid logEntryId, CancellationToken cancellationToken = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Single Retrieval

        Task<LogEntry?> GetByIdAsync(Guid logEntryId, CancellationToken cancellationToken = default);
        Task<LogEntry?> GetWithDetailsAsync(Guid logEntryId, CancellationToken cancellationToken = default);

        #endregion

        #region Bulk / Advanced Retrieval

        Task<IReadOnlyList<LogEntry>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByLevelAsync(LogLevel level, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByEntityAsync(string entityName, string entityKey, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetBySourceAsync(string source, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByTagAsync(string tag, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByDateRangeAsync(DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetByFilterAsync(Expression<Func<LogEntry, bool>> filter, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<LogEntry>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> GetSoftDeletedByUserAsync(Guid userId, CancellationToken cancellationToken = default);

        #endregion

        #region Paging, Filtering & Stats

        Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedAsync(
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByCategoryAsync(
            string category,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByLevelAsync(
            LogLevel level,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByEntityAsync(
            string entityName,
            string entityKey,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByUserAsync(
            Guid userId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByDateRangeAsync(
            DateTime fromUtc,
            DateTime toUtc,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByCategoryAsync(string category, CancellationToken cancellationToken = default);
        Task<int> GetCountByLevelAsync(LogLevel level, CancellationToken cancellationToken = default);
        Task<int> GetCountByEntityAsync(string entityName, string entityKey, CancellationToken cancellationToken = default);
        Task<int> GetCountByUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<int> GetCountByDateRangeAsync(DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default);

        #endregion

        #region Search Operations

        Task<IReadOnlyList<LogEntry>> SearchByMessageAsync(string messageKeyword, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> SearchByDetailsAsync(string detailsKeyword, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> SearchBySourceAsync(string sourceKeyword, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogEntry>> SearchByTagAsync(string tagKeyword, CancellationToken cancellationToken = default);

        #endregion

        #region Bulk Delete

        Task<int> BulkDeleteAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default);

        #endregion

        #region Existence & Validation

        Task<bool> ExistsAsync(Guid logEntryId, CancellationToken cancellationToken = default);
        Task<bool> AnyByCategoryAsync(string category, CancellationToken cancellationToken = default);
        Task<bool> AnyByEntityAsync(string entityName, string entityKey, CancellationToken cancellationToken = default);

        #endregion
    }
}