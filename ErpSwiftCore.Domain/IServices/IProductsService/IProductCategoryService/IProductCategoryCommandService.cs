using ErpSwiftCore.Domain.Entities.EntityProduct; 

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService
{
    public interface IProductCategoryCommandService
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateCategoryAsync(ProductCategory category, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddCategoriesRangeAsync(IEnumerable<ProductCategory> categories, CancellationToken cancellationToken = default);
        Task<bool> UpdateCategoryAsync(ProductCategory category, CancellationToken cancellationToken = default);

        // -------------------- [Soft‑Delete Operations] --------------------
        /// <summary>حذف فئة ناعماً (Soft Delete).</summary>
        Task<bool> SoftDeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

        /// <summary>حذف مجموعة من الفئات ناعماً (Soft Bulk Delete).</summary>
        Task<bool> SoftDeleteCategoriesRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);

        /// <summary>حذف جميع الفئات ناعماً.</summary>
        Task<bool> SoftDeleteAllCategoriesAsync(CancellationToken cancellationToken = default);

        // -------------------- [Hard‑Delete Operations] --------------------
        /// <summary>حذف فئة نهائياً (Hard Delete).</summary>
        Task<bool> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

        /// <summary>حذف مجموعة من الفئات نهائياً (Hard Bulk Delete).</summary>
        Task<bool> DeleteCategoriesRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);

        /// <summary>حذف جميع الفئات نهائياً.</summary>
        Task<bool> DeleteAllCategoriesAsync(CancellationToken cancellationToken = default);

        // -------------------- [Restore Operations] --------------------
        /// <summary>استرجاع فئة محذوفة ناعماً.</summary>
        Task<bool> RestoreCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

        /// <summary>استرجاع مجموعة من الفئات المحذوفة ناعماً (Soft Bulk Restore).</summary>
        Task<bool> RestoreCategoriesRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);

        /// <summary>استرجاع جميع الفئات المحذوفة ناعماً.</summary>
        Task<bool> RestoreAllCategoriesAsync(CancellationToken cancellationToken = default);

   
        // -------------------- [Bulk Import & Delete] --------------------
        Task<int> BulkImportCategoriesAsync(IEnumerable<ProductCategory> categories, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteCategoriesAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);
    }
}
