using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductPriceService
{
    public class ProductPriceCommandService : IProductPriceCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IProductPriceValidationService _validation;

        public ProductPriceCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            IProductPriceValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }

        // -------------------- [CRUD Operations] --------------------

        public async Task<Guid> CreatePriceAsync(ProductPrice price, CancellationToken cancellationToken = default)
        {
            // Reuse comprehensive validation
            //if (!await _validation.ValidatePriceAsync(price, cancellationToken))
            //    throw new InvalidOperationException("Price validation failed.");

            return await _unitOfWork.ProductPrice.CreateAsync(price, cancellationToken);
        }

        public async Task<bool> UpdatePriceAsync(ProductPrice price, CancellationToken cancellationToken = default)
        {
            if (!await _validation.ValidatePriceAsync(price, cancellationToken))
                throw new InvalidOperationException("Price validation failed.");

            return await _unitOfWork.ProductPrice.UpdateAsync(price, cancellationToken);
        }

        public async Task<IEnumerable<Guid>> AddPricesRangeAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default)
        {
            foreach (var price in prices)
            {
                if (!await _validation.ValidatePriceAsync(price, cancellationToken))
                    throw new InvalidOperationException($"Validation failed for price ID {price.ID}.");
            }
            return await _unitOfWork.ProductPrice.AddRangeAsync(prices, cancellationToken);
        }

        public async Task<bool> DeletePriceAsync(Guid priceId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.DeleteAsync(priceId, cancellationToken);

        public async Task<bool> DeletePricesRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.DeleteRangeAsync(priceIds, cancellationToken);

        public async Task<bool> DeleteAllPricesAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.DeleteAllAsync(cancellationToken);

        public async Task<bool> RestorePriceAsync(Guid priceId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.RestoreAsync(priceId, cancellationToken);

        public async Task<bool> RestorePricesRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.RestoreRangeAsync(priceIds, cancellationToken);

        public async Task<bool> RestoreAllPricesAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.RestoreAllAsync(cancellationToken);

        public async Task<int> BulkImportPricesAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.BulkImportAsync(prices, cancellationToken);

        public async Task<int> BulkDeletePricesAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.BulkDeleteAsync(priceIds, cancellationToken);
 

        // -------------------- [Soft Delete Operations] --------------------

        public async Task<bool> SoftDeletePriceAsync(Guid priceId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductPrice.SoftDeleteAsync(priceId, cancellationToken);
        }

        public async Task<bool> SoftDeletePricesRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductPrice.SoftDeleteRangeAsync(priceIds, cancellationToken);
        }

        public async Task<bool> SoftDeleteAllPricesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductPrice.SoftDeleteAllAsync(cancellationToken);
        }

        public async Task<int> BulkSoftDeletePricesAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
        {
            // Soft-delete many and return the count of attempted deletions
            var success = await _unitOfWork.ProductPrice.SoftDeleteRangeAsync(priceIds, cancellationToken);
            return success ? priceIds.Count() : 0;
        }

        public async Task<int> BulkRestorePricesAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
        {
            var success = await _unitOfWork.ProductPrice.RestoreRangeAsync(priceIds, cancellationToken);
            return success ? priceIds.Count() : 0;
        }
    }
}
