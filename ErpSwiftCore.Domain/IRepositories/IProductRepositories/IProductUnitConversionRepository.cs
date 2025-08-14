 

using ErpSwiftCore.Domain.Entities.EntityProduct;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;
 

namespace ErpSwiftCore.Domain.IRepositories.IProductRepositories
{
    public interface IProductUnitConversionRepository : IMultiTenantRepository<ProductUnitConversion>
    {
        Task<Guid> CreateAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default); 
        Task<ProductUnitConversion?> GetByIdAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetAllAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<ProductUnitConversion>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetByIdsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<ProductUnitConversion> Conversions, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<ProductUnitConversion> Conversions, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<bool> ExistsDuplicateAsync(Guid productId, Guid fromUnitId, Guid toUnitId, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsReverseConversionAsync(Guid productId, Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default);
        Task<bool> IsSelfConversionAsync(Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default);
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> IsValidUnitAsync(Guid unitId, CancellationToken cancellationToken = default);
        Task<ProductUnitConversion?> GetWithProductAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<ProductUnitConversion?> GetWithUnitsAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> BulkImportAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetByFilterAsync(Expression<Func<ProductUnitConversion, bool>> filter, CancellationToken cancellationToken = default);
        Task<ProductUnitConversion?> GetSoftDeletedByIdAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(Guid conversionId, CancellationToken cancellationToken);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken);
        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken);
    }
}