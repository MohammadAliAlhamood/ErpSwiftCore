using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryPolicyModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.InventoriesService
{
    public class InventoryPolicyService : IInventoryPolicyService
    {
        private readonly IBaseService _baseService;

        public InventoryPolicyService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/{id}"
            });

        public async Task<APIResponseDto?> GetByInventoryAsync(Guid inventoryId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/by-inventory/{inventoryId}"
            });

        public async Task<APIResponseDto?> GetAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/all"
            }, true);

        public async Task<APIResponseDto?> GetByTypeAsync(InventoryPolicyType policyType) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/by-type/{policyType}"
            });

        public async Task<APIResponseDto?> GetWithAutoReorderAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/with-auto-reorder"
            });

        public async Task<APIResponseDto?> GetBelowReorderAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/below-reorder"
            });

        public async Task<APIResponseDto?> GetAboveMaxAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/above-max"
            });

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/count"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> EnableAutoReorderAsync(EnableAutoReorderDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/enable-auto-reorder"
            });

        public async Task<APIResponseDto?> DisableAutoReorderAsync(DisableAutoReorderDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/disable-auto-reorder"
            });

        public async Task<APIResponseDto?> UpdatePolicyAsync(UpdatePolicyDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/update"
            });

        public async Task<APIResponseDto?> UpdateReorderLevelAsync(UpdateReorderLevelDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/update-reorder-level"
            });

        public async Task<APIResponseDto?> UpdateMaxStockLevelAsync(UpdateMaxStockLevelDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/update-max-stock-level"
            });

        public async Task<APIResponseDto?> UpdateRangeAsync(UpdatePoliciesRangeDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/inventory-policy/update-range"
            });
    }
}
