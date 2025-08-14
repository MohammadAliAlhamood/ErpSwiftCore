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
    /// Repository for InvoiceDiscount entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class InvoiceDiscountRepository :
        MultiTenantRepository<InvoiceDiscount>,
        IInvoiceDiscountRepository
    {
        private static readonly Expression<Func<InvoiceDiscount, object>>[] DefaultIncludes = { x => x.Invoice };

        public InvoiceDiscountRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<InvoiceDiscount> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        public async Task<Guid> CreateAsync(InvoiceDiscount discount, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(discount, true, cancellationToken);
            return discount.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<InvoiceDiscount> discounts, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var d in discounts)
            {
                ids.Add(await CreateAsync(d, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(InvoiceDiscount discount, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(discount, true, cancellationToken);
            return true;
        }

        // ----------- [Delete / Hard Delete] -----------

        public async Task<bool> DeleteAsync(Guid discountId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(d => d.ID == discountId, cancellationToken);
            if (entity is not null)
            {
                await base.RemoveAsync(entity, true);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> discountIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in discountIds)
            {
                ok &= await DeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await GetByInvoiceAsync(invoiceId, cancellationToken);
            var ok = true;
            foreach (var d in list)
            {
                ok &= await DeleteAsync(d.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            var ok = true;
            foreach (var d in all)
            {
                ok &= await DeleteAsync(d.ID, cancellationToken);
            }
            return ok;
        }

        // ----------- [Soft Delete / Archive / Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid discountId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(d => d.ID == discountId, cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> discountIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in discountIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> SoftDeleteByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await GetByInvoiceAsync(invoiceId, cancellationToken);
            var ok = true;
            foreach (var d in list)
            {
                ok &= await SoftDeleteAsync(d.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid discountId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(
                d => d.ID == discountId, 
                cancellationToken: cancellationToken);

            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> discountIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in discountIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var restoredAny = false;
            var softDeleted = await base.GetAllSoftDeletedAsync(
                filter: d => d.InvoiceId == invoiceId,
                cancellationToken: cancellationToken);

            foreach (var d in softDeleted)
            {
                await base.RestoreAsync(d, true, cancellationToken);
                restoredAny = true;
            }
            return restoredAny;
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid discountId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(d => d.ID == discountId, cancellationToken);

        public async Task<bool> AnyForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(d => d.InvoiceId == invoiceId, cancellationToken);
        }

        // ----------- [Get/Query - Single & Details] -----------

        public Task<InvoiceDiscount?> GetByIdAsync(Guid discountId, CancellationToken cancellationToken = default)
            => base.GetAsync(d => d.ID == discountId, cancellationToken, DefaultIncludes);

        public Task<InvoiceDiscount?> GetByNameAsync(Guid invoiceId, string discountName, CancellationToken cancellationToken = default)
            => base.GetAsync(
                d => d.InvoiceId == invoiceId && d.DiscountName == discountName, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<InvoiceDiscount>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<InvoiceDiscount>> GetByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(d => d.InvoiceId == invoiceId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<InvoiceDiscount>> GetByRateRangeAsync(decimal minRate, decimal maxRate, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceDiscount, bool>> filter = d => d.DiscountRate >= minRate && d.DiscountRate <= maxRate;
            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        // ----------- [Counts & Stats] -----------

        public async Task<int> CountAsync(Guid? invoiceId = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceDiscount, bool>>? filter = null;

            if (invoiceId.HasValue)
            {
                filter = d => d.InvoiceId == invoiceId.Value;
            }

            return await base.CountAsync(filter, cancellationToken);
        }


    }
}