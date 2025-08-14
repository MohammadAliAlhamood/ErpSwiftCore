using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IProductsService
{
    public interface IProductService
    {
        // ─────────────── Command Methods ───────────────
        Task<APIResponseDto?> CreateAsync(ProductCreateDto dto);
        Task<APIResponseDto?> UpdateAsync(ProductUpdateDto dto);
        Task<APIResponseDto?> DeleteAsync(ProductDeleteDto dto);
        Task<APIResponseDto?> DeleteRangeAsync(ProductDeleteRangeDto dto); 

        // ─────────────── Query Methods ───────────────
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByCategoryAsync(Guid categoryId);
        Task<APIResponseDto?> GetByBundleAsync(Guid bundleId);
        Task<APIResponseDto?> GetByTaxAsync(Guid taxId);
        Task<APIResponseDto?> GetByUnitAsync(Guid unitId); 
        Task<APIResponseDto?> GetByProductTypeAsync(Guid productTypeId);
        Task<APIResponseDto?> GetWithBundlesAsync(Guid id);
        Task<APIResponseDto?> GetWithPricesAsync(Guid id);
        Task<APIResponseDto?> GetWithTaxesAsync(Guid id);
        Task<APIResponseDto?> GetWithUnitsAsync(Guid id);
    }
}
