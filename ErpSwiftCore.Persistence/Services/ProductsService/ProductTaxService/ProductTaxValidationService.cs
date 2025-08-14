using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductTaxService
{
    public class ProductTaxValidationService : IProductTaxValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public ProductTaxValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<bool> TaxExistsByIdAsync(Guid taxId, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductTax.ExistsByIdAsync(taxId, cancellationToken);
        public Task<bool> TaxExistsForProductAsync(Guid productId, decimal rate, Guid? excludeTaxId = null, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductTax.ExistsForProductAsync(productId, rate, excludeTaxId, cancellationToken);
        public Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductTax.IsValidProductAsync(productId, cancellationToken);
        public Task<bool> IsValidRateAsync(decimal rate, CancellationToken cancellationToken = default) =>
            _unitOfWork.ProductTax.IsValidRateAsync(rate, cancellationToken);
        public Task<bool> IsRateValidAsync(decimal rate, CancellationToken cancellationToken = default)
        { 
            return Task.FromResult(rate >= 0m && rate <= 1m);
        } 
        public async Task<bool> ValidateTaxAsync(ProductTax tax, CancellationToken cancellationToken = default)
        {
            if (tax == null)
                return false;
            if (!await IsValidProductAsync(tax.ProductId, cancellationToken))
                return false;
            if (!await IsRateValidAsync(tax.Rate, cancellationToken))
                return false;
            if (await TaxExistsForProductAsync(tax.ProductId, tax.Rate, tax.ID, cancellationToken))
                return false;

            return true;
        }
    }
}
