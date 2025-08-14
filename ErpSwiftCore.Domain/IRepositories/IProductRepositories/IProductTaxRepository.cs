using ErpSwiftCore.Domain.Entities.EntityProduct;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IProductRepositories
{
    public interface IProductTaxRepository : IMultiTenantRepository<ProductTax>
    {
        Task<Guid> CreateAsync(ProductTax tax, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(ProductTax tax, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default); 
        Task<ProductTax?> GetByIdAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductTax>> GetAllAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<ProductTax>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductTax>> GetByIdsAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<ProductTax> Taxes, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<ProductTax> Taxes, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductTax>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductTax>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> ExistsForProductAsync(Guid productId, decimal rate, Guid? excludeTaxId = null, CancellationToken cancellationToken = default);
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> IsValidRateAsync(decimal rate, CancellationToken cancellationToken = default);
        Task<ProductTax?> GetWithProductAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> BulkImportAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductTax>> GetByFilterAsync(Expression<Func<ProductTax, bool>> filter, CancellationToken cancellationToken = default);
        Task<ProductTax?> GetSoftDeletedByIdAsync(Guid taxId, CancellationToken cancellationToken);
        Task<IReadOnlyList<ProductTax>> GetAllSoftDeletedAsync(CancellationToken cancellationToken);
        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken);
        Task<bool> SoftDeleteAsync(Guid taxId, CancellationToken cancellationToken);
    }
}