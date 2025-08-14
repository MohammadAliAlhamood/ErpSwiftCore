using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.Enums;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IProductRepositories
{
    /// <summary>
    /// Repository interface for managing product entities.
    /// Supports advanced CRUD, archive/restore, state, paging, search,
    /// validation, analytics, relations, and bulk operations.
    /// </summary>
    public interface IProductRepository : IMultiTenantRepository<Product>
    {
        Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
         Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<Product?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<Product> Products, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<Product> Products, int TotalCount)> GetPagedByCategoryAsync(Guid categoryId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
         Task<IReadOnlyList<Product>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByCodeAsync(string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsByBarcodeAsync(string barcode, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsDuplicateAsync(string name, string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> IsNameUniqueAsync(string name, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidCategoryAsync(Guid? categoryId, Guid? excludeProductId = null, CancellationToken cancellationToken = default);
        Task<Product?> GetWithCategoryAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetWithPricesAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetWithUnitConversionsAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetWithTaxesAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Product?> GetWithBundlesAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<int> BulkImportAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetByFilterAsync(Expression<Func<Product, bool>> filter, CancellationToken cancellationToken = default);
        Task<Product?> GetSoftDeletedByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetByTypeAsync(ProductType productType, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<Product> Products, int TotalCount)> GetPagedByTypeAsync(ProductType productType, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        public Task<int> GetCountByTypeAsync(ProductType productType, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken);
        Task<bool> SoftDeleteAsync(Guid productId, CancellationToken cancellationToken);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
    }
}