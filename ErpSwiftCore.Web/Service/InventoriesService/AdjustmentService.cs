using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.AdjustmentModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ErpSwiftCore.Web.Models.InventorySystemManagmentModels.AdjustmentModels.UpdateInventoryAdjustmentReasonByDateRangeDto;

namespace ErpSwiftCore.Web.Service.InventoriesService
{
    public class AdjustmentService : IAdjustmentService
    {
        private readonly IBaseService _baseService;

        public AdjustmentService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/{id}"
            });

        public async Task<APIResponseDto?> GetAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/all"
            }, true);

        public async Task<APIResponseDto?> GetAllSoftDeletedAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/soft-deleted/all"
            });

        public async Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/soft-deleted/{id}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/adjustment/by-ids"
            });

        public async Task<APIResponseDto?> GetByProductAsync(Guid productId, Guid? warehouseId = null)
        {
            var url = $"{SD.ErpAPIBase}/api/adjustment/by-product/{productId}";
            if (warehouseId.HasValue) url += $"?warehouseId={warehouseId}";
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = url
            });
        }

        public async Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/by-warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to, Guid? warehouseId = null)
        {
            var url = $"{SD.ErpAPIBase}/api/adjustment/by-date?from={from:o}&to={to:o}";
            if (warehouseId.HasValue) url += $"&warehouseId={warehouseId}";
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = url
            });
        }

        public async Task<APIResponseDto?> GetByStockTakeAsync(Guid stockTakeId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/by-stocktake/{stockTakeId}"
            });

        public async Task<APIResponseDto?> GetCountsByReasonAsync(Guid? warehouseId = null, DateTime? from = null, DateTime? to = null)
        {
            var query = $"{SD.ErpAPIBase}/api/adjustment/counts/by-reason";
            var qs = new List<string>();
            if (warehouseId.HasValue) qs.Add($"warehouseId={warehouseId}");
            if (from.HasValue) qs.Add($"from={from:o}");
            if (to.HasValue) qs.Add($"to={to:o}");
            if (qs.Count > 0) query += "?" + string.Join("&", qs);
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = query
            });
        }

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/count"
            });

        public async Task<APIResponseDto?> GetCountByProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/count/product/{productId}"
            });

        public async Task<APIResponseDto?> GetCountByWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/count/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetWithProductAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/with-product/{id}"
            });

        public async Task<APIResponseDto?> GetWithWarehouseAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/with-warehouse/{id}"
            });

        public async Task<APIResponseDto?> GetLastAdjustmentAsync(Guid productId, Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/last/{productId}/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetCurrentStockAsync(Guid productId, Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/current-stock/{productId}/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> SumQuantityByDateRangeAsync(Guid productId, DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/sum-quantity/{productId}?from={from:o}&to={to:o}"
            });


        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateInventoryAdjustmentDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/adjustment/create"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/adjustment/delete/{id}"
            });

        public async Task<APIResponseDto?> RestoreAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/adjustment/restore/{id}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/adjustment/delete-range"
            });

        public async Task<APIResponseDto?> DeleteAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/adjustment/delete-all"
            });

        public async Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/adjustment/restore-range"
            });

        public async Task<APIResponseDto?> RestoreAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/adjustment/restore-all"
            });

        public async Task<APIResponseDto?> BulkDeleteAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/adjustment/bulk-delete"
            });

        public async Task<APIResponseDto?> UpdateRangeAsync(UpdateInventoryAdjustmentsRangeDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/adjustment/update-range"
            });

        public async Task<APIResponseDto?> UpdateReasonAsync(UpdateInventoryAdjustmentReasonByDateRangeDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/adjustment/update-reason"
            });


        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> CheckExistsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/validate/exists/{id}"
            });

        public async Task<APIResponseDto?> CheckExistsOnDateAsync(ExistsForProductWarehouseOnDateDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/adjustment/validate/exists-on-date"
            });

        public async Task<APIResponseDto?> ValidateProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/validate/product/{productId}"
            });

        public async Task<APIResponseDto?> ValidateWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/validate/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> ValidateQuantityAsync(int quantity) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/adjustment/validate/quantity/{quantity}"
            });
    }
}
