using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductService
{
    public class ProductCommandService : IProductCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IProductValidationService _validation;

        public ProductCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            IProductValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }

        public async Task<Guid> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            if (!await _validation.IsNameValidAsync(product.Name, cancellationToken))
                throw new ArgumentException("اسم المنتج غير صالح.");
            
            if (!await _validation.IsProductNameUniqueAsync(product.Name, null, cancellationToken))
                throw new InvalidOperationException("اسم المنتج مستخدم مسبقاً.");
            
            if (!await _validation.IsProductCodeUniqueAsync(product.Code, null, cancellationToken))
                throw new InvalidOperationException("كود المنتج مستخدم مسبقاً.");
            
            if (!await _validation.IsSkuValidAsync(product.SKU, cancellationToken) ||
                !await _validation.IsSkuUniqueAsync(product.SKU, null, cancellationToken))
                throw new InvalidOperationException("SKU غير صالح أو مستخدم مسبقاً.");
            
            if (!await _validation.IsBarcodeValidAsync(product.Barcode, cancellationToken) ||
                !await _validation.IsBarcodeUniqueAsync(product.Barcode, null, cancellationToken))
                throw new InvalidOperationException("الباركود غير صالح أو مستخدم مسبقاً.");
            
            if (product.CategoryId.HasValue &&
                !await _validation.IsValidCategoryAsync(product.CategoryId.Value, cancellationToken))
                throw new InvalidOperationException("الفئة غير صالحة.");

            return await _unitOfWork.Product.CreateAsync(product, cancellationToken);
        }

        public async Task<IEnumerable<Guid>> AddProductsRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var prod in products)
                ids.Add(await CreateProductAsync(prod, cancellationToken));
            return ids;
        }

        public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            if (!await _validation.ProductExistsByIdAsync(product.ID, cancellationToken))
                throw new InvalidOperationException("المنتج غير موجود.");
            if (!await _validation.IsNameValidAsync(product.Name, cancellationToken) ||
                !await _validation.IsProductNameUniqueAsync(product.Name, product.ID, cancellationToken))
                throw new InvalidOperationException("اسم المنتج غير صالح أو مستخدم مسبقاً.");
            if (!await _validation.IsProductCodeUniqueAsync(product.Code, product.ID, cancellationToken))
                throw new InvalidOperationException("كود المنتج مستخدم مسبقاً.");
            if (!await _validation.IsSkuValidAsync(product.SKU, cancellationToken) ||
                !await _validation.IsSkuUniqueAsync(product.SKU, product.ID, cancellationToken))
                throw new InvalidOperationException("SKU غير صالح أو مستخدم مسبقاً.");
            if (!await _validation.IsBarcodeValidAsync(product.Barcode, cancellationToken) ||
                !await _validation.IsBarcodeUniqueAsync(product.Barcode, product.ID, cancellationToken))
                throw new InvalidOperationException("الباركود غير صالح أو مستخدم مسبقاً.");

            if (product.CategoryId.HasValue &&
                !await _validation.IsValidCategoryAsync(product.CategoryId.Value, cancellationToken))
                throw new InvalidOperationException("الفئة غير صالحة.");

            return await _unitOfWork.Product.UpdateAsync(product, cancellationToken);
        }

        public async Task<bool> DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.DeleteAsync(productId, cancellationToken);

        public async Task<bool> DeleteProductsRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.DeleteRangeAsync(productIds, cancellationToken);

        public async Task<bool> DeleteAllProductsAsync(CancellationToken cancellationToken = default)
        {
            var all = await _unitOfWork.Product.GetAllAsync(cancellationToken);
            return await DeleteProductsRangeAsync(all.Select(p => p.ID), cancellationToken);
        }

        public async Task<bool> RestoreProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.RestoreAsync(productId, cancellationToken);

        public async Task<bool> RestoreProductsRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.RestoreRangeAsync(productIds, cancellationToken);

        public async Task<bool> RestoreAllProductsAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await _unitOfWork.Product.GetAllSoftDeletedAsync(cancellationToken);
            return await RestoreProductsRangeAsync(SoftDeleted.Select(p => p.ID), cancellationToken);
        }

        public async Task<int> BulkImportProductsAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default)
        {
            int count = 0;
            foreach (var prod in products)
            {
                await CreateProductAsync(prod, cancellationToken);
                count++;
            }
            return count;
        }

        public async Task<int> BulkDeleteProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.BulkDeleteAsync(productIds, cancellationToken);

        public async Task<int> BulkUpdateStockAsync(IEnumerable<(Guid productId, int quantity)> stockUpdates, CancellationToken cancellationToken = default)
        {
            int updated = 0;
            foreach (var (productId, quantity) in stockUpdates)
            {
                var product = await _unitOfWork.Product.GetByIdAsync(productId, cancellationToken);
                if (product == null) continue;
                 
                await _unitOfWork.Product.UpdateAsync(product, cancellationToken);
                updated++;
            }
            return updated;
        }

      
        // -------------------- Soft Delete / Restore --------------------

        public async Task<bool> SoftDeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.ProductExistsByIdAsync(productId, cancellationToken))
                throw new InvalidOperationException("المنتج غير موجود.");
            return await _unitOfWork.Product.SoftDeleteAsync(productId, cancellationToken);
        }

        public async Task<bool> SoftDeleteProductsRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.SoftDeleteRangeAsync(productIds, cancellationToken);

        public async Task<bool> SoftDeleteAllProductsAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.SoftDeleteAllAsync(cancellationToken);

        public async Task<int> BulkSoftDeleteProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            var succeeded = await _unitOfWork.Product.SoftDeleteRangeAsync(productIds, cancellationToken);
            return succeeded ? productIds.Count() : 0;
        }

        public async Task<int> BulkRestoreProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            var succeeded = await _unitOfWork.Product.RestoreRangeAsync(productIds, cancellationToken);
            return succeeded ? productIds.Count() : 0;
        }

        public Task<bool> SetProductsActiveStatusesAsync(bool isActive, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
