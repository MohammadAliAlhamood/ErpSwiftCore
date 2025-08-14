using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService
{
    public interface IJournalEntryQueryService
    {

        // -------------------- [JournalEntry Retrieval & Queries] --------------------
        Task<JournalEntry?> GetJournalEntryByIdAsync(Guid entryId, bool includeLines = false, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<JournalEntry>> GetJournalEntriesByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<JournalEntry>> GetJournalEntriesByDateRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default);
        Task<IEnumerable<JournalEntryLine>> GetJournalEntryLinesByEntryAsync(Guid journalEntryId, CancellationToken cancellationToken = default);
        Task<decimal> GetJournalEntryTotalByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<decimal> GetJournalEntryLineDebitByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<decimal> GetJournalEntryLineCreditByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<decimal> ReconcileAccountAsync(Guid accountId, DateTime from, DateTime to, CancellationToken cancellationToken = default);


    }
}
