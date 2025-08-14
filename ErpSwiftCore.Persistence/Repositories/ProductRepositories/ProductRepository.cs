 
using System.Linq.Expressions; 
using ErpSwiftCore.Domain.Enums;
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
    /// Repository implementation for managing products.
    /// Implements all advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Validation, etc.
    /// </summary>
    public class ProductRepository
        : MultiTenantRepository<Product>, IProductRepository
    {
        private static readonly Expression<Func<Product, object>>[] DefaultIncludes =
        {
            x => x.Category,
            x => x.UnitOfMeasurement 
        };

        public ProductRepository(AppDbContext db, ITenantProvider tenantProvider, 
            IUserProvider userProvider, IIncludeValidator<Product> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(product, autoSave: true, cancellationToken);
                return product.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            try
            {
                foreach (var p in products ?? Array.Empty<Product>())
                {
                    var id = await CreateAsync(p, cancellationToken);
                    if (id != Guid.Empty) ids.Add(id);
                }
                return ids;
            }
            catch
            {
                return Array.Empty<Guid>();
            }
        }

        public async Task<int> BulkImportAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = products?.ToList() ?? new List<Product>();
                await base.AddRangeAsync(list, autoSave: true, cancellationToken);
                return list.Count;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(product, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ----------- [Delete/Archive/Restore] -----------

        public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(p => p.ID == productId, cancellationToken: cancellationToken);
                if (entity == null) return false;
                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in productIds ?? Enumerable.Empty<Guid>())
                ok &= await DeleteAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var all = await GetAllAsync(cancellationToken);
                return await DeleteRangeAsync(all.Select(p => p.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetOneSoftDeletedAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);
                if (entity == null) return false;
                await base.RestoreAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in productIds ?? Enumerable.Empty<Guid>())
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var all = await GetAllSoftDeletedAsync(cancellationToken);
                return await RestoreRangeAsync(all.Select(p => p.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }
         
 
        // ----------- [Get/Query - Single] -----------

        public Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);

        public Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.Code == code, cancellationToken, DefaultIncludes);

        public Task<Product?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.Barcode == barcode, cancellationToken, DefaultIncludes);

        public async Task<Product?> GetSoftDeletedByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetOneSoftDeletedAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);
            }
            catch
            {
                return null;
            }
        }

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

   

        public async Task<IReadOnlyList<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllAsync(p => p.CategoryId == categoryId, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

        public async Task<IReadOnlyList<Product>> GetByTypeAsync(ProductType productType, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.GetAllAsync(p => p.ProductType == productType, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

        public async Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = productIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return Array.Empty<Product>();
                var result = await base.GetAllAsync(p => ids.Contains(p.ID), cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

        public async Task<IReadOnlyList<Product>> GetByFilterAsync(Expression<Func<Product, bool>> filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (filter == null) return Array.Empty<Product>();
                var result = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

        // ----------- [Paging/Filtering/Searching] -----------

        public async Task<(IReadOnlyList<Product> Products, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var total = await base.CountAsync(null, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, null, p => p.Name, ascending: true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<Product>(), 0);
            }
        }

        

        public async Task<(IReadOnlyList<Product> Products, int TotalCount)> GetPagedByCategoryAsync(Guid categoryId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                Expression<Func<Product, bool>> filter = p => p.CategoryId == categoryId;
                var total = await base.CountAsync(filter, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, filter, p => p.Name, ascending: true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<Product>(), 0);
            }
        }

        public async Task<(IReadOnlyList<Product> Products, int TotalCount)> GetPagedByTypeAsync(ProductType productType, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                Expression<Func<Product, bool>> filter = p => p.ProductType == productType;
                var total = await base.CountAsync(filter, cancellationToken);
                var items = await base.GetPagedAsync(pageIndex, pageSize, filter, p => p.Name, ascending: true, cancellationToken, DefaultIncludes);
                return (items, total);
            }
            catch
            {
                return (Array.Empty<Product>(), 0);
            }
        }

        // ----------- [Search] -----------

        public async Task<IReadOnlyList<Product>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name)) return Array.Empty<Product>();
                var lowered = name.ToLowerInvariant();
                var result = await base.GetAllAsync(p => p.Name != null && p.Name.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

        public async Task<IReadOnlyList<Product>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword)) return Array.Empty<Product>();
                var lowered = keyword.ToLowerInvariant();
                var result = await base.GetAllAsync(
                    p => (p.Name != null && p.Name.ToLower().Contains(lowered)) ||
                         (p.Code != null && p.Code.ToLower().Contains(lowered)) ||
                         (p.Barcode != null && p.Barcode.ToLower().Contains(lowered)),
                    cancellationToken,
                    DefaultIncludes);
                return result ?? Array.Empty<Product>();
            }
            catch
            {
                return Array.Empty<Product>();
            }
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsByIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.ID == productId, cancellationToken);

        public Task<bool> ExistsByCodeAsync(string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.Code == code && (!excludeProductId.HasValue || p.ID != excludeProductId.Value), cancellationToken);

        public Task<bool> ExistsByBarcodeAsync(string barcode, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.Barcode == barcode && (!excludeProductId.HasValue || p.ID != excludeProductId.Value), cancellationToken);

        public Task<bool> ExistsDuplicateAsync(string name, string code, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.Name == name && p.Code == code && (!excludeProductId.HasValue || p.ID != excludeProductId.Value), cancellationToken);

        public async Task<bool> IsNameUniqueAsync(string name, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
            => !await base.ExistsAsync(p => p.Name == name && (!excludeProductId.HasValue || p.ID != excludeProductId.Value), cancellationToken);

        public async Task<bool> IsValidCategoryAsync(Guid? categoryId, Guid? excludeProductId = null, CancellationToken cancellationToken = default)
        {
            if (!categoryId.HasValue) return true;
            return await base.ExistsAsync(
                p => p.CategoryId == categoryId.Value && (!excludeProductId.HasValue || p.ID != excludeProductId.Value),
                cancellationToken);
        }

        // ----------- [Relations: Category/Price/Unit/Tax/Bundle] -----------

        public Task<Product?> GetWithCategoryAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);

        public Task<Product?> GetWithPricesAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);

        public Task<Product?> GetWithTaxesAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);

        public Task<Product?> GetWithUnitConversionsAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);

        public Task<Product?> GetWithBundlesAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == productId, cancellationToken, DefaultIncludes);

        // ----------- [Counts & Stats] -----------

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

        public async Task<int> GetCountByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.CountAsync(p => p.CategoryId == categoryId, cancellationToken);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> GetCountByTypeAsync(ProductType productType, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.CountAsync(p => p.ProductType == productType, cancellationToken);
            }
            catch
            {
                return 0;
            }
        }

        // ----------- [Bulk Delete] -----------

        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var products = await GetByIdsAsync(productIds, cancellationToken);
                await base.SoftDeleteRangeAsync(products, autoSave: true, cancellationToken);
                return products.Count;
            }
            catch
            {
                return 0;
            }
        }

        // ----------- [Soft Delete Implementations] -----------

        public async Task<bool> SoftDeleteAsync(Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await base.GetAsync(p => p.ID == productId, cancellationToken: cancellationToken);
                if (entity == null) return false;
                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken)
        {
            try
            {
                var ids = productIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return false;
                var entities = await base.GetAllAsync(p => ids.Contains(p.ID), cancellationToken, DefaultIncludes);
                if (!entities.Any()) return false;
                await base.SoftDeleteRangeAsync(entities, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var all = await GetAllAsync(cancellationToken);
                return await SoftDeleteRangeAsync(all.Select(p => p.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }
    }
}
