using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductCategoryService
{
    public class ProductCategoryQueryService : IProductCategoryQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductCategoryQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductCategory?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetByIdAsync(categoryId, cancellationToken);

        public async Task<ProductCategory?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetByNameAsync(name, cancellationToken);

        public async Task<ProductCategory?> GetSoftDeletedCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetSoftDeletedByIdAsync(categoryId, cancellationToken);

        public async Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetAllAsync(cancellationToken);
         
        public async Task<IReadOnlyList<ProductCategory>> GetSoftDeletedCategoriesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetSoftDeletedAsync(cancellationToken);

        public async Task<IReadOnlyList<ProductCategory>> GetAllSoftDeletedCategoriesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetAllSoftDeletedAsync(cancellationToken);

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesByParentAsync(Guid parentCategoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetByParentAsync(parentCategoryId, cancellationToken);

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesByIdsAsync(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetByIdsAsync(categoryIds, cancellationToken);

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesByFilterAsync(Expression<Func<ProductCategory, bool>> filter, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetByFilterAsync(filter, cancellationToken);

        public async Task<IReadOnlyList<ProductCategory>> GetAllDescendantCategoriesAsync(Guid parentCategoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetAllDescendantsAsync(parentCategoryId, cancellationToken);

        public async Task<IReadOnlyList<ProductCategory>> GetCategoryHierarchyAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetCategoryHierarchyAsync(categoryId, cancellationToken);

       
        public async Task<ProductCategory?> GetCategoryWithParentAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetWithParentAsync(categoryId, cancellationToken);

        public async Task<ProductCategory?> GetCategoryWithSubCategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetWithSubCategoriesAsync(categoryId, cancellationToken);

       
          

        public async Task<int> GetSoftDeletedCategoriesCountAsync(CancellationToken cancellationToken = default)
        {
            var allSoftDeleted = await _unitOfWork.ProductCategory.GetAllSoftDeletedAsync(cancellationToken);
            return allSoftDeleted.Count;
        }

   

        public async Task<int> GetCategoriesCountAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.CountAsync();

        public async Task<int> GetCategoriesCountByParentAsync(Guid parentCategoryId, bool includeDescendants = false, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.GetCountByParentAsync(parentCategoryId, includeDescendants, cancellationToken);

       

    }
}