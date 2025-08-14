using System;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductBundleService
{
    public class ProductBundleValidationService : IProductBundleValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductBundleValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExistsBundleComponentAsync(
            Guid parentProductId,
            Guid componentProductId,
            Guid? excludeBundleId = null,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle
                .ExistsBundleComponentAsync(parentProductId, componentProductId, excludeBundleId, cancellationToken);

        public async Task<bool> ExistsCircularDependencyAsync(
            Guid parentProductId,
            Guid componentProductId,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.ExistsCircularDependencyAsync(parentProductId, componentProductId, cancellationToken);

        public async Task<bool> IsValidParentProductAsync(
            Guid parentProductId,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.IsValidParentProductAsync(parentProductId, cancellationToken);

        public async Task<bool> IsValidComponentProductAsync(
            Guid componentProductId,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.IsValidComponentProductAsync(componentProductId, cancellationToken);

        public async Task<bool> IsValidUnitAsync(
            Guid unitOfMeasurementId,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.IsValidUnitAsync(unitOfMeasurementId, cancellationToken);

        public async Task<bool> BundleExistsByIdAsync(
            Guid bundleId,
            CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductBundle.ExistsByIdAsync(bundleId, cancellationToken);

        // تنفيذ التحقق أن الكمية موجبة
        public Task<bool> IsQuantityPositiveAsync(
            decimal quantity,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(quantity > 0);
        }

        // تنفيذ التحقق أن المنتجات مختلفة (لا توجد self-bundle)
        public Task<bool> AreDistinctProductsAsync(
            Guid parentProductId,
            Guid componentProductId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(parentProductId != componentProductId);
        }

        // تنفيذ التحقق الشامل لبند التجميع
        public async Task<bool> ValidateBundleAsync(
            ProductBundle bundle,
            CancellationToken cancellationToken = default)
        {
            if (bundle == null) return false;

            // تحقق من أنك لا تنشئ حلقة دائرية أولاً
            if (await ExistsCircularDependencyAsync(bundle.ParentProductId, bundle.ComponentProductId, cancellationToken))
                return false;
            // تحقق من أن المنتجات مختلفة
            if (!await AreDistinctProductsAsync(bundle.ParentProductId, bundle.ComponentProductId, cancellationToken))
                return false;
            // تحقق من وجود الوالد/المكون في النظام
            if (!await IsValidParentProductAsync(bundle.ParentProductId, cancellationToken))
                return false;
            if (!await IsValidComponentProductAsync(bundle.ComponentProductId, cancellationToken))
                return false;

            // تحقق من صلاحية الوحدة إن وُجدت
            if (bundle.UnitOfMeasurementId.HasValue)
            {
                if (!await IsValidUnitAsync(bundle.UnitOfMeasurementId.Value, cancellationToken))
                    return false;
            }

            // تحقق من الكمية
            if (!await IsQuantityPositiveAsync(bundle.Quantity, cancellationToken))
                return false;

            // تحقق من عدم وجود نفس المكون في بنود أخرى (مثال على استخدام exclude في حالة تحديث)
            if (await ExistsBundleComponentAsync(
                    bundle.ParentProductId,
                    bundle.ComponentProductId,
                    bundle.ID,
                    cancellationToken))
                return false;

            return true;
        }
    }
}
