using System.Linq.Expressions;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.TenantManagement.Interfaces;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IRepositories.IBillingRepositories;
using ErpSwiftCore.Infrastructure.Validation;
using Microsoft.Extensions.Caching.Distributed;
using ErpSwiftCore.Infrastructure.Caching;
namespace ErpSwiftCore.Persistence.Repositories.BillingRepositories
{
    /// <summary>
    /// Repository for Invoice entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class InvoiceRepository :
        MultiTenantRepository<Invoice>,
        IInvoiceRepository
    {
        private static readonly Expression<Func<Invoice, object>>[] DefaultIncludes =
        {
             x => x.Order,
             x => x.Currency,
             x => x.Lines,
             x => x.Taxes,
             x => x.Discounts,
             x => x.Approvals, 
             x => x.Payments,
         };

        public InvoiceRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<Invoice> includeValidator) :
            base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(Invoice invoice, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(invoice, true, cancellationToken);
            return invoice.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Invoice> invoices, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var inv in invoices)
            {
                ids.Add(await CreateAsync(inv, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(Invoice invoice, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(invoice, true, cancellationToken);
            return true;
        }

        // ----------- [Delete / Hard Delete] -----------

        public async Task<bool> DeleteAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(i => i.ID == invoiceId, cancellationToken);
            if (entity is not null)
            {
                await base.RemoveAsync(entity, true);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> invoiceIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in invoiceIds)
            {
                ok &= await DeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            var ok = true;
            foreach (var inv in all)
            {
                ok &= await DeleteAsync(inv.ID, cancellationToken);
            }
            return ok;
        }

        // ----------- [Soft Delete / Archive / Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(i => i.ID == invoiceId, cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> invoiceIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in invoiceIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> SoftDeleteByOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var list = await GetByOrderAsync(orderId, cancellationToken);
            var ok = true;
            foreach (var inv in list)
            {
                ok &= await SoftDeleteAsync(inv.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(
                i => i.ID == invoiceId,
                cancellationToken: cancellationToken);

            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> invoiceIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in invoiceIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreByOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var restoredAny = false;
            var softDeleted = await base.GetAllSoftDeletedAsync(
                filter: i => i.OrderId == orderId,
                cancellationToken: cancellationToken);

            foreach (var inv in softDeleted)
            {
                await base.RestoreAsync(inv, true, cancellationToken);
                restoredAny = true;
            }

            return restoredAny;
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(i => i.ID == invoiceId, cancellationToken);

        public async Task<bool> AnyForOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(i => i.OrderId == orderId, cancellationToken);
        }

        // ----------- [Get/Query - Single & Details] -----------

        public Task<Invoice?> GetByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => base.GetAsync(
                i => i.ID == invoiceId, cancellationToken, DefaultIncludes);

        public Task<Invoice?> GetWithDetailsAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == invoiceId, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Invoice>> GetByOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(
                i => i.OrderId == orderId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Invoice>> GetByStatusAsync(InvoiceStatus status, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(
                i => i.InvoiceStatus == status, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Invoice>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            Expression<Func<Invoice, bool>> filter = i => i.InvoiceDate >= fromDate && i.InvoiceDate <= toDate;
            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Invoice>> GetOverdueAsync(DateTime asOfDate, CancellationToken cancellationToken = default)
        {
            Expression<Func<Invoice, bool>> filter = i =>
                i.DueDate.HasValue &&
                i.DueDate.Value < asOfDate &&
                i.PaidAmount < i.TotalAmount;
            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        // ----------- [Counts & Stats] -----------

        public async Task<int> CountAsync(
            Guid? orderId = null,
            InvoiceStatus? status = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<Invoice, bool>>? filter = null;

            if (orderId.HasValue || status.HasValue || fromDate.HasValue || toDate.HasValue)
            {
                filter = i =>
                    (!orderId.HasValue || i.OrderId == orderId.Value) &&
                    (!status.HasValue || i.InvoiceStatus == status.Value) &&
                    (!fromDate.HasValue || i.InvoiceDate >= fromDate.Value) &&
                    (!toDate.HasValue || i.InvoiceDate <= toDate.Value);
            }

            return await base.CountAsync(filter, cancellationToken);
        }

        public async Task<int> CountOverdueAsync(DateTime asOfDate, CancellationToken cancellationToken = default)
        {
            Expression<Func<Invoice, bool>> filter = i =>
                i.DueDate.HasValue &&
                i.DueDate.Value < asOfDate &&
                i.PaidAmount < i.TotalAmount;

            return await base.CountAsync(filter, cancellationToken);
        }

          }
}