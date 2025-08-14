using ErpSwiftCore.Web.Models.ProductSystemManagmentModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IProductsService
{
    public interface IProductCategoryService
    {
        // ─────────────── Command Methods ───────────────
        Task<APIResponseDto?> CreateAsync(ProductCategoryCreateDto dto);
        Task<APIResponseDto?> UpdateAsync(ProductCategoryUpdateDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid id);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();

        // ─────────────── Query Methods ───────────────
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByParentAsync(Guid parentId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetDescendantsAsync(Guid parentId);
        Task<APIResponseDto?> GetHierarchyAsync(Guid id);
        Task<APIResponseDto?> GetWithParentAsync(Guid id);
        Task<APIResponseDto?> GetWithSubCategoriesAsync(Guid id);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByParentAsync(Guid parentId, bool recursive = false);
    }
}
