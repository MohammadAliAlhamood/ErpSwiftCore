using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductUnitConversionModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.ProductsService
{
    public class ProductUnitConversionService : IProductUnitConversionService
    {
        private readonly IBaseService _baseService;

        public ProductUnitConversionService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(ProductUnitConversionCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-unit-conversion/create"
            });
        }

        public async Task<APIResponseDto?> UpdateAsync(ProductUnitConversionUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-unit-conversion/update"
            });
        }

        public async Task<APIResponseDto?> DeleteAsync(Guid conversionId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/delete/{conversionId}"
            });
        }

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-unit-conversion/delete-range"
            });
        }

        public async Task<APIResponseDto?> DeleteAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/product-unit-conversion/delete-all"
            });
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-unit-conversion/all"
            }, true);
        }

        public async Task<APIResponseDto?> GetByIdAsync(Guid conversionId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/{conversionId}"
            });
        }

        public async Task<APIResponseDto?> GetByProductAsync(Guid productId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/product/{productId}"
            });
        }

        public async Task<APIResponseDto?> GetByFromUnitAsync(Guid fromUnitId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/from-unit/{fromUnitId}"
            });
        }

        public async Task<APIResponseDto?> GetByToUnitAsync(Guid toUnitId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/to-unit/{toUnitId}"
            });
        }

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-unit-conversion/by-ids"
            });
        }

        public async Task<APIResponseDto?> GetWithProductAsync(Guid conversionId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/with-product/{conversionId}"
            });
        }

        public async Task<APIResponseDto?> GetWithFromUnitAsync(Guid conversionId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/with-from-unit/{conversionId}"
            });
        }

        public async Task<APIResponseDto?> GetWithToUnitAsync(Guid conversionId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/with-to-unit/{conversionId}"
            });
        }

        public async Task<APIResponseDto?> GetCountAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-unit-conversion/count"
            });
        }

        public async Task<APIResponseDto?> GetCountByProductAsync(Guid productId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-unit-conversion/count/product/{productId}"
            });
        }
    }
}
