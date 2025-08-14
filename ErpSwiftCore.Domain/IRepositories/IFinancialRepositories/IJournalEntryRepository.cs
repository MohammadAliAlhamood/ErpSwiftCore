using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IFinancialRepositories
{
    public interface IJournalEntryRepository : IMultiTenantRepository<JournalEntry>
    {
        Task<Guid> CreateAsync(JournalEntry entry, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<JournalEntry> entries, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(JournalEntry entry, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid entryId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid entryId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
 
        Task<JournalEntry?> GetByIdAsync(Guid entryId,   CancellationToken cancellationToken = default);

        Task<JournalEntry?> GetByFilterAsync(Expression<Func<JournalEntry, bool>> filter,  CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntry>> GetAllAsync(Expression<Func<JournalEntry, bool>>? filter = null, CancellationToken cancellationToken = default);
 
        Task<IReadOnlyList<JournalEntry>> GetByIdsAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<JournalEntry> Entries, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<JournalEntry, bool>>? filter = null, CancellationToken cancellationToken = default);

        Task<JournalEntry?> GetByEntryNumberAsync(string entryNumber, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntry>> GetByDateAsync(DateTime entryDate, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntry>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntry>> GetUnpostedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntry>> GetPostedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntry>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(Guid entryId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByEntryNumberAsync(string entryNumber, CancellationToken cancellationToken = default);

        Task<bool> ExistsByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default);

        Task<int> CountByDateAsync(DateTime entryDate, CancellationToken cancellationToken = default);

        Task<int> CountUnpostedAsync(CancellationToken cancellationToken = default);

        Task<int> CountPostedAsync(CancellationToken cancellationToken = default);

        Task<Dictionary<DateTime, int>> GetDailyEntryCountsAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);

        Task<decimal> SumTotalByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
    }
}