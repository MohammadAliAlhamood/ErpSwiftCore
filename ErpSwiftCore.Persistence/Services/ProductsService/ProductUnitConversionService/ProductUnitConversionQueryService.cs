using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using System.Linq.Expressions;
namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductUnitConversionService
{
    public class ProductUnitConversionQueryService : IProductUnitConversionQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public ProductUnitConversionQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<ProductUnitConversion?> GetUnitConversionByIdAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetByIdAsync(conversionId, cancellationToken);
        public Task<IReadOnlyList<ProductUnitConversion>> GetAllUnitConversionsAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetAllAsync(cancellationToken);
        public Task<IReadOnlyList<ProductUnitConversion>> GetSoftDeletedUnitConversionsAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetSoftDeletedAsync(cancellationToken);
        public Task<IReadOnlyList<ProductUnitConversion>> GetAllSoftDeletedUnitConversionsAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetAllSoftDeletedAsync(cancellationToken);
        public Task<ProductUnitConversion?> GetSoftDeletedUnitConversionByIdAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetSoftDeletedByIdAsync(conversionId, cancellationToken);
        public Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByIdsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetByIdsAsync(conversionIds, cancellationToken);
        public Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetByProductAsync(productId, cancellationToken);
        public Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByFromUnitAsync(Guid fromUnitId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetByFilterAsync(c => c.FromUnitId == fromUnitId, cancellationToken);
        public Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByToUnitAsync(Guid toUnitId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetByFilterAsync(c => c.ToUnitId == toUnitId, cancellationToken);
        public Task<ProductUnitConversion?> GetUnitConversionWithProductAsync(Guid conversionId, CancellationToken cancellationToken = default)
      => _unitOfWork.ProductUnitConversion.GetWithProductAsync(conversionId, cancellationToken);
        public Task<ProductUnitConversion?> GetUnitConversionWithFromUnitAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetWithUnitsAsync(conversionId, cancellationToken); // Includes both units
        public Task<ProductUnitConversion?> GetUnitConversionWithToUnitAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetWithUnitsAsync(conversionId, cancellationToken); // Includes both units
        public Task<int> GetUnitConversionsCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetCountAsync(cancellationToken);
        public Task<int> GetUnitConversionsCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => _unitOfWork.ProductUnitConversion.GetCountByProductAsync(productId, cancellationToken);


        public async Task<int> GetSoftDeletedUnitConversionsCountAsync(CancellationToken cancellationToken = default)
        {
            return (await _unitOfWork.ProductUnitConversion.GetAllSoftDeletedAsync(cancellationToken)).Count;
        }


    }
}