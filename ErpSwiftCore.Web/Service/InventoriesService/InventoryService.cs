using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.InventoriesService
{
    public class InventoryService : IInventoryService
    {
        private readonly IBaseService _baseService;

        public InventoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/{id}"
            });

        public async Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/soft-deleted/{id}"
            });

        public async Task<APIResponseDto?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/by-product/{productId}/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetSnapshotAsync(Guid productId, Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/snapshot/{productId}/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetLastRecordAsync(Guid productId, Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/last-record/{productId}/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/all"
            }, true);

        public async Task<APIResponseDto?> GetByProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/by-product/{productId}"
            });

        public async Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/by-warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetWithProductAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/with-product/{id}"
            });

        public async Task<APIResponseDto?> GetWithWarehouseAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/with-warehouse/{id}"
            });

        public async Task<APIResponseDto?> GetWithPolicyAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/with-policy/{id}"
            });

        public async Task<APIResponseDto?> GetWithTransactionsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/with-transactions/{id}"
            });

        public async Task<APIResponseDto?> GetWithNotificationsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/with-notifications/{id}"
            });

        public async Task<APIResponseDto?> GetTransactionsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/{id}/transactions"
            });

        public async Task<APIResponseDto?> GetPolicyAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/{id}/policy"
            });

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/count"
            });

        public async Task<APIResponseDto?> GetCountByProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/count/product/{productId}"
            });

        public async Task<APIResponseDto?> GetCountByWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/count/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetLowStockCountAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/count/low-stock/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetOverstockedCountAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/count/overstock/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetAvailabilityAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/availability/{productId}"
            });

        public async Task<APIResponseDto?> GetStockSummaryAsync(IEnumerable<Guid> productIds) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = productIds,
                Url = $"{SD.ErpAPIBase}/api/inventory/summary/by-products"
            });

        public async Task<APIResponseDto?> GetTotalAvailableAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/total-available/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetTotalReservedAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/total-reserved/{warehouseId}"
            });

        public async Task<APIResponseDto?> CalculateValueAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/value/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetAverageLevelAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/average/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/turnover/{productId}?from={from:o}&to={to:o}"
            });

        public async Task<APIResponseDto?> GetCurrentAfterAdjustmentsAsync(Guid productId, Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/current/{productId}/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetBelowReorderAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/below-reorder"
            });

        public async Task<APIResponseDto?> GetAboveMaxAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory/above-max"
            });
    }
}
