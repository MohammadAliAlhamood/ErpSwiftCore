using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService
{
    public interface IProductPriceValidationService
    {
        Task<bool> PriceExistsByIdAsync(Guid priceId, CancellationToken cancellationToken = default);
        Task<bool> PriceExistsForProductAsync(Guid productId, ProductPriceType priceType, Guid? excludePriceId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> IsValidCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);
        Task<bool> IsValidPriceTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default);
        Task<bool> IsPricePositiveAsync(decimal price, CancellationToken cancellationToken = default);
        Task<bool> IsEffectiveDateValidAsync(DateTime effectiveDate, CancellationToken cancellationToken = default);
        Task<bool> ValidatePriceAsync(ProductPrice price, CancellationToken cancellationToken = default);
    }
}
