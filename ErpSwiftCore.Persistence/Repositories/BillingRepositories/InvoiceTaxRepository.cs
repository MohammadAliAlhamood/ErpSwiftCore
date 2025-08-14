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
    /// Repository for InvoiceTax entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class InvoiceTaxRepository :
        MultiTenantRepository<InvoiceTax>,
        IInvoiceTaxRepository
    {
        private static readonly Expression<Func<InvoiceTax, object>>[] DefaultIncludes =
              {
             x => x.Invoice
         };

        public InvoiceTaxRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<InvoiceTax> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }


        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(InvoiceTax tax, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(tax, true, cancellationToken);
            return tax.ID;
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<InvoiceTax> taxes, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var t in taxes)
            {
                ids.Add(await CreateAsync(t, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(InvoiceTax tax, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(tax, true, cancellationToken);
            return true;
        }

        // ----------- [Delete / Hard Delete] -----------

        public async Task<bool> DeleteAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(
                i => i.ID == taxId, cancellationToken, DefaultIncludes);

            if (entity is not null)
            {
                await base.RemoveAsync(entity, true);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in taxIds)
            {
                ok &= await DeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await GetByInvoiceAsync(invoiceId, cancellationToken);
            var ok = true;
            foreach (var t in list)
            {
                ok &= await DeleteAsync(t.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            var ok = true;
            foreach (var t in all)
            {
                ok &= await DeleteAsync(t.ID, cancellationToken);
            }
            return ok;
        }

        // ----------- [Soft Delete / Archive / Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(
                i => i.ID == taxId, cancellationToken, DefaultIncludes);

            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in taxIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> SoftDeleteByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await GetByInvoiceAsync(invoiceId, cancellationToken);
            var ok = true;
            foreach (var t in list)
            {
                ok &= await SoftDeleteAsync(t.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(
                i => i.ID == taxId, 
                cancellationToken: cancellationToken);

            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in taxIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var restoredAny = false;
            var softDeleted = await base.GetAllSoftDeletedAsync(
                filter: t => t.InvoiceId == invoiceId,
                cancellationToken: cancellationToken);

            foreach (var t in softDeleted)
            {
                await base.RestoreAsync(t, true, cancellationToken);
                restoredAny = true;
            }

            return restoredAny;
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid taxId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(i => i.ID == taxId, cancellationToken);

        public async Task<bool> AnyForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(i => i.InvoiceId == invoiceId, cancellationToken);
        }

        // ----------- [Get/Query - Single & Details] -----------

        public Task<InvoiceTax?> GetByIdAsync(Guid taxId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == taxId, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<InvoiceTax>> GetByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return (IReadOnlyList<InvoiceTax>)await base.GetAllAsync(i => i.InvoiceId == invoiceId, cancellationToken, DefaultIncludes);
        }

        public Task<InvoiceTax?> GetByNameAsync(Guid invoiceId, string taxName, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.InvoiceId == invoiceId && i.TaxName == taxName, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<InvoiceTax>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<InvoiceTax>> GetByRateRangeAsync(decimal minRate, decimal maxRate, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceTax, bool>> filter = t =>
                t.TaxRate >= minRate &&
                t.TaxRate <= maxRate;

            var list = await base.GetAllAsync(
                filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }

        // ----------- [Counts & Stats] -----------

        public async Task<int> CountAsync(Guid? invoiceId = null, decimal? minRate = null, decimal? maxRate = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceTax, bool>>? filter = null;
            if (invoiceId.HasValue || minRate.HasValue || maxRate.HasValue)
            {
                filter = t => (!invoiceId.HasValue || t.InvoiceId == invoiceId.Value) &&
                               (!minRate.HasValue || t.TaxRate >= minRate.Value) &&
                               (!maxRate.HasValue || t.TaxRate <= maxRate.Value);
            }
            return await base.CountAsync(filter, cancellationToken);
        }
        
    
    }
}