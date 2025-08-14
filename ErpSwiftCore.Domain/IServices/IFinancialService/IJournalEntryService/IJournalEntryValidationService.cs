using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService
{
    public interface IJournalEntryValidationService
    { 
        Task<bool> JournalEntryExistsByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default);
        Task<bool> JournalEntryLineExistsByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
    }
}