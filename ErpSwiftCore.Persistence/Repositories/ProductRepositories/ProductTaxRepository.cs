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
    /// <summary>
    /// Repository implementation for managing product tax relationships.
    /// Implements: CRUD, Bulk, Paging, Search, State, Archive, Restore, Validation, Counts, Bulk Delete, etc.
    /// </summary>
    public class ProductTaxRepository
        : MultiTenantRepository<ProductTax>, IProductTaxRepository
    {
        private static readonly Expression<Func<ProductTax, object>>[] DefaultIncludes =
        {
            x => x.Product
        };

        public ProductTaxRepository(AppDbContext db, ITenantProvider tenantProvider, 
            IUserProvider userProvider, IIncludeValidator<ProductTax> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        #region Create & Bulk

        public async Task<Guid> CreateAsync(ProductTax tax, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(tax, autoSave: true, cancellationToken);
                return tax.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            try
            {
                foreach (var t in taxes ?? Array.Empty<ProductTax>())
                {
                    var id = await CreateAsync(t, cancellationToken);
                    if (id != Guid.Empty) ids.Add(id);
                }
                return ids;
            }
            catch
            {
                return Array.Empty<Guid>();
            }
        }

        public async Task<int> BulkImportAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = taxes?.ToList() ?? new List<ProductTax>();
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

        public Task<ProductTax?> GetByIdAsync(Guid taxId, CancellationToken cancellationToken = default)
            => base.GetAsync(t => t.ID == taxId, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<ProductTax>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductTax>();
            }
            catch
            {
                return Array.Empty<ProductTax>();
            }
        }

         

        public async Task<IReadOnlyList<ProductTax>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllAsync(t => t.ProductId == productId, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductTax>();
            }
            catch
            {
                return Array.Empty<ProductTax>();
            }
        }

        public async Task<IReadOnlyList<ProductTax>> GetByIdsAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = taxIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return Array.Empty<ProductTax>();
                var result = await base.GetAllAsync(t => ids.Contains(t.ID), cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductTax>();
            }
            catch
            {
                return Array.Empty<ProductTax>();
            }
        }

        public async Task<IReadOnlyList<ProductTax>> GetByFilterAsync(Expression<Func<ProductTax, bool>> filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (filter == null) return Array.Empty<ProductTax>();
                var result = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductTax>();
            }
            catch
            {
                return Array.Empty<ProductTax>();
            }
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(ProductTax tax, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(tax, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Soft Delete

        public async Task<bool> SoftDeleteAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(t => t.ID == taxId, cancellationToken: cancellationToken);
                if (entity == null) return false;
                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = taxIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return false;
                var entities = await base.GetAllAsync(t => ids.Contains(t.ID), cancellationToken, DefaultIncludes);
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
                return await SoftDeleteRangeAsync(all.Select(t => t.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Hard Delete

        public async Task<bool> DeleteAsync(Guid taxId, CancellationToken cancellationToken = default)
            => await SoftDeleteAsync(taxId, cancellationToken);

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ok = true;
                foreach (var id in taxIds ?? Enumerable.Empty<Guid>())
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
                return await DeleteRangeAsync(all.Select(t => t.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Restore

        public async Task<bool> RestoreAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetOneSoftDeletedAsync(t => t.ID == taxId, cancellationToken, DefaultIncludes);
                if (entity == null) return false;
                await base.RestoreAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ok = true;
                foreach (var id in taxIds ?? Enumerable.Empty<Guid>())
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
                return await RestoreRangeAsync(all.Select(t => t.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        

        #region Paging & Filtering

        public async Task<(IReadOnlyList<ProductTax> Taxes, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var total = await base.CountAsync(null, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, null, t => t.CreatedAt, ascending: true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<ProductTax>(), 0);
            }
        }

        public async Task<(IReadOnlyList<ProductTax> Taxes, int TotalCount)> GetPagedByProductAsync(Guid productId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                Expression<Func<ProductTax, bool>> filter = t => t.ProductId == productId;
                var total = await base.CountAsync(filter, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, filter, t => t.CreatedAt, ascending: true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<ProductTax>(), 0);
            }
        }

      

        #endregion

        #region Search

        public async Task<IReadOnlyList<ProductTax>> SearchByProductNameAsync(string productName, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productName)) return Array.Empty<ProductTax>();
                var lowered = productName.ToLowerInvariant();
                var result = await base.GetAllAsync(
                    t => t.Product != null && t.Product.Name != null && t.Product.Name.ToLower().Contains(lowered),
                    cancellationToken,
                    DefaultIncludes);
                return result ?? Array.Empty<ProductTax>();
            }
            catch
            {
                return Array.Empty<ProductTax>();
            }
        }

        public async Task<IReadOnlyList<ProductTax>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword)) return Array.Empty<ProductTax>();
                var lowered = keyword.ToLowerInvariant();
                var result = await base.GetAllAsync(
                    t => t.Product != null && t.Product.Name != null && t.Product.Name.ToLower().Contains(lowered),
                    cancellationToken,
                    DefaultIncludes);
                return result ?? Array.Empty<ProductTax>();
            }
            catch
            {
                return Array.Empty<ProductTax>();
            }
        }

        #endregion

        #region Existence & Validation

        public Task<bool> ExistsByIdAsync(Guid taxId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(t => t.ID == taxId, cancellationToken);

        public Task<bool> ExistsForProductAsync(Guid productId, decimal rate, Guid? excludeTaxId = null, CancellationToken cancellationToken = default)
            => base.ExistsAsync(
                t => t.ProductId == productId
                     && t.Rate == rate
                     && (!excludeTaxId.HasValue || t.ID != excludeTaxId.Value),
                cancellationToken);

        public Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(t => t.ProductId == productId, cancellationToken);

        public Task<bool> IsValidRateAsync(decimal rate, CancellationToken cancellationToken = default)
            => Task.FromResult(rate > 0);

        #endregion

        #region Relations & Helpers

        public Task<ProductTax?> GetWithProductAsync(Guid taxId, CancellationToken cancellationToken = default)
            => base.GetAsync(t => t.ID == taxId, cancellationToken, DefaultIncludes);

        #endregion

        #region Counts

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
                return await base.CountAsync(t => t.ProductId == productId, cancellationToken);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Bulk Delete

        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var taxes = await GetByIdsAsync(taxIds, cancellationToken);
                await base.SoftDeleteRangeAsync(taxes, autoSave: true, cancellationToken);
                return taxes.Count;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Archive / SoftDeleted Queries

        public async Task<IReadOnlyList<ProductTax>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<ProductTax>();
            }
            catch
            {
                return Array.Empty<ProductTax>();
            }
        }

        public async Task<ProductTax?> GetSoftDeletedByIdAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetOneSoftDeletedAsync(t => t.ID == taxId, cancellationToken, DefaultIncludes);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
