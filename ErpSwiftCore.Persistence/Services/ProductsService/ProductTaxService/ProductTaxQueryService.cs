using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductTaxService
{
    public class ProductTaxQueryService : IProductTaxQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductTaxQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ProductTax?> GetTaxByIdAsync(Guid taxId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductTax.GetByIdAsync(taxId, cancellationToken);
        public Task<IReadOnlyList<ProductTax>> GetAllTaxesAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ProductTax.GetAllAsync(cancellationToken);
        public Task<IReadOnlyList<ProductTax>> GetTaxesByIdsAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductTax.GetByIdsAsync(taxIds, cancellationToken);
        public Task<IReadOnlyList<ProductTax>> GetTaxesByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductTax.GetByProductAsync(productId, cancellationToken);
        public Task<ProductTax?> GetTaxWithProductAsync(Guid taxId, CancellationToken cancellationToken = default)
     => _unitOfWork.ProductTax.GetWithProductAsync(taxId, cancellationToken);
        public Task<int> GetTaxesCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ProductTax.GetCountAsync(cancellationToken);
        public Task<int> GetTaxesCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductTax.GetCountByProductAsync(productId, cancellationToken);
        public async Task<ProductTax?> GetSoftDeletedTaxByIdAsync(Guid taxId, CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductTax.GetSoftDeletedByIdAsync(taxId, cancellationToken);
        public async Task<IReadOnlyList<ProductTax>> GetSoftDeletedTaxesAsync(CancellationToken cancellationToken = default)
            => await _unitOfWork.ProductTax.GetAllSoftDeletedAsync(cancellationToken);



        public async Task<int> GetSoftDeletedTaxesCountAsync(CancellationToken cancellationToken = default)
        {
            return (await _unitOfWork.ProductTax.GetAllSoftDeletedAsync(cancellationToken)).Count;
        }

    }
}