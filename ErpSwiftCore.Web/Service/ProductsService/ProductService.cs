using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
namespace ErpSwiftCore.Web.Service.ProductsService
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> CreateAsync(ProductCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product/create"
            });
        }
        public async Task<APIResponseDto?> UpdateAsync(ProductUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product/update"
            });
        }
        public async Task<APIResponseDto?> DeleteAsync(ProductDeleteDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product/delete"
            });
        }
        public async Task<APIResponseDto?> DeleteRangeAsync(ProductDeleteRangeDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product/delete-range"
            });
        }
        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product/all"
            });
        }
        public async Task<APIResponseDto?> GetByIdAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/{id}"
            });
        }
        public async Task<APIResponseDto?> GetByCategoryAsync(Guid categoryId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/by-category/{categoryId}"
            });
        }
        public async Task<APIResponseDto?> GetByBundleAsync(Guid bundleId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/by-bundle/{bundleId}"
            });
        }
        public async Task<APIResponseDto?> GetByTaxAsync(Guid taxId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/by-tax/{taxId}"
            });
        }
        public async Task<APIResponseDto?> GetByUnitAsync(Guid unitId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/by-unit/{unitId}"
            });
        }
        public async Task<APIResponseDto?> GetByProductTypeAsync(Guid productTypeId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/by-product-type/{productTypeId}"
            });
        }
        public async Task<APIResponseDto?> GetWithBundlesAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/with-bundles/{id}"
            });
        }
        public async Task<APIResponseDto?> GetWithPricesAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/with-prices/{id}"
            });
        }
        public async Task<APIResponseDto?> GetWithTaxesAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/with-taxes/{id}"
            });
        }
        public async Task<APIResponseDto?> GetWithUnitsAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product/with-units/{id}"
            });
        }
    }
}
