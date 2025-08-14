using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IProductRepositories
{
    /// <summary>
    /// Repository interface for managing product bundles.
    /// Supports advanced retrieval, validation, analytics, paging, search, state, archive/restore, and bulk operations.
    /// </summary>
    public interface IProductBundleRepository : IMultiTenantRepository<ProductBundle>
    {
        #region Soft Delete Operations
        Task<bool> SoftDeleteAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default);

        #endregion
        #region CRUD Operations
        Task<Guid> CreateAsync(ProductBundle bundle, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default);
        Task<int> BulkImportAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(ProductBundle bundle, CancellationToken cancellationToken = default);
        #endregion

        #region Delete / Archive / Restore

        Task<bool> DeleteAsync(Guid bundleId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid bundleId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

        #endregion

       

        #region Single Retrieval
        Task<ProductBundle?> GetByIdAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<ProductBundle?> GetWithParentProductAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<ProductBundle?> GetWithComponentProductAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<ProductBundle?> GetWithUnitAsync(Guid bundleId, CancellationToken cancellationToken = default);
        #endregion

        #region Bulk / Advanced Retrieval
        Task<IReadOnlyList<ProductBundle>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProductBundle?> GetSoftDeletedByIdAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<ProductBundle>> GetByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetByUnitAsync(Guid unitOfMeasurementId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetByIdsAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> GetByFilterAsync(Expression<Func<ProductBundle, bool>> filter, CancellationToken cancellationToken = default);
        #endregion

        #region Paging, Filtering & Stats
        Task<(IReadOnlyList<ProductBundle> Bundles, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<ProductBundle> Bundles, int TotalCount)> GetPagedByParentProductAsync(Guid parentProductId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
           Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default);
        Task<int> GetCountByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default);

        #endregion

        #region Search Operations
        Task<IReadOnlyList<ProductBundle>> SearchByParentProductNameAsync(string parentProductName, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> SearchByComponentProductNameAsync(string componentProductName, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductBundle>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default);
        #endregion

        #region Bulk Delete
        Task<int> BulkDeleteAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default);
        #endregion
        #region Existence & Validation
        Task<bool> ExistsByIdAsync(Guid bundleId, CancellationToken cancellationToken = default);
        Task<bool> ExistsBundleComponentAsync(Guid parentProductId, Guid componentProductId, Guid? excludeBundleId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsCircularDependencyAsync(Guid parentProductId, Guid componentProductId, CancellationToken cancellationToken = default);
        Task<bool> IsValidParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default);
        Task<bool> IsValidComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default);
        Task<bool> IsValidUnitAsync(Guid unitOfMeasurementId, CancellationToken cancellationToken = default);
        #endregion
    }
}
