using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.FinancialService.JournalEntryService
{
    public class JournalEntryValidationService : IJournalEntryValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public JournalEntryValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> JournalEntryExistsByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.JournalEntry
                .ExistsByReferenceAsync(referenceNumber, cancellationToken);
        }
        public Task<bool> JournalEntryLineExistsByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.JournalEntryLine
                .ExistsByAccountAsync(accountId, cancellationToken);
        }

    }
}
