using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductService
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default)
          => await _unitOfWork.Product.GetByIdAsync(productId, cancellationToken);
        public async Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetByCodeAsync(code, cancellationToken);
        public async Task<Product?> GetProductByBarcodeAsync(string barcode, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetByBarcodeAsync(barcode, cancellationToken);
        public async Task<Product?> GetProductBySkuAsync(string sku, CancellationToken cancellationToken = default)
        {
            var products = await _unitOfWork.Product.GetAllAsync(p => p.SKU == sku, cancellationToken);
            return products.FirstOrDefault();
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetAllAsync(cancellationToken)  ;
 
        public async Task<IReadOnlyList<Product>> GetSoftDeletedProductsAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetAllSoftDeletedAsync(cancellationToken);
        public async Task<IReadOnlyList<Product>> GetAllSoftDeletedProductsAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetAllSoftDeletedAsync(cancellationToken);
        public async Task<Product?> GetSoftDeletedProductByIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetSoftDeletedByIdAsync(productId, cancellationToken);
        public async Task<IReadOnlyList<Product>> GetProductsByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetByIdsAsync(productIds, cancellationToken);
        public async Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetByCategoryAsync(categoryId, cancellationToken);
        public async Task<IReadOnlyList<Product>> GetProductsByProductTypeAsync(Guid productTypeId, CancellationToken cancellationToken = default)
        {
            // Assuming ProductType is an enum and productTypeId is its value
            var type = (ProductType)Enum.ToObject(typeof(ProductType), productTypeId);
            return await _unitOfWork.Product.GetByTypeAsync(type, cancellationToken);
        }
    
        
        public async Task<Product?> GetProductWithPricesAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetWithPricesAsync(productId, cancellationToken);
        public async Task<Product?> GetProductWithTaxesAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetWithTaxesAsync(productId, cancellationToken);
        public async Task<Product?> GetProductWithUnitsAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetWithUnitConversionsAsync(productId, cancellationToken);
        public async Task<Product?> GetProductWithBundlesAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.Product.GetWithBundlesAsync(productId, cancellationToken); 
        public async Task<int> GetProductsCountAsync(CancellationToken cancellationToken = default)
        {
            var all = await _unitOfWork.Product.GetAllAsync(cancellationToken);
            return all.Count;
        }
        public async Task<int> GetProductsCountByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var products = await _unitOfWork.Product.GetByCategoryAsync(categoryId, cancellationToken);
            return products.Count;
        }
        
        public async Task<int> GetProductsCountByTypeAsync(Guid productTypeId, CancellationToken cancellationToken = default)
        {
            var type = (ProductType)Enum.ToObject(typeof(ProductType), productTypeId);
            return await _unitOfWork.Product.GetCountByTypeAsync(type, cancellationToken);
        }
        public async Task<IReadOnlyList<Product>> GetProductsByPriceTypeAsync(string priceType, CancellationToken cancellationToken = default)
        {
            var prices = await _unitOfWork.ProductPrice.GetByTypeAsync(priceType, cancellationToken);
            var productIds = prices.Select(p => p.ProductId).Distinct();
            return await _unitOfWork.Product.GetByIdsAsync(productIds, cancellationToken);
        }
        public async Task<IReadOnlyList<Product>> GetProductsByTaxAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            var tax = await _unitOfWork.ProductTax.GetByProductAsync(taxId, cancellationToken);
            var productIds = tax.Select(t => t.ProductId).Distinct();
            return await _unitOfWork.Product.GetByIdsAsync(productIds, cancellationToken);
         }
        public async Task<IReadOnlyList<Product>> GetProductsByUnitAsync(Guid unitId, CancellationToken cancellationToken = default)
        {
            var conversions = await _unitOfWork.ProductUnitConversion.GetByIdsAsync(new[] { unitId }, cancellationToken);
            var productIds = conversions.Select(c => c.ProductId).Distinct();
            return await _unitOfWork.Product.GetByIdsAsync(productIds, cancellationToken);
         }
        public async Task<IReadOnlyList<Product>> GetProductsByBundleAsync(Guid bundleId, CancellationToken cancellationToken = default)
        {
            var bundle = await _unitOfWork.ProductBundle.GetByParentProductAsync(bundleId, cancellationToken);
            var productIds = bundle.Select(b => b.ComponentProductId).Distinct();
            return await _unitOfWork.Product.GetByIdsAsync(productIds, cancellationToken);
        
        }

        // =======================
        // Soft‑Deleted / SoftDeleted
        // ============ 
        public async Task<(IReadOnlyList<Product> Products, int TotalCount)> GetProductsPagedSoftDeletedAsync(
            int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await _unitOfWork.Product.GetAllSoftDeletedAsync(cancellationToken);
            var total = SoftDeleted.Count;
            var paged = SoftDeleted
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return (paged, total);
        }
        public async Task<int> GetSoftDeletedProductsCountAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await _unitOfWork.Product.GetAllSoftDeletedAsync(cancellationToken);
            return SoftDeleted.Count;
        }
    }
}
