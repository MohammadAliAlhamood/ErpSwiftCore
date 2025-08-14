using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryTransactionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.InventoryService.InventoryTransactionService
{
    public class InventoryTransactionQueryService : IInventoryTransactionQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public InventoryTransactionQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // -------------------- [Get Single] --------------------
        public Task<InventoryTransaction?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetByIdAsync(transactionId, cancellationToken);
        public Task<InventoryTransaction?> GetFirstTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetFirstTransactionForInventoryAsync(inventoryId, cancellationToken);
        public Task<InventoryTransaction?> GetLastTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetLastTransactionForInventoryAsync(inventoryId, cancellationToken);
        public Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetByInventoryIdAsync(inventoryId, cancellationToken);
        public Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetByProductIdAsync(productId, cancellationToken);
        public Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetByWarehouseIdAsync(warehouseId, cancellationToken);
        public Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByTypeAsync(InventoryTransactionType type, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetByTypeAsync(type, cancellationToken);
        public Task<IReadOnlyList<InventoryTransaction>> GetTransactionsByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetByDateRangeAsync(from, to, cancellationToken);
        public Task<IReadOnlyList<InventoryTransaction>> GetTransactionsAffectingBalanceAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.GetTransactionsAffectingBalanceAsync(inventoryId, cancellationToken);
         public Task<IReadOnlyList<InventoryTransaction>> GetTransactionsWithNotesContainingAsync(string noteTerm, CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.SearchByNotesAsync(noteTerm, cancellationToken);
        public Task<int> GetTransactionsCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.InventoryTransaction.CountAsync(cancellationToken);
      
        public Task<int> SumQuantityByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}