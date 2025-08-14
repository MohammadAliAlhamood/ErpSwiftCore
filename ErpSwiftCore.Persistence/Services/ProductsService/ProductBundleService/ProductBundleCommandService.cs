using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductBundleService
{
    public class ProductBundleCommandService : IProductBundleCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IProductBundleValidationService _validation;

        public ProductBundleCommandService(IMultiTenantUnitOfWork unitOfWork, IProductBundleValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }

        public async Task<Guid> CreateBundleAsync(ProductBundle bundle, CancellationToken cancellationToken = default)
        {
            // Validate bundle according to shared validation logic
            if (!await _validation.ValidateBundleAsync(bundle, cancellationToken))
                throw new InvalidOperationException("Bundle validation failed.");

            return await _unitOfWork.ProductBundle.CreateAsync(bundle, cancellationToken);
        }

        public async Task<bool> UpdateBundleAsync(ProductBundle bundle, CancellationToken cancellationToken = default)
        {
            // Reuse validation, excluding current bundle from duplicate check
            if (!await _validation.ValidateBundleAsync(bundle, cancellationToken))
                throw new InvalidOperationException("Bundle validation failed.");

            return await _unitOfWork.ProductBundle.UpdateAsync(bundle, cancellationToken);
        }

        public async Task<IEnumerable<Guid>> AddBundlesRangeAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default)
        {
            // Validate each bundle
            foreach (var bundle in bundles)
            {
                if (!await _validation.ValidateBundleAsync(bundle, cancellationToken))
                    throw new InvalidOperationException($"Validation failed for bundle {{bundle.ID}}.");
            }

            return await _unitOfWork.ProductBundle.AddRangeAsync(bundles, cancellationToken);
        }

        public async Task<bool> DeleteBundleAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.DeleteAsync(bundleId, cancellationToken);

        public async Task<bool> DeleteBundlesRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.DeleteRangeAsync(bundleIds, cancellationToken);

        public async Task<bool> DeleteAllBundlesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.DeleteAllAsync(cancellationToken);

        public async Task<bool> RestoreBundleAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.RestoreAsync(bundleId, cancellationToken);

        public async Task<bool> RestoreBundlesRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.RestoreRangeAsync(bundleIds, cancellationToken);

        public async Task<bool> RestoreAllBundlesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.RestoreAllAsync(cancellationToken);

        public async Task<bool> SoftDeleteBundleAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.SoftDeleteAsync(bundleId, cancellationToken);

        public async Task<bool> SoftDeleteBundlesRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.SoftDeleteRangeAsync(bundleIds, cancellationToken);
        public async Task<bool> SoftDeleteAllBundlesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.SoftDeleteAllAsync(cancellationToken);

        public async Task<int> BulkImportBundlesAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.BulkImportAsync(bundles, cancellationToken);

        public async Task<int> BulkDeleteBundlesAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.BulkDeleteAsync(bundleIds, cancellationToken);

        public async Task<int> BulkSoftDeleteBundlesAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.BulkDeleteAsync(bundleIds, cancellationToken);

        public async Task<int> BulkRestoreBundlesAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.RestoreRangeAsync(bundleIds, cancellationToken)
                ? bundleIds.Count()
                : 0;


    }
}
