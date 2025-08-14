using ErpSwiftCore.Domain.Entities.EntityProduct; 
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService
{
    public interface IProductTaxValidationService
    {
        #region Existence & Referential Integrity
        Task<bool> TaxExistsByIdAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> TaxExistsForProductAsync(Guid productId, decimal rate, Guid? excludeTaxId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);
        #endregion
        #region Business Rules Validation
        Task<bool> IsRateValidAsync(decimal rate, CancellationToken cancellationToken = default);
        #endregion
        #region Aggregate Validation
        Task<bool> ValidateTaxAsync(ProductTax tax, CancellationToken cancellationToken = default);
        Task<bool> IsValidRateAsync(decimal rate, CancellationToken cancellationToken);
        #endregion
    }
}
