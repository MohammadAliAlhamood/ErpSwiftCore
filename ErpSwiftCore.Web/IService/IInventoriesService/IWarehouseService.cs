using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.WarehouseModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ErpSwiftCore.Web.IService.IInventoriesService
{
    public interface IWarehouseService
    {
        // Queries
        Task<APIResponseDto?> GetInventoriesAsync(Guid warehouseId);
        Task<APIResponseDto?> GetTotalInventoriesAsync(Guid warehouseId);
        Task<APIResponseDto?> GetTotalProductsAsync(Guid warehouseId);
        Task<APIResponseDto?> GetLowStockCountAsync(Guid warehouseId);
        Task<APIResponseDto?> GetOverstockedCountAsync(Guid warehouseId);
        Task<APIResponseDto?> GetAverageStockLevelAsync(Guid warehouseId);
        Task<APIResponseDto?> GetRecentAsync(int maxCount = 10);
        Task<APIResponseDto?> GetInventoryCountPerWarehouseAsync();
        Task<APIResponseDto?> GetTotalWarehousesAsync();
        Task<APIResponseDto?> GetCountByBranchAsync(Guid branchId);
        Task<APIResponseDto?> GetByIdAsync(Guid warehouseId);
        Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid warehouseId);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetOperationalAsync();
        Task<APIResponseDto?> GetStorageOnlyAsync();
        Task<APIResponseDto?> GetByBranchAsync(Guid branchId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetWithBranchAsync(Guid warehouseId);
        Task<APIResponseDto?> GetWithInventoriesAsync(Guid warehouseId);

        // Commands
        Task<APIResponseDto?> CreateAsync(CreateWarehouseDto dto);
        Task<APIResponseDto?> UpdateAsync(UpdateWarehouseDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid warehouseId);
        Task<APIResponseDto?> DeleteRangeAsync(DeleteWarehousesRangeDto dto);
        Task<APIResponseDto?> DeleteAllAsync();
        Task<APIResponseDto?> RestoreAsync(RestoreWarehouseDto dto);
        Task<APIResponseDto?> RestoreRangeAsync(RestoreWarehousesRangeDto dto);
        Task<APIResponseDto?> RestoreAllAsync();
        Task<APIResponseDto?> AddRangeAsync(AddWarehousesRangeDto dto);
        Task<APIResponseDto?> BulkImportAsync(BulkImportWarehousesDto dto);
        Task<APIResponseDto?> BulkDeleteAsync(BulkDeleteWarehousesDto dto);

        // Validation
        Task<APIResponseDto?> CheckExistsAsync(WarehouseExistsDto dto);
        Task<APIResponseDto?> CheckNameAsync(ExistsWarehouseNameDto dto);
        Task<APIResponseDto?> ValidateBranchAsync(ValidateBranchDto dto);
    }
}
