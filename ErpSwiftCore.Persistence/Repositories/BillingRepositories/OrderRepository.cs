using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
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
    /// Repository for Order entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class OrderRepository : MultiTenantRepository<Order>, IOrderRepository
    {
        private static readonly Expression<Func<Order, object>>[] DefaultIncludes =
         {
           //  x => x.OrderLines,
             x => x.Currency
         };
        public OrderRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<Order> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        { }
        // ----------- [CRUD & Bulk] -----------
        public async Task<bool> DeleteAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(o => o.ID == orderId, cancellationToken: cancellationToken);
            if (entity is null) return false;
            await base.RemoveAsync(entity, true, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken = default)
        {
            var entities = await base.GetAllAsync(o => orderIds.Contains(o.ID), cancellationToken);
            if (!entities.Any()) return false;
            await base.RemoveRangeAsync(entities, true, cancellationToken);
            return true;
        }

        public async Task<Guid> CreateAsync(Order order, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(order, true, cancellationToken);
            return order.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Order> orders, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var o in orders)
            {
                ids.Add(await CreateAsync(o, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(order, true, cancellationToken);
            return true;
        }

        // ----------- [Delete/Archive/Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(o => o.ID == orderId, cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in orderIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(o => o.ID == orderId, cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in orderIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(o => o.ID == orderId, cancellationToken);

        // ----------- [Get/Query - Single] -----------

        public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
            => base.GetAsync(
                o => o.ID == orderId, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(
                filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Order>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            Expression<Func<Order, bool>> filter = o =>
                o.OrderDate >= from &&
                o.OrderDate <= to;

            var list = await base.GetAllAsync(
                filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

       
        public async Task<IReadOnlyList<Order>> GetByStatusAsync(OrderStatus orderStatus, CancellationToken cancellationToken = default)
        {
            Expression<Func<Order, bool>> filter = o =>
                o.OrderStatus == orderStatus;

            var list = await base.GetAllAsync(
                filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Order>> GetByTypeAsync(OrderType orderType, CancellationToken cancellationToken = default)
        {
            Expression<Func<Order, bool>> filter = o =>
                o.OrderType == orderType;

            var list = await base.GetAllAsync(
                filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<int> CountAsync(
            Guid? partyId = null,
            OrderType? orderType = null,
            OrderStatus? status = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<Order, bool>>? filter = null;

            if (partyId.HasValue ||
                orderType.HasValue ||
                status.HasValue ||
                fromDate.HasValue ||
                toDate.HasValue)
            {
                filter = o =>
                    (!partyId.HasValue || o.PartyId == partyId.Value) && 
                    (!orderType.HasValue || o.OrderType == orderType.Value) &&
                    (!status.HasValue || o.OrderStatus == status.Value) &&
                    (!fromDate.HasValue || o.OrderDate >= fromDate.Value) &&
                    (!toDate.HasValue || o.OrderDate <= toDate.Value);
            }

            return await base.CountAsync(filter, cancellationToken);
        }

          }
}