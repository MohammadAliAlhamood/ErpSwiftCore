using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductPriceService
{
    public class ProductPriceQueryService : IProductPriceQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        public ProductPriceQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductPrice?> GetPriceByIdAsync(Guid priceId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetByIdAsync(priceId, cancellationToken);
        public async Task<ProductPrice?> GetSoftDeletedPriceByIdAsync(Guid priceId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetSoftDeletedByIdAsync(priceId, cancellationToken);
        public async Task<ProductPrice?> GetLatestPriceByProductAsync(Guid productId, string priceType, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetLatestByProductAsync(productId, priceType, cancellationToken);
        public async Task<IReadOnlyList<ProductPrice>> GetAllPricesAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetAllAsync(cancellationToken);
        public async Task<IReadOnlyList<ProductPrice>> GetSoftDeletedPricesAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetSoftDeletedAsync(cancellationToken);

        public async Task<IReadOnlyList<ProductPrice>> GetAllSoftDeletedPricesAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetAllSoftDeletedAsync(cancellationToken);

        public async Task<IReadOnlyList<ProductPrice>> GetPricesByProductAsync(Guid productId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetByProductAsync(productId, cancellationToken) ;

        public async Task<IReadOnlyList<ProductPrice>> GetPricesByTypeAsync(string priceType, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetByTypeAsync(priceType, cancellationToken);

        public async Task<IReadOnlyList<ProductPrice>> GetPricesByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetByCurrencyAsync(currencyId, cancellationToken);

        public async Task<IReadOnlyList<ProductPrice>> GetPricesByIdsAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetByIdsAsync(priceIds, cancellationToken);
 
        public async Task<ProductPrice?> GetPriceWithProductAsync(Guid priceId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetWithProductAsync(priceId, cancellationToken);
        public async Task<int> GetPricesCountAsync(CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetCountAsync(cancellationToken);
        public async Task<int> GetPricesCountByProductAsync(Guid productId, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetCountByProductAsync(productId, cancellationToken);
        public async Task<int> GetPricesCountByTypeAsync(string priceType, CancellationToken cancellationToken = default) =>
            await _unitOfWork.ProductPrice.GetCountByTypeAsync(priceType, cancellationToken);
        // ================================
        // Implements the previously missing members
        // ================================
        public async Task<(IReadOnlyList<ProductPrice> Prices, int TotalCount)> GetPricesPagedSoftDeletedAsync(
            int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await _unitOfWork.ProductPrice.GetAllSoftDeletedAsync(cancellationToken);
            var total = SoftDeleted.Count;
            var paged = SoftDeleted.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return (paged, total);
        }
        public async Task<int> GetSoftDeletedPricesCountAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await _unitOfWork.ProductPrice.GetAllSoftDeletedAsync(cancellationToken);
            return SoftDeleted.Count;
        }
        public async Task<int> GetSoftDeteltedPricesCountAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await _unitOfWork.ProductPrice.GetAllSoftDeletedAsync(cancellationToken);
            return SoftDeleted.Count;
        }
    }
}
