using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.InventoriesService
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly IBaseService _baseService;

        public InventoryTransactionService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/{id}"
            });

        public async Task<APIResponseDto?> GetFirstForInventoryAsync(Guid inventoryId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/first/{inventoryId}"
            });

        public async Task<APIResponseDto?> GetLastForInventoryAsync(Guid inventoryId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/last/{inventoryId}"
            });

        public async Task<APIResponseDto?> GetByInventoryAsync(Guid inventoryId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/inventory/{inventoryId}"
            });

        public async Task<APIResponseDto?> GetByProductAsync(Guid productId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/product/{productId}"
            });

        public async Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/warehouse/{warehouseId}"
            });

        public async Task<APIResponseDto?> GetByTypeAsync(InventoryTransactionType transactionType) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/type/{transactionType}"
            });

        public async Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/date-range?from={from:o}&to={to:o}"
            });

        public async Task<APIResponseDto?> GetAffectingBalanceAsync(Guid inventoryId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/affecting-balance/{inventoryId}"
            });

        public async Task<APIResponseDto?> SearchByNotesAsync(string noteTerm) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/search?noteTerm={Uri.EscapeDataString(noteTerm)}"
            });

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/count"
            });

        public async Task<APIResponseDto?> SumQuantityByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/sum-quantity/{productId}?from={from:o}&to={to:o}"
            });

        public async Task<APIResponseDto?> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-transaction/turnover-rate/{productId}?from={from:o}&to={to:o}"
            });
    }
}
