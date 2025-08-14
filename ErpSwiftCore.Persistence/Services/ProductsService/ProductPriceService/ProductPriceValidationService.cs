using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductPriceService
{
    public class ProductPriceValidationService : IProductPriceValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public ProductPriceValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> PriceExistsByIdAsync(Guid priceId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductPrice.ExistsByIdAsync(priceId, cancellationToken);

        public async Task<bool> PriceExistsForProductAsync(Guid productId, ProductPriceType priceType, Guid? excludePriceId = null, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductPrice.ExistsForProductAsync(productId, priceType, excludePriceId, cancellationToken);

        public async Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductPrice.IsValidProductAsync(productId, cancellationToken);

        public async Task<bool> IsValidCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductPrice.IsValidCurrencyAsync(currencyId, cancellationToken);

        public Task<bool> IsValidPriceTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default)
            => Task.FromResult(Enum.IsDefined(typeof(ProductPriceType), priceType));

        public Task<bool> IsPricePositiveAsync(decimal price, CancellationToken cancellationToken = default)
            => Task.FromResult(price > 0m);

        public Task<bool> IsEffectiveDateValidAsync(DateTime effectiveDate, CancellationToken cancellationToken = default)
            => Task.FromResult(effectiveDate <= DateTime.UtcNow);

        public async Task<bool> ValidatePriceAsync(ProductPrice price, CancellationToken cancellationToken = default)
        {
            if (price == null)
                return false;

            if (await PriceExistsForProductAsync(price.ProductId, price.PriceType, price.ID, cancellationToken))
                return false;

            if (!await IsValidProductAsync(price.ProductId, cancellationToken))
                return false;

            if (!await IsValidCurrencyAsync(price.CurrencyId, cancellationToken))
                return false;

            if (!await IsValidPriceTypeAsync(price.PriceType, cancellationToken))
                return false;

            if (!await IsPricePositiveAsync(price.Price, cancellationToken))
                return false;

            if (!await IsEffectiveDateValidAsync(price.EffectiveDate, cancellationToken))
                return false;

            return true;
        }
         
    }
}
