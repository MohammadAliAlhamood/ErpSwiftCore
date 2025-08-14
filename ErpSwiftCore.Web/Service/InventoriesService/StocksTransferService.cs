using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.StocksTransferModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.InventoriesService
{
    public class StocksTransferService : IStocksTransferService
    {
        private readonly IBaseService _baseService;

        public StocksTransferService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/{id}"
            });

        public async Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/soft-deleted/{id}"
            });

        public async Task<APIResponseDto?> GetAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/all"
            }, true);

        public async Task<APIResponseDto?> GetActiveAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/active"
            });

        public async Task<APIResponseDto?> GetAllSoftDeletedAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/soft-deleted/all"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/by-ids"
            });

        public async Task<APIResponseDto?> GetByProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/by-product/{productId}"
            });

        public async Task<APIResponseDto?> GetByFromWarehouseAsync(Guid fromWarehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/by-from-warehouse/{fromWarehouseId}"
            });

        public async Task<APIResponseDto?> GetByToWarehouseAsync(Guid toWarehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/by-to-warehouse/{toWarehouseId}"
            });

        public async Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/by-warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/by-date-range?from={from:o}&to={to:o}"
            });

        public async Task<APIResponseDto?> GetByFilterAsync(string filterExpression) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = filterExpression,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/filter"
            });

        public async Task<APIResponseDto?> GetPagedAsync(int pageIndex, int pageSize) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/paged?pageIndex={pageIndex}&pageSize={pageSize}"
            });

        public async Task<APIResponseDto?> GetPagedByActiveAsync(bool isActive, int pageIndex, int pageSize) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/paged/active?isActive={isActive}&pageIndex={pageIndex}&pageSize={pageSize}"
            });

        public async Task<APIResponseDto?> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/paged/product/{productId}?pageIndex={pageIndex}&pageSize={pageSize}"
            });

        public async Task<APIResponseDto?> GetPagedByWarehouseAsync(Guid warehouseId, int pageIndex, int pageSize) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/paged/warehouse/{warehouseId}?pageIndex={pageIndex}&pageSize={pageSize}"
            });

        public async Task<APIResponseDto?> GetWithProductAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/with-product/{id}"
            });

        public async Task<APIResponseDto?> GetWithWarehousesAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/with-warehouses/{id}"
            });

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/count"
            });

        public async Task<APIResponseDto?> GetCountByProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/count/product/{productId}"
            });

        public async Task<APIResponseDto?> GetCountByWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/count/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetTotalQuantityByProductAsync(Guid productId, DateTime? from = null, DateTime? to = null)
        {
            var url = $"{SD.ErpAPIBase}/api/stock-transfer/sum/product/{productId}";
            if (from.HasValue || to.HasValue)
                url += $"?from={from:o}&to={to:o}";
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = url
            });
        }

        public async Task<APIResponseDto?> GetTotalQuantityByWarehouseAsync(Guid warehouseId, DateTime? from = null, DateTime? to = null)
        {
            var url = $"{SD.ErpAPIBase}/api/stock-transfer/sum/warehouse/{warehouseId}";
            if (from.HasValue || to.HasValue)
                url += $"?from={from:o}&to={to:o}";
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = url
            });
        }

        public async Task<APIResponseDto?> SearchByNotesAsync(string term) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/search/notes?term={Uri.EscapeDataString(term)}"
            });

        public async Task<APIResponseDto?> SearchByReferenceAsync(string reference) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/search/reference?reference={Uri.EscapeDataString(reference)}"
            });

        public async Task<APIResponseDto?> SearchByProductNameAsync(string productName) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/search/product-name?productName={Uri.EscapeDataString(productName)}"
            });

        public async Task<APIResponseDto?> GetLastForProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/last/product/{productId}"
            });

        public async Task<APIResponseDto?> GetLastForWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/last/warehouse/{warehouseId}"
            });


        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateStockTransferDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/create"
            });

        public async Task<APIResponseDto?> UpdateAsync(UpdateStockTransferDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/update"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/delete/{id}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/delete-range"
            });

        public async Task<APIResponseDto?> DeleteAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/delete-all"
            });

        public async Task<APIResponseDto?> RestoreAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/restore/{id}"
            });

        public async Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/restore-range"
            });

        public async Task<APIResponseDto?> RestoreAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/restore-all"
            });

        public async Task<APIResponseDto?> BulkImportAsync(BulkImportStockTransfersDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/bulk-import"
            });

        public async Task<APIResponseDto?> BulkDeleteAsync(BulkDeleteStockTransfersDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/bulk-delete"
            });


        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> CheckExistsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/validate/exists/{id}"
            });

        public async Task<APIResponseDto?> CheckDuplicateAsync(DuplicateTransferCheckDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/validate/duplicate"
            });

        public async Task<APIResponseDto?> ValidateProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/validate/product/{productId}"
            });

        public async Task<APIResponseDto?> ValidateWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/validate/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> ValidateQuantityAsync(int quantity) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/stock-transfer/validate/quantity/{quantity}"
            });
    }
}
