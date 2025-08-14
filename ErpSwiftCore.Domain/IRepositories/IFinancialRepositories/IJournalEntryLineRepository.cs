using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IFinancialRepositories
{
    public interface IJournalEntryLineRepository : IMultiTenantRepository<JournalEntryLine>
    {
        Task<Guid> CreateAsync(JournalEntryLine line, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<JournalEntryLine> lines, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(JournalEntryLine line, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid lineId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid lineId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
 
        Task<JournalEntryLine?> GetByIdAsync(Guid lineId, bool tracked = false, CancellationToken cancellationToken = default);

        Task<JournalEntryLine?> GetByFilterAsync(Expression<Func<JournalEntryLine, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntryLine>> GetAllAsync(Expression<Func<JournalEntryLine, bool>>? filter = null, CancellationToken cancellationToken = default);
 
        Task<IReadOnlyList<JournalEntryLine>> GetSoftDeletedAsync(Expression<Func<JournalEntryLine, bool>>? filter = null, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntryLine>> GetByIdsAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<JournalEntryLine> Lines, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<JournalEntryLine, bool>>? filter = null, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntryLine>> GetByJournalEntryIdAsync(Guid journalEntryId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<JournalEntryLine>> SearchByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(Guid lineId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCostCenterAsync(Guid costCenterId, CancellationToken cancellationToken = default);

        Task<decimal> SumDebitByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<decimal> SumCreditByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<decimal> GetBalanceChangeByAccountAsync(Guid accountId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
    }
}