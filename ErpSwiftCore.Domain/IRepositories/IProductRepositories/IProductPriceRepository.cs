using ErpSwiftCore.Domain.Entities.EntityProduct;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Domain.IRepositories.IProductRepositories
{
    public interface IProductPriceRepository : IMultiTenantRepository<ProductPrice>
    {
        Task<Guid> CreateAsync(ProductPrice price, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default);
        Task<int> BulkImportAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetByIdAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetLatestByProductAsync(Guid productId, ProductPriceType priceType, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(ProductPrice price, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<bool> ExistsForProductAsync(Guid productId, ProductPriceType priceType, Guid? excludePriceId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> IsValidCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);
        Task<bool> IsValidPriceTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetWithProductAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetWithCurrencyAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetSoftDeletedByIdAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetByTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetByIdsAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetByFilterAsync(Expression<Func<ProductPrice, bool>> filter, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> GetCountByTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetLatestByProductAsync(Guid productId, string priceType, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetByTypeAsync(string priceType, CancellationToken cancellationToken = default);
        Task<bool> ExistsForProductAsync(Guid productId, string priceType, Guid? excludePriceId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidPriceTypeAsync(string priceType, CancellationToken cancellationToken = default);
        Task<int> GetCountByTypeAsync(string priceType, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);
    }
}