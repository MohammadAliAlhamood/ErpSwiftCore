using ErpSwiftCore.Domain.Entities.EntityProduct;
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService
{
    public interface IProductBundleQueryService
    {
        #region Single Retrieval

        Task<ProductBundle?> GetBundleByIdAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<ProductBundle?> GetSoftDeletedBundleByIdAsync(Guid bundleId, CancellationToken cancellationToken = default);

        #endregion

        #region Bulk / Advanced Retrieval
        Task<IReadOnlyList<ProductBundle>> GetAllBundlesAsync(CancellationToken cancellationToken = default);
         Task<IReadOnlyList<ProductBundle>> GetSoftDeletedBundlesAsync(CancellationToken cancellationToken = default);
         Task<IReadOnlyList<ProductBundle>> GetBundlesByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetBundlesByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetBundlesByUnitAsync(Guid unitOfMeasurementId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetBundlesByIdsAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
 
        #endregion

   
        #region Relations
        Task<ProductBundle?> GetBundleWithParentProductAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<ProductBundle?> GetBundleWithComponentProductAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<ProductBundle?> GetBundleWithUnitAsync(Guid bundleId, CancellationToken cancellationToken = default);
        #endregion

        #region Counts & Stats
        Task<int> GetBundlesCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetBundlesCountByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default);
        Task<int> GetBundlesCountByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default);
        Task<int> GetSoftDeletedBundlesCountAsync(CancellationToken cancellationToken = default);
         #endregion
    }
}
