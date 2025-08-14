using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService
{
    public interface IJournalEntryCommandService
    {
        Task<bool> DeleteJournalEntryAsync(Guid entryId, CancellationToken cancellationToken = default);
        Task<int> BatchDeleteJournalEntriesAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteJournalEntryAsync(Guid entryId, CancellationToken cancellationToken = default);
        Task<int> SoftBatchDeleteJournalEntriesAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreJournalEntryAsync(Guid entryId, CancellationToken cancellationToken = default);
        Task<int> BatchRestoreJournalEntriesAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default);

    }
}
