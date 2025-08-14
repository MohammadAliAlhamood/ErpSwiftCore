using System;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductUnitConversionService
{
    public class ProductUnitConversionValidationService : IProductUnitConversionValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductUnitConversionValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<bool> UnitConversionExistsByIdAsync(Guid conversionId, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductUnitConversion.ExistsByIdAsync(conversionId, cancellationToken);
        public Task<bool> UnitConversionExistsForProductAsync(Guid productId, Guid fromUnitId, Guid toUnitId, Guid? excludeConversionId = null, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductUnitConversion.ExistsDuplicateAsync(productId, fromUnitId, toUnitId, excludeConversionId, cancellationToken);
        public Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductUnitConversion.IsValidProductAsync(productId, cancellationToken);
        public Task<bool> IsValidUnitAsync(Guid unitId, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductUnitConversion.IsValidUnitAsync(unitId, cancellationToken);
        public Task<bool> IsValidConversionRateAsync(decimal conversionRate, CancellationToken cancellationToken = default) =>
            Task.FromResult(conversionRate > 0);
        public Task<bool> IsValidFactorAsync(decimal factor, CancellationToken cancellationToken = default) =>
            Task.FromResult(factor > 0);
        public Task<bool> IsNotSelfConversionAsync(Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default)
            => Task.FromResult(fromUnitId != toUnitId);
        public Task<bool> IsNotReverseConversionExistsAsync(Guid productId, Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductUnitConversion.ExistsReverseConversionAsync(productId, fromUnitId, toUnitId, cancellationToken).ContinueWith(t => !t.Result, cancellationToken);
        public async Task<bool> ValidateUnitConversionAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default)
        {
            if (conversion == null)
                return false;
            if (!await IsNotSelfConversionAsync(conversion.FromUnitId, conversion.ToUnitId, cancellationToken))
                return false;
            if (!await IsNotReverseConversionExistsAsync(conversion.ProductId, conversion.FromUnitId, conversion.ToUnitId, cancellationToken))
                return false;
            if (!await IsValidProductAsync(conversion.ProductId, cancellationToken))
                return false;
            if (!await IsValidUnitAsync(conversion.FromUnitId, cancellationToken)
             || !await IsValidUnitAsync(conversion.ToUnitId, cancellationToken))
                return false;
            if (!await IsValidConversionRateAsync(conversion.ConversionRate, cancellationToken)
             || !await IsValidFactorAsync(conversion.Factor, cancellationToken))
                return false;
            if (await UnitConversionExistsForProductAsync(conversion.ProductId, conversion.FromUnitId, conversion.ToUnitId, conversion.ID, cancellationToken))
                return false;
            return true;
        }
    }
}
