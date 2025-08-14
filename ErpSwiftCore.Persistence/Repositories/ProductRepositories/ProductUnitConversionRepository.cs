using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories.IProductRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace ErpSwiftCore.Persistence.Repositories.ProductRepositories
{
    public class ProductUnitConversionRepository
        : MultiTenantRepository<ProductUnitConversion>, IProductUnitConversionRepository
    {
        private static readonly Expression<Func<ProductUnitConversion, object>>[] DefaultIncludes =
        {
            x => x.Product,
            x => x.FromUnit,
            x => x.ToUnit
        };

        public ProductUnitConversionRepository(AppDbContext db, ITenantProvider tenantProvider, 
            IUserProvider userProvider, IIncludeValidator<ProductUnitConversion> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        #region Create

        public async Task<Guid> CreateAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(conversion, autoSave: true, cancellationToken);
                return conversion.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            try
            {
                foreach (var c in conversions ?? Enumerable.Empty<ProductUnitConversion>())
                {
                    var id = await CreateAsync(c, cancellationToken);
                    if (id != Guid.Empty) ids.Add(id);
                }
                return ids;
            }
            catch
            {
                return Array.Empty<Guid>();
            }
        }

        public async Task<int> BulkImportAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = conversions?.ToList() ?? new List<ProductUnitConversion>();
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

        public Task<ProductUnitConversion?> GetByIdAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == conversionId, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<ProductUnitConversion>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductUnitConversion>();
            }
            catch
            {
                return Array.Empty<ProductUnitConversion>();
            }
        }

   

        public async Task<IReadOnlyList<ProductUnitConversion>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllAsync(c => c.ProductId == productId, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductUnitConversion>();
            }
            catch
            {
                return Array.Empty<ProductUnitConversion>();
            }
        }

        public async Task<IReadOnlyList<ProductUnitConversion>> GetByIdsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = conversionIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return Array.Empty<ProductUnitConversion>();
                var result = await base.GetAllAsync(c => ids.Contains(c.ID), cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductUnitConversion>();
            }
            catch
            {
                return Array.Empty<ProductUnitConversion>();
            }
        }

        public async Task<IReadOnlyList<ProductUnitConversion>> GetByFilterAsync(Expression<Func<ProductUnitConversion, bool>> filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (filter == null) return Array.Empty<ProductUnitConversion>();
                var result = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductUnitConversion>();
            }
            catch
            {
                return Array.Empty<ProductUnitConversion>();
            }
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(conversion, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Soft Delete

        public async Task<bool> SoftDeleteAsync(Guid conversionId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(c => c.ID == conversionId, cancellationToken: cancellationToken);
                if (entity == null) return false;
                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = conversionIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return false;
                var entities = await base.GetAllAsync(c => ids.Contains(c.ID), cancellationToken, DefaultIncludes);
                if (!entities.Any()) return false;
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
                var all = await GetAllAsync(cancellationToken);
                return await SoftDeleteRangeAsync(all.Select(c => c.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Delete (Hard)

        public async Task<bool> DeleteAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => await SoftDeleteAsync(conversionId, cancellationToken);

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ok = true;
                foreach (var id in conversionIds ?? Enumerable.Empty<Guid>())
                    ok &= await SoftDeleteAsync(id, cancellationToken);
                return ok;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var all = await GetAllAsync(cancellationToken);
                return await DeleteRangeAsync(all.Select(c => c.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Restore

        public async Task<bool> RestoreAsync(Guid conversionId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetOneSoftDeletedAsync(c => c.ID == conversionId, cancellationToken, DefaultIncludes);
                if (entity == null) return false;
                await base.RestoreAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ok = true;
                foreach (var id in conversionIds ?? Enumerable.Empty<Guid>())
                    ok &= await RestoreAsync(id, cancellationToken);
                return ok;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var all = await GetAllSoftDeletedAsync(cancellationToken);
                return await RestoreRangeAsync(all.Select(c => c.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Activate / Deactivate

       

        #endregion

        #region Paging & Filtering

        public async Task<(IReadOnlyList<ProductUnitConversion> Conversions, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var total = await base.CountAsync(null, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, null, c => c.CreatedAt, ascending: true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<ProductUnitConversion>(), 0);
            }
        }

      

        public async Task<(IReadOnlyList<ProductUnitConversion> Conversions, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                Expression<Func<ProductUnitConversion, bool>> filter = c => c.ProductId == productId;
                var total = await base.CountAsync(filter, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, filter, c => c.CreatedAt, ascending: true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<ProductUnitConversion>(), 0);
            }
        }

        #endregion

        #region Search

        public async Task<IReadOnlyList<ProductUnitConversion>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productName))
                    return Array.Empty<ProductUnitConversion>();

                var lowered = productName.ToLowerInvariant();
                var result = await base.GetAllAsync(
                    c => c.Product != null && c.Product.Name != null && c.Product.Name.ToLower().Contains(lowered),
                    cancellationToken,
                    DefaultIncludes
                );
                return result ?? Array.Empty<ProductUnitConversion>();
            }
            catch
            {
                return Array.Empty<ProductUnitConversion>();
            }
        }

        public async Task<IReadOnlyList<ProductUnitConversion>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return Array.Empty<ProductUnitConversion>();

                var lowered = keyword.ToLowerInvariant();
                var result = await base.GetAllAsync(
                    c => (c.Product != null && c.Product.Name != null && c.Product.Name.ToLower().Contains(lowered)),
                    cancellationToken,
                    DefaultIncludes
                );
                return result ?? Array.Empty<ProductUnitConversion>();
            }
            catch
            {
                return Array.Empty<ProductUnitConversion>();
            }
        }

        #endregion

        #region Existence & Validation

        public Task<bool> ExistsByIdAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.ID == conversionId, cancellationToken);

        public Task<bool> ExistsDuplicateAsync(Guid productId, Guid fromUnitId, Guid toUnitId, Guid? excludeId = null, CancellationToken cancellationToken = default)
            => base.ExistsAsync(
                c => c.ProductId == productId
                     && c.FromUnitId == fromUnitId
                     && c.ToUnitId == toUnitId
                     && (!excludeId.HasValue || c.ID != excludeId.Value),
                cancellationToken
            );

        public Task<bool> ExistsReverseConversionAsync(Guid productId, Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(
                c => c.ProductId == productId
                     && c.FromUnitId == toUnitId
                     && c.ToUnitId == fromUnitId,
                cancellationToken
            );

        public Task<bool> IsSelfConversionAsync(Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default)
            => Task.FromResult(fromUnitId == toUnitId);

        public Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.ProductId == productId, cancellationToken);

        public Task<bool> IsValidUnitAsync(Guid unitId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(c => c.FromUnitId == unitId || c.ToUnitId == unitId, cancellationToken);

        #endregion

        #region Bulk Delete

        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var conversions = await GetByIdsAsync(conversionIds, cancellationToken);
                await base.SoftDeleteRangeAsync(conversions, autoSave: true, cancellationToken);
                return conversions.Count;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Helpers

        public Task<ProductUnitConversion?> GetWithProductAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == conversionId, cancellationToken, DefaultIncludes);

        public Task<ProductUnitConversion?> GetWithUnitsAsync(Guid conversionId, CancellationToken cancellationToken = default)
            => base.GetAsync(c => c.ID == conversionId, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<ProductUnitConversion>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductUnitConversion>();
            }
            catch
            {
                return Array.Empty<ProductUnitConversion>();
            }
        }

      

        public async Task<ProductUnitConversion?> GetSoftDeletedByIdAsync(Guid conversionId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetOneSoftDeletedAsync(c => c.ID == conversionId, cancellationToken, DefaultIncludes);
            }
            catch
            {
                return null;
            }
        }


        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.CountAsync(null, cancellationToken);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> GetCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.CountAsync(c => c.ProductId == productId, cancellationToken);
            }
            catch
            {
                return 0;
            }
        }

        public Task<IReadOnlyList<ProductUnitConversion>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
