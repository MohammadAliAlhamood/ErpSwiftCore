using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.StocksTransferModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
using System.Collections.Generic;

namespace ErpSwiftCore.Web.IService.IInventoriesService
{
    public interface IStocksTransferService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetActiveAsync();
        Task<APIResponseDto?> GetAllSoftDeletedAsync();
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetByProductAsync(Guid productId);
        Task<APIResponseDto?> GetByFromWarehouseAsync(Guid fromWarehouseId);
        Task<APIResponseDto?> GetByToWarehouseAsync(Guid toWarehouseId);
        Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to);  
        Task<APIResponseDto?> GetWithProductAsync(Guid id);
        Task<APIResponseDto?> GetWithWarehousesAsync(Guid id);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByProductAsync(Guid productId);
        Task<APIResponseDto?> GetCountByWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> GetTotalQuantityByProductAsync(Guid productId, DateTime? from = null, DateTime? to = null);
        Task<APIResponseDto?> GetTotalQuantityByWarehouseAsync(Guid warehouseId, DateTime? from = null, DateTime? to = null);
        Task<APIResponseDto?> SearchByNotesAsync(string term);
        Task<APIResponseDto?> SearchByReferenceAsync(string reference);
        Task<APIResponseDto?> SearchByProductNameAsync(string productName);
        Task<APIResponseDto?> GetLastForProductAsync(Guid productId);
        Task<APIResponseDto?> GetLastForWarehouseAsync(Guid warehouseId);

        // Commands
        Task<APIResponseDto?> CreateAsync(CreateStockTransferDto dto);
        Task<APIResponseDto?> UpdateAsync(UpdateStockTransferDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid id);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();
        Task<APIResponseDto?> RestoreAsync(Guid id);
        Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> RestoreAllAsync();
        Task<APIResponseDto?> BulkImportAsync(BulkImportStockTransfersDto dto);
        Task<APIResponseDto?> BulkDeleteAsync(BulkDeleteStockTransfersDto dto);

        // Validation Commands
        Task<APIResponseDto?> CheckExistsAsync(Guid id);
        Task<APIResponseDto?> CheckDuplicateAsync(DuplicateTransferCheckDto dto);
        Task<APIResponseDto?> ValidateProductAsync(Guid productId);
        Task<APIResponseDto?> ValidateWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> ValidateQuantityAsync(int quantity);
    }
}
