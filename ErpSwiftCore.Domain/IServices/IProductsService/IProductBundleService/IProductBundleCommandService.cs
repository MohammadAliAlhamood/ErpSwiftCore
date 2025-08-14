using ErpSwiftCore.Domain.Entities.EntityProduct; 
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService
{
    public interface IProductBundleCommandService
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateBundleAsync(ProductBundle bundle, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddBundlesRangeAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default);
        Task<bool> UpdateBundleAsync(ProductBundle bundle, CancellationToken cancellationToken = default);

        // -------------------- [Soft‑Delete Operations] --------------------
        Task<bool> SoftDeleteBundleAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteBundlesRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllBundlesAsync(CancellationToken cancellationToken = default);
        Task<bool> DeleteBundleAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<bool> DeleteBundlesRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllBundlesAsync(CancellationToken cancellationToken = default);

        // -------------------- [Restore Operations] --------------------
        Task<bool> RestoreBundleAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<bool> RestoreBundlesRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllBundlesAsync(CancellationToken cancellationToken = default);

        
        // -------------------- [Bulk Operations] --------------------
        /// <summary>استيراد مجموعة كبيرة من الباقات دفعة واحدة.</summary>
        Task<int> BulkImportBundlesAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteBundlesAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        Task<int> BulkSoftDeleteBundlesAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        Task<int> BulkRestoreBundlesAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
    }
}
