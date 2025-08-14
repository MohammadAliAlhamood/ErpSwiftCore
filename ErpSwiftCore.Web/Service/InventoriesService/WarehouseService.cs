using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.WarehouseModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.InventoriesService
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IBaseService _baseService;

        public WarehouseService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetInventoriesAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/inventories"
            });
        }

        public async Task<APIResponseDto?> GetTotalInventoriesAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/inventories/count"
            });
        }

        public async Task<APIResponseDto?> GetTotalProductsAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/products/count"
            });
        }

        public async Task<APIResponseDto?> GetLowStockCountAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/count/low-stock"
            });
        }

        public async Task<APIResponseDto?> GetOverstockedCountAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/count/overstock"
            });
        }

        public async Task<APIResponseDto?> GetAverageStockLevelAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/average-stock"
            });
        }

        public async Task<APIResponseDto?> GetRecentAsync(int maxCount = 10)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/recent?maxCount={maxCount}"
            });
        }

        public async Task<APIResponseDto?> GetInventoryCountPerWarehouseAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/inventory-count-per-warehouse"
            });
        }

        public async Task<APIResponseDto?> GetTotalWarehousesAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/count"
            });
        }

        public async Task<APIResponseDto?> GetCountByBranchAsync(Guid branchId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/count/branch/{branchId}"
            });
        }

        public async Task<APIResponseDto?> GetByIdAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}"
            });
        }

        public async Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/soft-deleted/{warehouseId}"
            });
        }

        public async Task<APIResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/all"
            }, true);
        }

        public async Task<APIResponseDto?> GetOperationalAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/operational"
            });
        }

        public async Task<APIResponseDto?> GetStorageOnlyAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/storage-only"
            });
        }

        public async Task<APIResponseDto?> GetByBranchAsync(Guid branchId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/by-branch/{branchId}"
            });
        }

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/warehouse/by-ids"
            });
        }

        public async Task<APIResponseDto?> GetWithBranchAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/with-branch"
            });
        }

        public async Task<APIResponseDto?> GetWithInventoriesAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/warehouse/{warehouseId}/with-inventories"
            });
        }

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateWarehouseDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/create"
            });
        }

        public async Task<APIResponseDto?> UpdateAsync(UpdateWarehouseDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/update"
            });
        }

        public async Task<APIResponseDto?> DeleteAsync(Guid warehouseId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/warehouse/delete/{warehouseId}"
            });
        }

        public async Task<APIResponseDto?> DeleteRangeAsync(DeleteWarehousesRangeDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/delete-range"
            });
        }

        public async Task<APIResponseDto?> DeleteAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/warehouse/delete-all"
            });
        }

        public async Task<APIResponseDto?> RestoreAsync(RestoreWarehouseDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/restore"
            });
        }

        public async Task<APIResponseDto?> RestoreRangeAsync(RestoreWarehousesRangeDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/restore-range"
            });
        }

        public async Task<APIResponseDto?> RestoreAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/warehouse/restore-all"
            });
        }

        public async Task<APIResponseDto?> AddRangeAsync(AddWarehousesRangeDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/add-range"
            });
        }

        public async Task<APIResponseDto?> BulkImportAsync(BulkImportWarehousesDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/bulk-import"
            });
        }

        public async Task<APIResponseDto?> BulkDeleteAsync(BulkDeleteWarehousesDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/bulk-delete"
            });
        }

        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> CheckExistsAsync(WarehouseExistsDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/check-exists"
            });
        }

        public async Task<APIResponseDto?> CheckNameAsync(ExistsWarehouseNameDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/check-name"
            });
        }

        public async Task<APIResponseDto?> ValidateBranchAsync(ValidateBranchDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/warehouse/validate-branch"
            });
        }
    }
}
