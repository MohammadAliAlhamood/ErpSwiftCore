using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IInventoryRepositories
{
    /// <summary>
    /// Repository interface for managing InventoryTransaction records.
    /// Covers CRUD, retrieval, search, analytics, paging, and validation scenarios for inventory transactions.
    /// </summary>
    public interface IInventoryTransactionRepository : IMultiTenantRepository<InventoryTransaction>
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAsync(InventoryTransaction transaction, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(InventoryTransaction transaction, CancellationToken cancellationToken = default);

        // -------------------- [Bulk Operations] --------------------
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<InventoryTransaction> transactions, CancellationToken cancellationToken = default);

        Task<int> BulkDeleteAsync(IEnumerable<Guid> transactionIds, CancellationToken cancellationToken = default);

        Task<int> BulkRestoreAsync(IEnumerable<Guid> transactionIds, CancellationToken cancellationToken = default);

        // -------------------- [Delete/Archive/Restore Operations] --------------------
        Task<bool> DeleteAsync(Guid transactionId, CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid transactionId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Single] --------------------
        Task<InventoryTransaction?> GetByIdAsync(Guid transactionId, CancellationToken cancellationToken = default);

        Task<InventoryTransaction?> GetSoftDeletedByIdAsync(Guid transactionId, CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        Task<IReadOnlyList<InventoryTransaction>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByTypeAsync(InventoryTransactionType type, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByJournalEntryIdAsync(Guid journalEntryId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByIdsAsync(IEnumerable<Guid> transactionIds, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetByFilterAsync(Expression<Func<InventoryTransaction, bool>> filter, CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering/Searching] --------------------
        Task<(IReadOnlyList<InventoryTransaction> Transactions, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> SearchByReferenceAsync(string searchTerm, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> SearchByNotesAsync(string noteTerm, CancellationToken cancellationToken = default);

        // -------------------- [Existence & Validation] --------------------
        Task<bool> ExistsByIdAsync(Guid transactionId, CancellationToken cancellationToken = default);

        Task<bool> ExistsForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        // -------------------- [Stats & Analytics] --------------------
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        // -------------------- [Advanced Use Cases] --------------------
        Task<InventoryTransaction?> GetLastTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<InventoryTransaction?> GetFirstTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsAffectingBalanceAsync(Guid inventoryId, CancellationToken cancellationToken = default);

        Task<int> SumQuantityChangeByProductAndDateRangeAsync(
            Guid productId,
            DateTime from,
            DateTime to,
            CancellationToken cancellationToken = default);
    }
}