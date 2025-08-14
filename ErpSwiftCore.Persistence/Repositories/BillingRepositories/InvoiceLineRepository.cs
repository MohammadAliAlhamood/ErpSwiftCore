using System.Linq.Expressions;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using Microsoft.Extensions.Caching.Distributed;
using ErpSwiftCore.TenantManagement.Interfaces;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IRepositories.IBillingRepositories;
namespace ErpSwiftCore.Persistence.Repositories.BillingRepositories
{
    /// <summary>
    /// Repository for InvoiceLine entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class InvoiceLineRepository :
        MultiTenantRepository<InvoiceLine>,
        IInvoiceLineRepository
    {
        private static readonly Expression<Func<InvoiceLine, object>>[] DefaultIncludes =
        {
             x => x.Invoice,
             x => x.Product
         };

        public InvoiceLineRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<InvoiceLine> includeValidator)
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(InvoiceLine line, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(line, true, cancellationToken);
            return line.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<InvoiceLine> lines, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var l in lines)
            {
                ids.Add(await CreateAsync(l, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(InvoiceLine line, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(line, true, cancellationToken);
            return true;
        }

        // ----------- [Delete / Hard Delete] -----------

        public async Task<bool> DeleteAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(l => l.ID == lineId, cancellationToken);

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

        public async Task<bool> DeleteByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var lines = await GetByInvoiceAsync(invoiceId, cancellationToken);
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
            var entity = await base.GetAsync(
                l => l.ID == lineId, cancellationToken);

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

        public async Task<bool> SoftDeleteByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var lines = await GetByInvoiceAsync(invoiceId, cancellationToken);
            var ok = true;
            foreach (var l in lines)
            {
                ok &= await SoftDeleteAsync(l.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid lineId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(
                l => l.ID == lineId,
                cancellationToken: cancellationToken);

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

        public async Task<bool> RestoreByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var restoredAny = false;
            var softDeleted = await base.GetAllSoftDeletedAsync(
                filter: l => l.InvoiceId == invoiceId,
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

        public async Task<bool> AnyForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(l => l.InvoiceId == invoiceId, cancellationToken);
        }

        public async Task<bool> AnyForProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(l => l.ProductId == productId, cancellationToken);
        }

        // ----------- [Get/Query - Single & Details] ------------

        public Task<InvoiceLine?> GetByIdAsync(Guid lineId, CancellationToken cancellationToken = default)
            => base.GetAsync(
                l => l.ID == lineId, cancellationToken, DefaultIncludes);

        public Task<InvoiceLine?> GetByIdWithDetailsAsync(Guid lineId, CancellationToken cancellationToken = default)
            => base.GetAsync(l => l.ID == lineId, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<InvoiceLine>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<InvoiceLine>> GetByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(l => l.InvoiceId == invoiceId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<InvoiceLine>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(
                l => l.ProductId == productId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<InvoiceLine>> GetByQuantityRangeAsync(int minQuantity, int maxQuantity, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceLine, bool>> filter = l => l.Quantity >= minQuantity && l.Quantity <= maxQuantity;

            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<InvoiceLine>> GetBySubTotalRangeAsync(decimal minSubTotal, decimal maxSubTotal, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceLine, bool>> filter = l =>
                (l.Quantity * l.UnitPrice - l.Discount) >= minSubTotal &&
                (l.Quantity * l.UnitPrice - l.Discount) <= maxSubTotal;

            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<int> CountAsync(Guid? invoiceId = null, Guid? productId = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceLine, bool>>? filter = null;

            if (invoiceId.HasValue || productId.HasValue)
            {
                filter = l =>
                    (!invoiceId.HasValue || l.InvoiceId == invoiceId.Value) &&
                    (!productId.HasValue || l.ProductId == productId.Value);
            }
            return await base.CountAsync(filter, cancellationToken);
        }


    }
}