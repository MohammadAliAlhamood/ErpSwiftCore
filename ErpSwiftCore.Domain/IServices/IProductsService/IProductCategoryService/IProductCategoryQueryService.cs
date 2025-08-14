using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService
{
    public interface IProductCategoryQueryService
    {
        #region Single Retrieval
        Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
        /// <summary>Retrieves a category by its ID, active or soft-deleted.</summary>
        Task<ProductCategory?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);

        /// <summary>Retrieves a category by its name.</summary>
        Task<ProductCategory?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>Retrieves a category that has been soft-deleted.</summary>
        Task<ProductCategory?> GetSoftDeletedCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);

        #endregion

        #region Bulk / Advanced Retrieval

    
        /// <summary>Soft-deleted categories (IsDeleted=true).</summary>
        Task<IReadOnlyList<ProductCategory>> GetSoftDeletedCategoriesAsync(CancellationToken cancellationToken = default);

   
        Task<IReadOnlyList<ProductCategory>> GetCategoriesByParentAsync(Guid parentCategoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetCategoriesByIdsAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetAllDescendantCategoriesAsync(Guid parentCategoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductCategory>> GetCategoryHierarchyAsync(Guid categoryId, CancellationToken cancellationToken = default);

        #endregion

 

        #region Relations

        Task<ProductCategory?> GetCategoryWithParentAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<ProductCategory?> GetCategoryWithSubCategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default);

        #endregion

        #region Counts & Stats

        Task<int> GetCategoriesCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCategoriesCountByParentAsync(Guid parentCategoryId, bool recursive = false, CancellationToken cancellationToken = default);

        /// <summary>Count of soft-deleted categories.</summary>
        Task<int> GetSoftDeletedCategoriesCountAsync(CancellationToken cancellationToken = default); 

        #endregion
    }
}
