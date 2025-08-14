using ErpSwiftCore.Domain.Entities.EntityProduct; 
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductService
{
    public interface IProductQueryService
    {
        #region Single Retrieval
        Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetProductBySkuAsync(string sku, CancellationToken cancellationToken = default);
        Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<Product?> GetProductByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
        Task<Product?> GetSoftDeletedProductByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        #endregion
        #region Bulk / Advanced Retrieval
        Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<Product>> GetSoftDeletedProductsAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetProductsByBundleAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetProductsByTaxAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetProductsByUnitAsync(Guid unitId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetProductsByPriceTypeAsync(string priceType, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetProductsByProductTypeAsync(Guid productTypeId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetProductsByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
        #endregion
            #region Relations
        Task<Product?> GetProductWithBundlesAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetProductWithPricesAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetProductWithTaxesAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetProductWithUnitsAsync(Guid productId, CancellationToken cancellationToken = default);
        #endregion
        #region Counts & Stats
        Task<int> GetProductsCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetProductsCountByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<int> GetProductsCountByTypeAsync(Guid productTypeId, CancellationToken cancellationToken = default);
        Task<int> GetSoftDeletedProductsCountAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}
