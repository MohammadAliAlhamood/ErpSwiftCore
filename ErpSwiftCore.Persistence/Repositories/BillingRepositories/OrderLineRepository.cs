using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IRepositories.IBillingRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Repositories.BillingRepositories
{
    /// <summary>
    /// Repository for OrderLine entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class OrderLineRepository : MultiTenantRepository<OrderLine>, IOrderLineRepository
    {
        private static readonly Expression<Func<OrderLine, object>>[] DefaultIncludes =
         {
             x => x.Order,
             x => x.Product
         };
        public OrderLineRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<OrderLine> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(OrderLine line, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(line, true, cancellationToken);
            return line.ID;
        }

        public async Task<IEnumerable<Guid>> CreateRangeAsync(IEnumerable<OrderLine> lines, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var l in lines)
            {
                ids.Add(await CreateAsync(l, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(OrderLine line, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(line, true, cancellationToken);
            return true;
        }

        // ----------- [Delete / Hard Delete] -----------

        public async Task<bool> DeleteAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(l => l.ID == lineId, cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.RemoveAsync(entity, true);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in lineIds)
            {
                ok &= await DeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteByOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var lines = await GetByOrderAsync(orderId, cancellationToken);
            var ok = true;
            foreach (var l in lines)
            {
                ok &= await DeleteAsync(l.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            var ok = true;
            foreach (var l in all)
            {
                ok &= await DeleteAsync(l.ID, cancellationToken);
            }
            return ok;
        }

        // ----------- [Soft Delete / Archive / Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(l => l.ID == lineId, cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in lineIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> SoftDeleteByOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var lines = await GetByOrderAsync(orderId, cancellationToken);
            var ok = true;
            foreach (var l in lines)
            {
                ok &= await SoftDeleteAsync(l.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(l => l.ID == lineId,  cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> lineIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in lineIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreByOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            // Retrieve soft-deleted lines for the specified order
            var restoredAny = false;
            var softDeleted = await base.GetAllSoftDeletedAsync(
                filter: l => l.OrderId == orderId,
                cancellationToken: cancellationToken);

            foreach (var l in softDeleted)
            {
                await base.RestoreAsync(l, true, cancellationToken);
                restoredAny = true;
            }

            return restoredAny;
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid lineId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(l => l.ID == lineId, cancellationToken);

        public async Task<bool> AnyForOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(l => l.OrderId == orderId, cancellationToken);
        }

        public async Task<bool> AnyForProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(l => l.ProductId == productId, cancellationToken);
        }

        // ----------- [Get/Query - Single & Details] -----------

        public Task<OrderLine?> GetByIdAsync(Guid lineId, CancellationToken cancellationToken = default)
            => base.GetAsync(l => l.ID == lineId, cancellationToken, DefaultIncludes);

        public Task<OrderLine?> GetByIdWithDetailsAsync(Guid lineId, CancellationToken cancellationToken = default)
            => base.GetAsync(
                l => l.ID == lineId, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<OrderLine>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<OrderLine>> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(l => l.OrderId == orderId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<OrderLine>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(l => l.ProductId == productId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<OrderLine>> GetByQuantityRangeAsync(int minQuantity, int maxQuantity, CancellationToken cancellationToken = default)
        {
            Expression<Func<OrderLine, bool>> filter = l =>
                l.Quantity >= minQuantity &&
                l.Quantity <= maxQuantity;

            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<OrderLine>> GetBySubTotalRangeAsync(decimal minSubTotal, decimal maxSubTotal, CancellationToken cancellationToken = default)
        {
            Expression<Func<OrderLine, bool>> filter = l =>
                (l.Quantity * l.UnitPrice - l.Discount) >= minSubTotal &&
                (l.Quantity * l.UnitPrice - l.Discount) <= maxSubTotal;

            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<int> CountAsync(Guid? orderId = null, Guid? productId = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<OrderLine, bool>>? filter = null;

            if (orderId.HasValue || productId.HasValue)
            {
                filter = l =>
                    (!orderId.HasValue || l.OrderId == orderId.Value) &&
                    (!productId.HasValue || l.ProductId == productId.Value);
            }

            return await base.CountAsync(filter, cancellationToken);
        }

      }
}