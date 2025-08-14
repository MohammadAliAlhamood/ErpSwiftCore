using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using ErpSwiftCore.Domain.IRepositories;
namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductBundleService
{
    public class ProductBundleQueryService : IProductBundleQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductBundleQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Single Retrieval

        public async Task<ProductBundle?> GetBundleByIdAsync(Guid bundleId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetByIdAsync(bundleId, cancellationToken);

        public async Task<ProductBundle?> GetSoftDeletedBundleByIdAsync(Guid bundleId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetSoftDeletedByIdAsync(bundleId, cancellationToken);

        #endregion

        #region Bulk / Advanced Retrieval

        public async Task<IReadOnlyList<ProductBundle>> GetAllBundlesAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetAllAsync(cancellationToken);
         
        public async Task<IReadOnlyList<ProductBundle>> GetSoftDeletedBundlesAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetAllSoftDeletedAsync(cancellationToken);

        public async Task<IReadOnlyList<ProductBundle>> GetBundlesByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetByParentProductAsync(parentProductId, cancellationToken);

        public async Task<IReadOnlyList<ProductBundle>> GetBundlesByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetByComponentProductAsync(componentProductId, cancellationToken);

        public async Task<IReadOnlyList<ProductBundle>> GetBundlesByUnitAsync(Guid unitOfMeasurementId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetByUnitAsync(unitOfMeasurementId, cancellationToken);

        public async Task<IReadOnlyList<ProductBundle>> GetBundlesByIdsAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetByIdsAsync(bundleIds, cancellationToken);

 
        #endregion



        #region Relations

        public async Task<ProductBundle?> GetBundleWithParentProductAsync(Guid bundleId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetWithParentProductAsync(bundleId, cancellationToken);
        public async Task<ProductBundle?> GetBundleWithComponentProductAsync(Guid bundleId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetWithComponentProductAsync(bundleId, cancellationToken);
        public async Task<ProductBundle?> GetBundleWithUnitAsync(Guid bundleId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetWithUnitAsync(bundleId, cancellationToken);

        #endregion

        #region Counts & Stats
        public async Task<int> GetBundlesCountAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetCountAsync(cancellationToken);
        public async Task<int> GetBundlesCountByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetCountByParentProductAsync(parentProductId, cancellationToken);
        public async Task<int> GetBundlesCountByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductBundle.GetCountByComponentProductAsync(componentProductId, cancellationToken);         
        public async Task<int> GetSoftDeletedBundlesCountAsync(CancellationToken cancellationToken = default)
        {
            var allSoftDeleted = await _unitOfWork.ProductBundle.GetAllSoftDeletedAsync(cancellationToken);
            return allSoftDeleted.Count;
        } 
        #endregion
    }
}
