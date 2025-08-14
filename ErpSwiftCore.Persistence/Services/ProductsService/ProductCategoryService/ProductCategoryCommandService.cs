using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductCategoryService
{
    public class ProductCategoryCommandService : IProductCategoryCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IProductCategoryValidationService _validation;

        public ProductCategoryCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            IProductCategoryValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }

        public async Task<Guid> CreateCategoryAsync(
            ProductCategory category,
            CancellationToken cancellationToken = default)
        {
            // Comprehensive validation
            if (!await _validation.ValidateCategoryAsync(category, cancellationToken))
                throw new InvalidOperationException("Category validation failed.");

            return await _unitOfWork.ProductCategory.CreateAsync(category, cancellationToken);
        }

        public async Task<bool> UpdateCategoryAsync(
            ProductCategory category,
            CancellationToken cancellationToken = default)
        {
            // Exclude self in circular check
            if (!await _validation.ValidateCategoryAsync(category, cancellationToken))
                throw new InvalidOperationException("Category validation failed.");

            return await _unitOfWork.ProductCategory.UpdateAsync(category, cancellationToken);
        }

        public async Task<IEnumerable<Guid>> AddCategoriesRangeAsync(
            IEnumerable<ProductCategory> categories,
            CancellationToken cancellationToken = default)
        {
            foreach (var category in categories)
            {
                if (!await _validation.ValidateCategoryAsync(category, cancellationToken))
                    throw new InvalidOperationException($"Validation failed for category '{category.Name}'.");
            }

            return await _unitOfWork.ProductCategory.AddRangeAsync(categories, cancellationToken);
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.DeleteAsync(categoryId, cancellationToken);

        public async Task<bool> DeleteCategoriesRangeAsync(
            IEnumerable<Guid> categoryIds,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.DeleteRangeAsync(categoryIds, cancellationToken);

        public async Task<bool> DeleteAllCategoriesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.DeleteAllAsync(cancellationToken);

        public async Task<bool> RestoreCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.RestoreAsync(categoryId, cancellationToken);

        public async Task<bool> RestoreCategoriesRangeAsync(
            IEnumerable<Guid> categoryIds,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.RestoreRangeAsync(categoryIds, cancellationToken);

        public async Task<bool> RestoreAllCategoriesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.RestoreAllAsync(cancellationToken);

        // Implement soft delete operations
        public async Task<bool> SoftDeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.SoftDeleteAsync(categoryId, cancellationToken);

        public async Task<bool> SoftDeleteCategoriesRangeAsync(
            IEnumerable<Guid> categoryIds,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.SoftDeleteRangeAsync(categoryIds, cancellationToken);

        public async Task<bool> SoftDeleteAllCategoriesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.SoftDeleteAllAsync(cancellationToken);

        public async Task<int> BulkImportCategoriesAsync(
            IEnumerable<ProductCategory> categories,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.BulkImportAsync(categories, cancellationToken);

        public async Task<int> BulkDeleteCategoriesAsync(
            IEnumerable<Guid> categoryIds,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductCategory.BulkDeleteAsync(categoryIds, cancellationToken);

           }
}
