using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels; 
namespace ErpSwiftCore.Web.IService.IFinancialsService
{
    public interface IJournalEntryService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id, bool includeLines = false);
        Task<APIResponseDto?> GetByAccountAsync(Guid accountId);
        Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<APIResponseDto?> GetLinesAsync(Guid journalEntryId);
        Task<APIResponseDto?> GetSumDebitAsync(Guid accountId);
        Task<APIResponseDto?> GetSumCreditAsync(Guid accountId);
        Task<APIResponseDto?> GetTotalAsync(Guid accountId);
        Task<APIResponseDto?> ReconcileAsync(Guid accountId, DateTime from, DateTime to);

        // Validation Queries
        Task<APIResponseDto?> CheckReferenceExistsAsync(string referenceNumber);
        Task<APIResponseDto?> CheckLineExistsByAccountAsync(Guid accountId);

        // Commands
        Task<APIResponseDto?> DeleteAsync(Guid id);
        Task<APIResponseDto?> RestoreAsync(Guid id);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids); 
    }
}
