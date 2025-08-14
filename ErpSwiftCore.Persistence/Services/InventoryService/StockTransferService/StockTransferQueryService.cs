using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.InventoryService.StockTransferService
{
    public class StockTransferQueryService : IStockTransferQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public StockTransferQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StockTransfer?> GetStockTransferByIdAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByIdAsync(transferId, cancellationToken);
        }

        public async Task<StockTransfer?> GetSoftDeletedStockTransferByIdAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetSoftDeletedByIdAsync(transferId, cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetAllStockTransfersAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetAllAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetActiveStockTransfersAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetActiveAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetSoftDeletedStockTransfersAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetSoftDeletedAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetStockTransfersByIdsAsync(IEnumerable<Guid> transferIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByIdsAsync(transferIds, cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetStockTransfersByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByProductIdAsync(productId, cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetStockTransfersByFromWarehouseAsync(Guid fromWarehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByFromWarehouseAsync(fromWarehouseId, cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetStockTransfersByToWarehouseAsync(Guid toWarehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByToWarehouseAsync(toWarehouseId, cancellationToken);
        }

        public async Task<IReadOnlyList<StockTransfer>> GetStockTransfersByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByWarehouseAsync(warehouseId, cancellationToken);
        }
        public async Task<IReadOnlyList<StockTransfer>> GetStockTransfersByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByDateRangeAsync(from, to, cancellationToken);
        }
        public async Task<IReadOnlyList<StockTransfer>> GetStockTransfersByFilterAsync(Expression<Func<StockTransfer, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByFilterAsync(filter, cancellationToken);
        }
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetPagedAsync(pageIndex, pageSize, cancellationToken);
        }
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedByActiveStatusAsync(bool isActive, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetPagedByActiveStatusAsync(isActive, pageIndex, pageSize, cancellationToken);
        }
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetPagedByProductAsync(productId, pageIndex, pageSize, cancellationToken);
        }
        public async Task<(IReadOnlyList<StockTransfer> Transfers, int TotalCount)> GetStockTransfersPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetPagedByWarehouseAsync(warehouseId, pageIndex, pageSize, cancellationToken);
        }
        public async Task<StockTransfer?> GetStockTransferWithProductAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetWithProductAsync(transferId, cancellationToken);
        }
        public async Task<StockTransfer?> GetStockTransferWithWarehousesAsync(Guid transferId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetWithWarehousesAsync(transferId, cancellationToken);
        }
        public async Task<int> GetStockTransfersCountAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.CountAsync(cancellationToken);
        }
        public async Task<int> GetStockTransfersCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.CountByProductAsync(productId, cancellationToken);
        }
        public async Task<int> GetStockTransfersCountByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.CountByWarehouseAsync(warehouseId, cancellationToken);
        }
        public async Task<int> GetTotalTransferredQuantityByProductAsync(Guid productId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.StockTransfer.GetByProductIdAsync(productId, cancellationToken);
            if (from.HasValue) list = list.Where(t => t.TransferDate >= from.Value).ToList();
            if (to.HasValue) list = list.Where(t => t.TransferDate <= to.Value).ToList();
            return list.Sum(t => t.Quantity);
        }
        public async Task<int> GetTotalTransferredQuantityByWarehouseAsync(Guid warehouseId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.StockTransfer.GetByWarehouseAsync(warehouseId, cancellationToken);
            if (from.HasValue) list = list.Where(t => t.TransferDate >= from.Value).ToList();
            if (to.HasValue) list = list.Where(t => t.TransferDate <= to.Value).ToList();
            return list.Sum(t => t.Quantity);
        }
        public async Task<IReadOnlyList<StockTransfer>> SearchStockTransfersByNotesAsync(string noteTerm, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.GetByFilterAsync(t => t.Notes != null && t.Notes.Contains(noteTerm), cancellationToken);
        }
        public async Task<IReadOnlyList<StockTransfer>> SearchStockTransfersByReferenceAsync(string referenceNumber, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.SearchByReferenceAsync(referenceNumber, cancellationToken);
        }
        public async Task<IReadOnlyList<StockTransfer>> SearchStockTransfersByProductNameAsync(string productName, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StockTransfer.SearchByProductNameAsync(productName, cancellationToken);
        }
        public async Task<StockTransfer?> GetLastStockTransferForProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.StockTransfer.GetByProductIdAsync(productId, cancellationToken);
            return list.OrderByDescending(t => t.TransferDate).FirstOrDefault();
        }
        public async Task<StockTransfer?> GetLastStockTransferForWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default)
        {
            var list = await _unitOfWork.StockTransfer.GetByWarehouseAsync(warehouseId, cancellationToken);
            return list.OrderByDescending(t => t.TransferDate).FirstOrDefault();
        }
    }
}
