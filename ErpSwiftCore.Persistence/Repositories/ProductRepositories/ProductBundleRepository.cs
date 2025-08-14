using System.Linq.Expressions;
using ErpSwiftCore.Persistence.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories.IProductRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.TenantManagement.Interfaces;
namespace ErpSwiftCore.Persistence.Repositories.ProductRepositories
{
    public class ProductBundleRepository : MultiTenantRepository<ProductBundle>, IProductBundleRepository
    {
        private static readonly Expression<Func<ProductBundle, object>>[] DefaultIncludes =
        {
            x => x.ParentProduct,
            x => x.ComponentProduct,
            x => x.UnitOfMeasurement
        };

        public ProductBundleRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<ProductBundle> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        #region Create
        public async Task<Guid> CreateAsync(ProductBundle bundle, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(bundle, autoSave: true, cancellationToken);
                return bundle.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var b in bundles ?? Array.Empty<ProductBundle>())
            {
                var id = await CreateAsync(b, cancellationToken);
                if (id != Guid.Empty) ids.Add(id);
            }
            return ids;
        }
        public async Task<int> BulkImportAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = bundles?.ToList() ?? new List<ProductBundle>();
                await base.AddRangeAsync(list, autoSave: true, cancellationToken);
                return list.Count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region Read
        public Task<ProductBundle?> GetByIdAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => SafeGet(() => base.GetAsync(b => b.ID == bundleId, cancellationToken, DefaultIncludes));
        public Task<ProductBundle?> GetWithParentProductAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => SafeGet(() => base.GetAsync(b => b.ID == bundleId, cancellationToken, DefaultIncludes));
        public Task<ProductBundle?> GetWithComponentProductAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => SafeGet(() => base.GetAsync(b => b.ID == bundleId, cancellationToken, DefaultIncludes));
        public Task<ProductBundle?> GetWithUnitAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => SafeGet(() => base.GetAsync(b => b.ID == bundleId, cancellationToken, DefaultIncludes));
        public async Task<IReadOnlyList<ProductBundle>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await SafeGet(() => base.GetAllAsync(null, cancellationToken, DefaultIncludes));
            return result ?? Array.Empty<ProductBundle>();
        } 



 
        public Task<ProductBundle?> GetSoftDeletedByIdAsync(Guid bundleId, CancellationToken cancellationToken = default)
            => SafeGet(() => base.GetOneSoftDeletedAsync(b => b.ID == bundleId, cancellationToken, DefaultIncludes));
        public async Task<IReadOnlyList<ProductBundle>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            var result = await SafeGet(() => base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes));
            return result ?? Array.Empty<ProductBundle>();
        } 

        public async Task<IReadOnlyList<ProductBundle>> GetByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetAllAsync(b => b.ParentProductId == parentProductId, cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }
        public async Task<IReadOnlyList<ProductBundle>> GetByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetAllAsync(b => b.ComponentProductId == componentProductId, cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }
        public async Task<IReadOnlyList<ProductBundle>> GetByUnitAsync(Guid unitOfMeasurementId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetAllAsync(
                    b => EF.Property<Guid?>(b, "UnitOfMeasurementId") == unitOfMeasurementId,
                    cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }
        public async Task<IReadOnlyList<ProductBundle>> GetByIdsAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = bundleIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return Array.Empty<ProductBundle>();
                return await base.GetAllAsync(b => ids.Contains(b.ID), cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }
        public async Task<IReadOnlyList<ProductBundle>> GetByFilterAsync(Expression<Func<ProductBundle, bool>> filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (filter == null) return Array.Empty<ProductBundle>();
                return await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }
        #endregion





        #region Update

        public async Task<bool> UpdateAsync(ProductBundle bundle, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(bundle, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateRangeAsync(IEnumerable<ProductBundle> bundles, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var bundle in bundles ?? Array.Empty<ProductBundle>())
            {
                ok &= await UpdateAsync(bundle, cancellationToken);
            }
            return ok;
        }
      
        #endregion



        #region Soft Delete Operations

        public async Task<bool> SoftDeleteAsync(Guid bundleId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(b => b.ID == bundleId, cancellationToken: cancellationToken);
                if (entity == null)
                    return false;

                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = bundleIds?.ToList() ?? new List<Guid>();
                if (!ids.Any())
                    return false;

                var entities = await base.GetAllAsync(b => ids.Contains(b.ID), cancellationToken: cancellationToken);
                if (entities == null || !entities.Any())
                    return false;

                await base.SoftDeleteRangeAsync(entities, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var allBundles = await GetAllAsync(cancellationToken);
                var allIds = allBundles.Select(b => b.ID);
                return await SoftDeleteRangeAsync(allIds, cancellationToken);
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region Delete

        public async Task<bool> DeleteAsync(Guid bundleId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(b => b.ID == bundleId, cancellationToken);
                if (entity == null) return false;
                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in bundleIds ?? Array.Empty<Guid>())
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var all = await GetAllAsync(cancellationToken);
                return await DeleteRangeAsync(all.Select(b => b.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Restore

        public async Task<bool> RestoreAsync(Guid bundleId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetOneSoftDeletedAsync(b => b.ID == bundleId, cancellationToken);
                if (entity == null) return false;
                await base.RestoreAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in bundleIds ?? Array.Empty<Guid>())
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var all = await GetAllSoftDeletedAsync(cancellationToken);
                return await RestoreRangeAsync(all.Select(b => b.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Paging & Filtering

        public async Task<(IReadOnlyList<ProductBundle> Bundles, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var total = await base.CountAsync(null, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, null, b => b.ID, true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<ProductBundle>(), 0);
            }
        }
         

        public async Task<(IReadOnlyList<ProductBundle> Bundles, int TotalCount)> GetPagedByParentProductAsync(Guid parentProductId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                Expression<Func<ProductBundle, bool>> filter = b => b.ParentProductId == parentProductId;
                var total = await base.CountAsync(filter, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, filter, b => b.ID, true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<ProductBundle>(), 0);
            }
        }

        #endregion

        #region Search

        public async Task<IReadOnlyList<ProductBundle>> SearchByParentProductNameAsync(string parentProductName, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(parentProductName))
                    return Array.Empty<ProductBundle>();

                var lowered = parentProductName.ToLowerInvariant();
                return await base.GetAllAsync(
                    b => b.ParentProduct != null && b.ParentProduct.Name.ToLower().Contains(lowered),
                    cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }

        public async Task<IReadOnlyList<ProductBundle>> SearchByComponentProductNameAsync(string componentProductName, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(componentProductName))
                    return Array.Empty<ProductBundle>();

                var lowered = componentProductName.ToLowerInvariant();
                return await base.GetAllAsync(
                    b => b.ComponentProduct != null && b.ComponentProduct.Name.ToLower().Contains(lowered),
                    cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }

        public async Task<IReadOnlyList<ProductBundle>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return Array.Empty<ProductBundle>();

                var lowered = keyword.ToLowerInvariant();
                return await base.GetAllAsync(
                    b => (b.ParentProduct != null && b.ParentProduct.Name.ToLower().Contains(lowered)) ||
                         (b.ComponentProduct != null && b.ComponentProduct.Name.ToLower().Contains(lowered)),
                    cancellationToken, DefaultIncludes);
            }
            catch
            {
                return Array.Empty<ProductBundle>();
            }
        }

        #endregion

        #region Existence & Validation
        public async Task<bool> ExistsByIdAsync(Guid bundleId, CancellationToken cancellationToken = default)
        {
            try { return await base.ExistsAsync(b => b.ID == bundleId, cancellationToken); }
            catch { return false; }
        }
        public async Task<bool> ExistsBundleComponentAsync(Guid parentProductId, Guid componentProductId, Guid? excludeBundleId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.ExistsAsync(
                    b => b.ParentProductId == parentProductId
                         && b.ComponentProductId == componentProductId
                         && (!excludeBundleId.HasValue || b.ID != excludeBundleId.Value),
                    cancellationToken);
            }
            catch { return false; }
        }
        public async Task<bool> ExistsCircularDependencyAsync(Guid parentProductId, Guid componentProductId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.ExistsAsync(
                    b => b.ParentProductId == componentProductId && b.ComponentProductId == parentProductId,
                    cancellationToken);
            }
            catch { return false; }
        }
        public async Task<bool> IsValidParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default)
        {
            try { return await base.ExistsAsync(b => b.ParentProductId == parentProductId, cancellationToken); }
            catch { return false; }
        }
        public async Task<bool> IsValidComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default)
        {
            try { return await base.ExistsAsync(b => b.ComponentProductId == componentProductId, cancellationToken); }
            catch { return false; }
        }
        public async Task<bool> IsValidUnitAsync(Guid unitOfMeasurementId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (unitOfMeasurementId == Guid.Empty) return false;
                return await base.ExistsAsync(b => b.UnitOfMeasurementId.HasValue && b.UnitOfMeasurementId.Value == unitOfMeasurementId, cancellationToken);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Counts & Stats
        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            try { return await base.CountAsync(null, cancellationToken); }
            catch { return 0; }
        }
        public async Task<int> GetCountByParentProductAsync(Guid parentProductId, CancellationToken cancellationToken = default)
        {
            try { return await base.CountAsync(b => b.ParentProductId == parentProductId, cancellationToken); }
            catch { return 0; }
        }
        public async Task<int> GetCountByComponentProductAsync(Guid componentProductId, CancellationToken cancellationToken = default)
        {
            try { return await base.CountAsync(b => b.ComponentProductId == componentProductId, cancellationToken); }
            catch { return 0; }
        }

        #endregion

        #region Bulk Delete
        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> bundleIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var bundles = await GetByIdsAsync(bundleIds, cancellationToken);
                await base.SoftDeleteRangeAsync(bundles, autoSave: true, cancellationToken);
                return bundles.Count;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Helpers
        private async Task<T?> SafeGet<T>(Func<Task<T?>> getter) where T : class
        {
            try { return await getter(); }
            catch { return null; }
        }
        #endregion


    }
}
