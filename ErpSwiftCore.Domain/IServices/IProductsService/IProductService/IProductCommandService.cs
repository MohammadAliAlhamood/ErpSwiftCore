using ErpSwiftCore.Domain.Entities.EntityProduct;
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductService
{
    public interface IProductCommandService
    { 
        Task<Guid> CreateProductAsync(Product product, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddProductsRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);
        Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken = default); 
        Task<bool> SoftDeleteProductAsync(Guid productId, CancellationToken cancellationToken = default); 
        Task<bool> SoftDeleteProductsRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default); 
        Task<bool> SoftDeleteAllProductsAsync(CancellationToken cancellationToken = default); 
        Task<bool> DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default); 
        Task<bool> DeleteProductsRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default); 
        Task<bool> DeleteAllProductsAsync(CancellationToken cancellationToken = default); 
        Task<bool> RestoreProductAsync(Guid productId, CancellationToken cancellationToken = default); 
        Task<bool> RestoreProductsRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default); 
        Task<bool> RestoreAllProductsAsync(CancellationToken cancellationToken = default); 
       // -------------------- [Bulk Operations] -------------------- 
        Task<int> BulkImportProductsAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default); 
        Task<int> BulkUpdateStockAsync(IEnumerable<(Guid productId, int quantity)> stockUpdates, CancellationToken cancellationToken = default); 
        Task<int> BulkDeleteProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default); 
        Task<int> BulkSoftDeleteProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default); 
        Task<int> BulkRestoreProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
    }
}
