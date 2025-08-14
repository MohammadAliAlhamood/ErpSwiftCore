using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductBundleModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IProductsService;

namespace ErpSwiftCore.Web.Service.ProductsService
{
    public class ProductBundleService : IProductBundleService
    {
        private readonly IBaseService _baseService;

        public ProductBundleService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(ProductBundleCreateDto dto)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-bundle/create"
            });

        public async Task<APIResponseDto?> UpdateAsync(ProductBundleUpdateDto dto)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-bundle/update"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid bundleId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/product-bundle/delete/{bundleId}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-bundle/delete-range"
            });

        public async Task<APIResponseDto?> DeleteAllAsync()
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/product-bundle/delete-all"
            });

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetAllAsync()
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-bundle/all"
            }, true);

        public async Task<APIResponseDto?> GetActiveAsync()
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-bundle/active"
            });

        public async Task<APIResponseDto?> GetByIdAsync(Guid bundleId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/{bundleId}"
            });

        public async Task<APIResponseDto?> GetByParentProductAsync(Guid parentProductId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/parent/{parentProductId}"
            });

        public async Task<APIResponseDto?> GetByComponentProductAsync(Guid componentProductId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/component/{componentProductId}"
            });

        public async Task<APIResponseDto?> GetByUnitAsync(Guid unitOfMeasurementId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/unit/{unitOfMeasurementId}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-bundle/by-ids"
            });

        public async Task<APIResponseDto?> GetWithParentAsync(Guid bundleId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/with-parent/{bundleId}"
            });

        public async Task<APIResponseDto?> GetWithComponentAsync(Guid bundleId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/with-component/{bundleId}"
            });

        public async Task<APIResponseDto?> GetWithUnitAsync(Guid bundleId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/with-unit/{bundleId}"
            });

        public async Task<APIResponseDto?> GetCountAsync()
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-bundle/count"
            });

        public async Task<APIResponseDto?> GetCountByParentAsync(Guid parentProductId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/count/parent/{parentProductId}"
            });

        public async Task<APIResponseDto?> GetCountByComponentAsync(Guid componentProductId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-bundle/count/component/{componentProductId}"
            });
    }
}
