using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.FinancialService.JournalEntryService
{
    public class JournalEntryCommandService : IJournalEntryCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public JournalEntryCommandService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        public async Task<bool> DeleteJournalEntryAsync(Guid entryId, CancellationToken cancellationToken = default)
        {
            if (!await _unitOfWork.JournalEntry.ExistsByIdAsync(entryId, cancellationToken))
                return false;

            var deleted = await _unitOfWork.JournalEntry.DeleteAsync(entryId, cancellationToken);
            return deleted;
        }
        public async Task<bool> RestoreJournalEntryAsync(Guid entryId, CancellationToken cancellationToken = default)
        {
            var restored = await _unitOfWork.JournalEntry.RestoreAsync(entryId, cancellationToken);
            return restored;
        }
        public async Task<int> BatchDeleteJournalEntriesAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default)
        {
            if (entryIds == null) throw new ArgumentNullException(nameof(entryIds));

            var result = await _unitOfWork.JournalEntry.DeleteRangeAsync(entryIds, cancellationToken);
            await _unitOfWork.SaveAsync();
            // DeleteRangeAsync returns true/false; if true, assume all deleted
            return result ? entryIds.Count() : 0;
        }
        public async Task<int> BatchRestoreJournalEntriesAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default)
        {
            if (entryIds == null) throw new ArgumentNullException(nameof(entryIds));

            var result = await _unitOfWork.JournalEntry.RestoreRangeAsync(entryIds, cancellationToken);
            await _unitOfWork.SaveAsync();
            return result ? entryIds.Count() : 0;
        }
        public Task<bool> SoftDeleteJournalEntryAsync(Guid entryId, CancellationToken cancellationToken = default)
            => DeleteJournalEntryAsync(entryId, cancellationToken);
        public Task<int> SoftBatchDeleteJournalEntriesAsync(IEnumerable<Guid> entryIds, CancellationToken cancellationToken = default)
            => BatchDeleteJournalEntriesAsync(entryIds, cancellationToken);
 
    }
}
