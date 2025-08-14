using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryTransactionService
{
    public interface IInventoryTransactionQueryService
    { 
        Task<InventoryTransaction?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByTypeAsync(InventoryTransactionType type, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<int> GetTransactionsCountAsync(CancellationToken cancellationToken = default);
        Task<int> SumQuantityByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<decimal> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<InventoryTransaction?> GetLastTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<InventoryTransaction?> GetFirstTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsAffectingBalanceAsync(Guid inventoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<InventoryTransaction>> GetTransactionsWithNotesContainingAsync(string noteTerm, CancellationToken cancellationToken = default);
    }
}
