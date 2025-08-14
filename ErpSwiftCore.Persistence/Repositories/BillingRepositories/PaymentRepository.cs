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
    /// Repository for Payment entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class PaymentRepository :
        MultiTenantRepository<Payment>,
        IPaymentRepository
    {
        private static readonly Expression<Func<Payment, object>>[] DefaultIncludes =
         {
             x => x.Invoice
         };

        public PaymentRepository(AppDbContext db, ITenantProvider tenantProvider, 
            IUserProvider userProvider, IIncludeValidator<Payment> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(payment, true, cancellationToken);
            return payment.ID;
        }

        public async Task<IEnumerable<Guid>> CreateRangeAsync(IEnumerable<Payment> payments, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var p in payments)
            {
                ids.Add(await CreateAsync(p, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(payment, true, cancellationToken);
            return true;
        }

        // ----------- [Delete/Archive/Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(p => p.ID == paymentId, cancellationToken, DefaultIncludes);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> paymentIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in paymentIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(p => p.ID == paymentId, cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> paymentIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in paymentIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid paymentId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.ID == paymentId, cancellationToken);

        // ----------- [Get/Query - Single] -----------

        public Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == paymentId, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Payment>> GetByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(p => p.InvoiceId == invoiceId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Payment>> GetByAmountRangeAsync(decimal minAmount, decimal maxAmount, CancellationToken cancellationToken = default)
        {
            Expression<Func<Payment, bool>> filter = p =>
                p.PaymentAmount >= minAmount &&
                p.PaymentAmount <= maxAmount;
            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Payment>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            Expression<Func<Payment, bool>> filter = p =>
                p.PaymentDate >= from &&
                p.PaymentDate <= to;

            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        public async Task<int> CountAsync(
            Guid? invoiceId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            decimal? minAmount = null,
            decimal? maxAmount = null,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<Payment, bool>>? filter = null;
            // Build dynamic filter
            if (invoiceId.HasValue ||
                fromDate.HasValue ||
                toDate.HasValue ||
                minAmount.HasValue ||
                maxAmount.HasValue)
            {
                filter = p =>
                    (!invoiceId.HasValue || p.InvoiceId == invoiceId.Value) &&
                    (!fromDate.HasValue || p.PaymentDate >= fromDate.Value) &&
                    (!toDate.HasValue || p.PaymentDate <= toDate.Value) &&
                    (!minAmount.HasValue || p.PaymentAmount >= minAmount.Value) &&
                    (!maxAmount.HasValue || p.PaymentAmount <= maxAmount.Value);
            }

            return await base.CountAsync(filter, cancellationToken);
        }

   
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


    }
}