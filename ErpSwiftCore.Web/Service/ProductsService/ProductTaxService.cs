using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductTaxModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.ProductsService
{
    public class ProductTaxService : IProductTaxService
    {
        private readonly IBaseService _baseService;

        public ProductTaxService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(ProductTaxCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-tax/create"
            });
        }

        public async Task<APIResponseDto?> UpdateAsync(ProductTaxUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-tax/update"
            });
        }

        public async Task<APIResponseDto?> DeleteAsync(Guid taxId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/product-tax/delete/{taxId}"
            });
        }

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-tax/delete-range"
            });
        }

        public async Task<APIResponseDto?> DeleteAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/product-tax/delete-all"
            });
        }

        public async Task<APIResponseDto?> BulkDeleteAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-tax/bulk-delete"
            });
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-tax/all"
            }, true);
        }

        public async Task<APIResponseDto?> GetByIdAsync(Guid taxId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-tax/{taxId}"
            });
        }

        public async Task<APIResponseDto?> GetByProductAsync(Guid productId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-tax/by-product/{productId}"
            });
        }

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-tax/by-ids"
            });
        }

        public async Task<APIResponseDto?> GetCountAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-tax/count"
            });
        }

        public async Task<APIResponseDto?> GetCountByProductAsync(Guid productId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-tax/count/by-product/{productId}"
            });
        }

        public async Task<APIResponseDto?> GetWithProductAsync(Guid taxId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-tax/with-product/{taxId}"
            });
        }
    }
}
