using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductService
{
    /// <summary>
    /// Validation service for Product entity.
    /// Ensures existence, uniqueness, referential integrity, business rules and data consistency.
    /// </summary>
    public interface IProductValidationService
    {
        #region Existence & Referential Integrity
        Task<bool> ProductExistsByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> ProductExistsBySkuAsync(string sku, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> ProductExistsByCodeAsync(string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> ProductExistsByBarcodeAsync(string barcode, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidCategoryAsync(Guid? categoryId, CancellationToken cancellationToken = default);
         #endregion
        #region Uniqueness Checks
        Task<bool> IsProductNameUniqueAsync(string name, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> IsSkuUniqueAsync(string sku, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> IsBarcodeUniqueAsync(string barcode, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> IsProductCodeUniqueAsync(string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        #endregion

        #region Business Rules Validation
        Task<bool> IsNameValidAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> IsSkuValidAsync(string sku, CancellationToken cancellationToken = default);
        Task<bool> IsBarcodeValidAsync(string barcode, CancellationToken cancellationToken = default);
        Task<bool> IsStockNonNegativeAsync(int stock, CancellationToken cancellationToken = default);
        #endregion

        #region Aggregate Validation
        Task<bool> ValidateProductAsync(Product product, CancellationToken cancellationToken = default);
        #endregion
    }
}
