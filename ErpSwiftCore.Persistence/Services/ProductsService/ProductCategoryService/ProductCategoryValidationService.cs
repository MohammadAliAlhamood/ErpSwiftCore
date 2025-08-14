using System;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductCategoryService
{
    public class ProductCategoryValidationService : IProductCategoryValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductCategoryValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CategoryExistsByIdAsync(Guid categoryId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductCategory.ExistsByIdAsync(categoryId, cancellationToken);

        public async Task<bool> CategoryExistsByNameAsync(
            string name,
            Guid? parentCategoryId = null,
            Guid? excludeCategoryId = null,
            CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductCategory.ExistsByNameAsync(name, parentCategoryId, excludeCategoryId, cancellationToken);

        public async Task<bool> IsCategoryNameUniqueAsync(
            string name,
            Guid? parentCategoryId = null,
            Guid? excludeCategoryId = null,
            CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductCategory.IsNameUniqueAsync(name, parentCategoryId, excludeCategoryId, cancellationToken);

        public async Task<bool> IsValidParentCategoryAsync(
            Guid? parentCategoryId,
            Guid? excludeCategoryId = null,
            CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductCategory.IsValidParentCategoryAsync(parentCategoryId, excludeCategoryId, cancellationToken);

        public async Task<bool> ExistsCircularDependencyAsync(
            Guid categoryId,
            Guid? parentCategoryId,
            CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductCategory.ExistsCircularDependencyAsync(categoryId, parentCategoryId, cancellationToken);

        // Validate that the category name is non-empty and within allowed length
        public Task<bool> IsNameValidAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            const int maxLength = 100;
            return Task.FromResult(name.Trim().Length <= maxLength);
        }

        // Comprehensive validation for creating/updating a category
        public async Task<bool> ValidateCategoryAsync(
            ProductCategory category,
            CancellationToken cancellationToken = default)
        {
            if (category == null)
                return false;

            // Name validity
            if (!await IsNameValidAsync(category.Name ?? string.Empty, cancellationToken))
                return false;

            // Unique name within same parent
            if (!await IsCategoryNameUniqueAsync(
                    category.Name!,
                    category.ParentCategoryId,
                    category.ID,
                    cancellationToken))
                return false;

            // Parent category validity
            if (category.ParentCategoryId.HasValue)
            {
                if (!await IsValidParentCategoryAsync(
                        category.ParentCategoryId,
                        category.ID,
                        cancellationToken))
                    return false;

                // No circular dependency
                if (await ExistsCircularDependencyAsync(
                        category.ID,
                        category.ParentCategoryId,
                        cancellationToken))
                    return false;
            }

            return true;
        }
    }
}
