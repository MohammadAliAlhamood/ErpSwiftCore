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
using ErpSwiftCore.Domain.Enums;  // For ProductPriceType

namespace ErpSwiftCore.Persistence.Repositories.ProductRepositories
{
    public class ProductPriceRepository : MultiTenantRepository<ProductPrice>, IProductPriceRepository
    {
        private static readonly Expression<Func<ProductPrice, object>>[] DefaultIncludes =
            { x => x.Product, x => x.Currency };

        public ProductPriceRepository(
            AppDbContext db,
            ITenantProvider tenantProvider,
            IUserProvider userProvider,
            IIncludeValidator<ProductPrice> includeValidator)
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        #region Create

        public async Task<Guid> CreateAsync(ProductPrice price, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(price, autoSave: true, cancellationToken);
                return price.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var p in prices ?? Enumerable.Empty<ProductPrice>())
            {
                var id = await CreateAsync(p, cancellationToken);
                if (id != Guid.Empty) ids.Add(id);
            }
            return ids;
        }

        public async Task<int> BulkImportAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = prices?.ToList() ?? new List<ProductPrice>();
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

        public Task<ProductPrice?> GetByIdAsync(Guid priceId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == priceId, cancellationToken, DefaultIncludes);

        public async Task<ProductPrice?> GetLatestByProductAsync(Guid productId, ProductPriceType priceType, CancellationToken cancellationToken = default)
        {
            var prices = await base.GetAllAsync(
                p => p.ProductId == productId && p.PriceType == priceType,
                cancellationToken,
                DefaultIncludes);
            return prices.OrderByDescending(p => p.EffectiveDate).FirstOrDefault();
        }

        public async Task<IReadOnlyList<ProductPrice>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            return result ?? Array.Empty<ProductPrice>();
        }

        public async Task<IReadOnlyList<ProductPrice>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
            return result ?? Array.Empty<ProductPrice>();
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(ProductPrice price, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(price, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Soft Delete

        public async Task<bool> SoftDeleteAsync(Guid priceId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(p => p.ID == priceId, cancellationToken: cancellationToken);
            if (entity == null) return false;
            await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
            return true;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
        {
            var ids = priceIds?.ToList() ?? new List<Guid>();
            if (!ids.Any()) return false;
            var entities = await base.GetAllAsync(p => ids.Contains(p.ID), cancellationToken, DefaultIncludes);
            if (!entities.Any()) return false;
            await base.SoftDeleteRangeAsync(entities, autoSave: true, cancellationToken);
            return true;
        }

        public async Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default)
            => await SoftDeleteRangeAsync((await GetAllAsync(cancellationToken)).Select(p => p.ID), cancellationToken);

        #endregion

        #region Delete (Hard)

        public Task<bool> DeleteAsync(Guid priceId, CancellationToken cancellationToken = default)
            => SoftDeleteAsync(priceId, cancellationToken);

        public Task<bool> DeleteRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
            => SoftDeleteRangeAsync(priceIds, cancellationToken);

        public Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
            => SoftDeleteAllAsync(cancellationToken);

        #endregion

        #region Restore

        public async Task<bool> RestoreAsync(Guid priceId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(p => p.ID == priceId, cancellationToken, DefaultIncludes);
            if (entity == null) return false;
            await base.RestoreAsync(entity, autoSave: true, cancellationToken);
            return true;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in priceIds ?? Enumerable.Empty<Guid>())
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }
 
        #endregion

        #region Existence & Validation

        public Task<bool> ExistsByIdAsync(Guid priceId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.ID == priceId, cancellationToken);

        public Task<bool> ExistsForProductAsync(Guid productId, ProductPriceType priceType, Guid? excludePriceId = null, CancellationToken cancellationToken = default)
            => base.ExistsAsync(
                   p => p.ProductId == productId
                        && p.PriceType == priceType
                        && (!excludePriceId.HasValue || p.ID != excludePriceId.Value),
                   cancellationToken);

        public Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.ProductId == productId, cancellationToken);

        public Task<bool> IsValidCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.CurrencyId == currencyId, cancellationToken);

        public Task<bool> IsValidPriceTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default)
            => Task.FromResult(Enum.IsDefined(typeof(ProductPriceType), priceType));

        #endregion

        #region Helpers

        public Task<ProductPrice?> GetWithProductAsync(Guid priceId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == priceId, cancellationToken, DefaultIncludes);

        public Task<ProductPrice?> GetWithCurrencyAsync(Guid priceId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == priceId, cancellationToken, DefaultIncludes);

        public Task<ProductPrice?> GetSoftDeletedByIdAsync(Guid priceId, CancellationToken cancellationToken = default)
            => base.GetOneSoftDeletedAsync(p => p.ID == priceId, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<ProductPrice>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.GetAllAsync(p => p.ProductId == productId, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<ProductPrice>> GetByTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default)
            => base.GetAllAsync(p => p.PriceType == priceType, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<ProductPrice>> GetByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default)
            => base.GetAllAsync(p => p.CurrencyId == currencyId, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<ProductPrice>> GetByIdsAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
        {
            var ids = priceIds?.ToList() ?? new List<Guid>();
            if (!ids.Any()) return Array.Empty<ProductPrice>();
            return await base.GetAllAsync(p => ids.Contains(p.ID), cancellationToken, DefaultIncludes);
        }

        public async Task<IReadOnlyList<ProductPrice>> GetByFilterAsync(Expression<Func<ProductPrice, bool>> filter, CancellationToken cancellationToken = default)
        {
            if (filter == null) return Array.Empty<ProductPrice>();
            return await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        }

        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);

        public Task<int> GetCountByProductAsync(Guid productId, CancellationToken cancellationToken = default)
            => base.CountAsync(p => p.ProductId == productId, cancellationToken);

        public Task<int> GetCountByTypeAsync(ProductPriceType priceType, CancellationToken cancellationToken = default)
            => base.CountAsync(p => p.PriceType == priceType, cancellationToken);

        public Task<IReadOnlyList<ProductPrice>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
            => base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);

        // إضافة في أسفل الصنف ProductPriceRepository
        #region Unimplemented Interface Methods

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            // جلب جميع المحذوفين ثم استعادتهم
            var softDeleted = await GetAllSoftDeletedAsync(cancellationToken);
            var ids = softDeleted.Select(p => p.ID);
            return await RestoreRangeAsync(ids, cancellationToken);
        }

        public async Task<ProductPrice?> GetLatestByProductAsync(Guid productId, string priceType, CancellationToken cancellationToken = default)
        {
            // محاولة تحويل النص إلى Enum ثم استدعاء الدالة المحمّلة
            if (!Enum.TryParse<ProductPriceType>(priceType, out var pt))
                return null;
            return await GetLatestByProductAsync(productId, pt, cancellationToken);
        }

        public async Task<IReadOnlyList<ProductPrice>> GetByTypeAsync(string priceType, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<ProductPriceType>(priceType, out var pt))
                return Array.Empty<ProductPrice>();
            return await GetByTypeAsync(pt, cancellationToken);
        }

        public async Task<bool> ExistsForProductAsync(Guid productId, string priceType, Guid? excludePriceId = null, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<ProductPriceType>(priceType, out var pt))
                return false;
            return await ExistsForProductAsync(productId, pt, excludePriceId, cancellationToken);
        }

        public Task<bool> IsValidPriceTypeAsync(string priceType, CancellationToken cancellationToken = default)
        {
            // التحقق فقط من وجود الاسم في الـ enum
            var valid = Enum.TryParse<ProductPriceType>(priceType, out var pt)
                        && Enum.IsDefined(typeof(ProductPriceType), pt);
            return Task.FromResult(valid);
        }

        public async Task<int> GetCountByTypeAsync(string priceType, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<ProductPriceType>(priceType, out var pt))
                return 0;
            return await GetCountByTypeAsync(pt, cancellationToken);
        }

        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default)
        {
            // حذف (ست-دليت) مجموعة المعرفات وإرجاع العدد المحذوف
            var ids = priceIds?.ToList() ?? new List<Guid>();
            if (!ids.Any()) return 0;
            var entities = await GetByIdsAsync(ids, cancellationToken);
            await SoftDeleteRangeAsync(entities.Select(e => e.ID), cancellationToken);
            return entities.Count;
        }

        #endregion

        #endregion
    }
}
