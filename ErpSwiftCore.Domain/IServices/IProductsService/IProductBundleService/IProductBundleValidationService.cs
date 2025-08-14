using ErpSwiftCore.Domain.Entities.EntityProduct; 
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService
{
  public interface IProductBundleValidationService
    {
        Task<bool> BundleExistsByIdAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<bool> ExistsBundleComponentAsync(Guid parentProductId, Guid componentProductId, Guid? excludeBundleId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsCircularDependencyAsync(Guid parentProductId, Guid componentProductId, CancellationToken cancellationToken = default);
        Task<bool> IsValidParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default);
        Task<bool> IsValidComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default);
        Task<bool> IsValidUnitAsync(Guid unitOfMeasurementId, CancellationToken cancellationToken = default);
        Task<bool> IsQuantityPositiveAsync(decimal quantity, CancellationToken cancellationToken = default);
        Task<bool> AreDistinctProductsAsync(Guid parentProductId, Guid componentProductId, CancellationToken cancellationToken = default);
        Task<bool> ValidateBundleAsync(ProductBundle bundle, CancellationToken cancellationToken = default);
    }
}