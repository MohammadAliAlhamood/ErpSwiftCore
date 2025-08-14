using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductPriceModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
namespace ErpSwiftCore.Web.Service.ProductsService
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly IBaseService _baseService;

        public ProductPriceService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(ProductPriceCreateDto dto)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-price/create"
            });

        public async Task<APIResponseDto?> UpdateAsync(ProductPriceUpdateDto dto)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-price/update"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid priceId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/product-price/delete/{priceId}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-price/delete-range"
            });

        public async Task<APIResponseDto?> DeleteAllAsync()
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/product-price/delete-all"
            });

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetAllAsync()
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-price/all"
            }, true);

        public async Task<APIResponseDto?> GetByIdAsync(Guid priceId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/{priceId}"
            });

        public async Task<APIResponseDto?> GetLatestAsync(Guid productId, string priceType)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/latest/{productId}/{priceType}"
            });

        public async Task<APIResponseDto?> GetByProductAsync(Guid productId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/by-product/{productId}"
            });

        public async Task<APIResponseDto?> GetByTypeAsync(string priceType)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/by-type/{priceType}"
            });

        public async Task<APIResponseDto?> GetByCurrencyAsync(Guid currencyId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/by-currency/{currencyId}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-price/by-ids"
            });

        public async Task<APIResponseDto?> GetCountAsync()
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-price/count"
            });

        public async Task<APIResponseDto?> GetCountByProductAsync(Guid productId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/count/by-product/{productId}"
            });

        public async Task<APIResponseDto?> GetCountByTypeAsync(string priceType)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/count/by-type/{priceType}"
            });

        public async Task<APIResponseDto?> GetWithProductAsync(Guid priceId)
            => await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-price/with-product/{priceId}"
            });
    }
}
