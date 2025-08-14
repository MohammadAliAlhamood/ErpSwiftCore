using ErpSwiftCore.Domain.Entities.EntityProduct; 
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService
{
    public interface IProductPriceQueryService
    {
        #region Single Retrieval
        Task<ProductPrice?> GetPriceByIdAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetLatestPriceByProductAsync(Guid productId, string priceType, CancellationToken cancellationToken = default);
        Task<ProductPrice?> GetSoftDeletedPriceByIdAsync(Guid priceId, CancellationToken cancellationToken = default);
        #endregion

        #region Bulk / Advanced Retrieval
        Task<IReadOnlyList<ProductPrice>> GetAllPricesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetSoftDeletedPricesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetPricesByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetPricesByTypeAsync(string priceType, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetPricesByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductPrice>> GetPricesByIdsAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);
        #endregion

        #region Relations
        Task<ProductPrice?> GetPriceWithProductAsync(Guid priceId, CancellationToken cancellationToken = default);
        #endregion
        #region Counts & Stats
        Task<int> GetPricesCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetPricesCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> GetPricesCountByTypeAsync(string priceType, CancellationToken cancellationToken = default);
        Task<int> GetSoftDeteltedPricesCountAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}
