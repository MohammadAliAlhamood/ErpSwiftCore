using ErpSwiftCore.Domain.Entities.EntityProduct;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IProductRepositories
{
    /// <summary>
    /// Repository interface for managing product categories.
    /// Supports advanced retrieval, validation, analytics, paging, search, state, archive/restore, and bulk operations.
    /// </summary>
    public interface IProductCategoryRepository : IMultiTenantRepository<ProductCategory>
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateAsync(ProductCategory category, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductCategory> categories, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(ProductCategory category, CancellationToken cancellationToken = default);
        
        
        
        Task<bool> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken);

        Task<bool> RestoreAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

     
        // -------------------- [Get/Query Operations - Single] --------------------
        Task<ProductCategory?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<ProductCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetAllAsync(CancellationToken cancellationToken = default);
         Task<IReadOnlyList<ProductCategory>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetByParentAsync(Guid parentCategoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetByIdsAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetAllDescendantsAsync(Guid parentCategoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetCategoryHierarchyAsync(Guid categoryId, CancellationToken cancellationToken = default);
             Task<bool> ExistsByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, Guid? parentCategoryId = null, Guid? excludeCategoryId = null, CancellationToken cancellationToken = default);
        Task<bool> IsNameUniqueAsync(string name, Guid? parentCategoryId = null, Guid? excludeCategoryId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidParentCategoryAsync(Guid? parentCategoryId, Guid? excludeCategoryId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsCircularDependencyAsync(Guid categoryId, Guid? parentCategoryId, CancellationToken cancellationToken = default);
        Task<ProductCategory?> GetWithParentAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<ProductCategory?> GetWithSubCategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByParentAsync(Guid parentCategoryId, bool recursive = false, CancellationToken cancellationToken = default);
        Task<int> BulkImportAsync(IEnumerable<ProductCategory> categories, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetByFilterAsync(Expression<Func<ProductCategory, bool>> filter, CancellationToken cancellationToken = default);
        Task<ProductCategory?> GetSoftDeletedByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default);
    }
}