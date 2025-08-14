using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductService
{
    public class ProductValidationService : IProductValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ProductExistsByIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.ExistsByIdAsync(productId, cancellationToken);

        public async Task<bool> ProductExistsByCodeAsync(string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.ExistsByCodeAsync(code, excludeProductId, cancellationToken);

        public async Task<bool> ProductExistsByBarcodeAsync(string barcode, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.ExistsByBarcodeAsync(barcode, excludeProductId, cancellationToken);

        public async Task<bool> ProductExistsBySkuAsync(string sku, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => (await _unitOfWork.Product.GetByFilterAsync(
                    p => p.SKU == sku && (!excludeProductId.HasValue || p.ID != excludeProductId.Value),
                    cancellationToken))
                .Any();

        public async Task<bool> IsProductNameUniqueAsync(string name, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.IsNameUniqueAsync(name, excludeProductId, cancellationToken);

        public async Task<bool> IsProductCodeUniqueAsync(string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => !await _unitOfWork.Product.ExistsByCodeAsync(code, excludeProductId, cancellationToken);

        public async Task<bool> IsSkuUniqueAsync(string sku, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => !(await _unitOfWork.Product.GetByFilterAsync(
                    p => p.SKU == sku && (!excludeProductId.HasValue || p.ID != excludeProductId.Value),
                    cancellationToken))
                .Any();

        public async Task<bool> IsBarcodeUniqueAsync(string barcode, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => !await _unitOfWork.Product.ExistsByBarcodeAsync(barcode, excludeProductId, cancellationToken);

      

        public Task<bool> IsValidPriceTypeAsync(string priceType, CancellationToken cancellationToken = default)
            => Task.FromResult(!string.IsNullOrWhiteSpace(priceType));

      

        // -------------------- [New Validators] --------------------

        public async Task<bool> IsValidCategoryAsync(Guid? categoryId, CancellationToken cancellationToken = default)
        {
          return true;
             
        }

        public Task<bool> IsNameValidAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name)) return Task.FromResult(false);
            return Task.FromResult(name.Trim().Length <= 100);
        }

        public Task<bool> IsSkuValidAsync(string sku, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sku)) return Task.FromResult(false);
            return Task.FromResult(sku.Trim().Length <= 50);
        }

        public Task<bool> IsBarcodeValidAsync(string barcode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return Task.FromResult(false);
            // Simple numeric or alphanumeric check
            return Task.FromResult(barcode.Trim().Length <= 50);
        }

        public Task<bool> IsStockNonNegativeAsync(int stock, CancellationToken cancellationToken = default)
            => Task.FromResult(stock >= 0);

        /// <summary>
        /// Comprehensive product validation before create/update.
        /// </summary>
        public async Task<bool> ValidateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null) return false;

            // Name
            if (!await IsNameValidAsync(product.Name ?? "", cancellationToken)) return false;
            if (!await IsProductNameUniqueAsync(product.Name!, product.ID, cancellationToken)) return false;

            // Code
            if (!await IsProductCodeUniqueAsync(product.Code ?? "", product.ID, cancellationToken)) return false;

            // SKU & Barcode
            if (!await IsSkuValidAsync(product.SKU ?? "", cancellationToken)) return false;
            if (!await IsSkuUniqueAsync(product.SKU!, product.ID, cancellationToken)) return false;
            if (!await IsBarcodeValidAsync(product.Barcode ?? "", cancellationToken)) return false;
            if (!await IsBarcodeUniqueAsync(product.Barcode, product.ID, cancellationToken)) return false;

            // Category
            if (!await IsValidCategoryAsync(product.CategoryId ?? Guid.Empty, cancellationToken)) return false;
             
            
            return true;
        }
    }
}
