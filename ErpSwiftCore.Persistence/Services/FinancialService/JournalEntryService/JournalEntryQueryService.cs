using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.FinancialService.JournalEntryService
{
    public class JournalEntryQueryService : IJournalEntryQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public JournalEntryQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<JournalEntry?> GetJournalEntryByIdAsync(Guid entryId, bool includeLines = false, CancellationToken cancellationToken = default)
        {
            var entry = await _unitOfWork.JournalEntry.GetByIdAsync(entryId, cancellationToken: cancellationToken);
            if (entry != null && includeLines)
            {
                var lines = await _unitOfWork.JournalEntryLine
                    .GetByJournalEntryIdAsync(entryId, cancellationToken);
                entry.Lines = lines.ToList();
            }

            return entry;
        }
        public async Task<IReadOnlyList<JournalEntry>> GetJournalEntriesByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.JournalEntry.GetByAccountIdAsync(accountId, cancellationToken);
        }
        public async Task<IReadOnlyList<JournalEntry>> GetJournalEntriesByDateRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.JournalEntry.GetByDateRangeAsync(start, end, cancellationToken);
        }
        public async Task<IEnumerable<JournalEntryLine>> GetJournalEntryLinesByEntryAsync(Guid journalEntryId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.JournalEntryLine
                .GetByJournalEntryIdAsync(journalEntryId, cancellationToken);
        }
        public Task<decimal> GetJournalEntryLineDebitByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.JournalEntryLine
                .SumDebitByAccountAsync(accountId, cancellationToken);
        }
        public Task<decimal> GetJournalEntryLineCreditByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.JournalEntryLine
                .SumCreditByAccountAsync(accountId, cancellationToken);
        }
        public Task<decimal> GetJournalEntryTotalByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.JournalEntry
                .SumTotalByAccountAsync(accountId, cancellationToken);
        }
        public Task<decimal> ReconcileAccountAsync(Guid accountId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            // Returns net change over the period
            return _unitOfWork.JournalEntryLine.GetBalanceChangeByAccountAsync(accountId, from, to, cancellationToken);
        }
    }
}
