using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
namespace ErpSwiftCore.Web.Service.ProductsService
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IBaseService _baseService;
        public ProductCategoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(ProductCategoryCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-category/create"
            });
        }

        public async Task<APIResponseDto?> UpdateAsync(ProductCategoryUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/product-category/update"
            });
        }

        public async Task<APIResponseDto?> DeleteAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/product-category/delete/{id}"
            });
        }

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-category/delete-range"
            });
        }

        public async Task<APIResponseDto?> DeleteAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/product-category/delete-all"
            });
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/{id}"
            });
        }

        public async Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/soft-deleted/{id}"
            });
        }

        public async Task<APIResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-category/all"
            }, true);
        }

        public async Task<APIResponseDto?> GetByParentAsync(Guid parentId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/by-parent/{parentId}"
            });
        }

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = SD.ErpAPIBase + "/api/product-category/by-ids"
            });
        }

        public async Task<APIResponseDto?> GetDescendantsAsync(Guid parentId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/descendants/{parentId}"
            });
        }

        public async Task<APIResponseDto?> GetHierarchyAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/hierarchy/{id}"
            });
        }

        public async Task<APIResponseDto?> GetWithParentAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/with-parent/{id}"
            });
        }

        public async Task<APIResponseDto?> GetWithSubCategoriesAsync(Guid id)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/with-subcategories/{id}"
            });
        }

        public async Task<APIResponseDto?> GetCountAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/product-category/count"
            });
        }

        public async Task<APIResponseDto?> GetCountByParentAsync(Guid parentId, bool recursive = false)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/product-category/count/parent/{parentId}?recursive={recursive}"
            });
        }
    }
}
