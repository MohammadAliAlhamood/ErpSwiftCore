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
    /// Repository for InvoiceApproval entity.
    /// Implements advanced scenarios: CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, etc.,
    /// within a multi-tenant context.
    /// </summary>
    public class InvoiceApprovalRepository :
        MultiTenantRepository<InvoiceApproval>,
        IInvoiceApprovalRepository
    {
        private static readonly Expression<Func<InvoiceApproval, object>>[] DefaultIncludes =
        {  x => x.Invoice};

        public InvoiceApprovalRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<InvoiceApproval> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(InvoiceApproval approval, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(approval, true, cancellationToken);
            return approval.ID;
        }

        public async Task<IEnumerable<Guid>> CreateRangeAsync(IEnumerable<InvoiceApproval> approvals, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var a in approvals)
            {
                ids.Add(await CreateAsync(a, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(InvoiceApproval approval, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(approval, true, cancellationToken);
            return true;
        }

        // ----------- [Delete / Hard Delete] -----------

        public async Task<bool> DeleteAsync(Guid approvalId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(
                i => i.ID == approvalId, cancellationToken: cancellationToken);

            if (entity is not null)
            {
                await base.RemoveAsync(entity, true);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> approvalIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in approvalIds)
            {
                ok &= await DeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            var ok = true;
            foreach (var a in all)
            {
                ok &= await DeleteAsync(a.ID, cancellationToken);
            }
            return ok;
        }

        // ----------- [Soft Delete / Archive / Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid approvalId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(  i => i.ID == approvalId, cancellationToken: cancellationToken);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> approvalIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in approvalIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> SoftDeleteByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await GetByInvoiceAsync(invoiceId, cancellationToken);
            var ok = true;
            foreach (var a in list)
            {
                ok &= await SoftDeleteAsync(a.ID, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid approvalId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(i => i.ID == approvalId, cancellationToken: cancellationToken);

            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> approvalIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in approvalIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var restoredAny = false;
            var softDeleted = await base.GetAllSoftDeletedAsync(filter: i => i.InvoiceId == invoiceId, cancellationToken: cancellationToken);
            foreach (var a in softDeleted)
            {
                await base.RestoreAsync(a, true, cancellationToken);
                restoredAny = true;
            }
            return restoredAny;
        }
        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid approvalId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(i => i.ID == approvalId, cancellationToken);
        public async Task<bool> AnyForApproverAsync(Guid approverId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(i => i.UpdatedBy == approverId, cancellationToken);
        }
        public async Task<bool> AnyForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await base.ExistsAsync(i => i.InvoiceId == invoiceId, cancellationToken);
        }
        // ----------- [Counts & Stats] -----------
        public async Task<int> CountAsync(Guid? invoiceId = null, Guid? approverId = null, InvoiceApprovalStatus? status = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceApproval, bool>>? filter = null;

            if (invoiceId.HasValue || approverId.HasValue || status.HasValue)
            {
                filter = i =>
                    (!invoiceId.HasValue || i.InvoiceId == invoiceId.Value) &&
                    (!approverId.HasValue || i.UpdatedBy == approverId.Value) &&
                    (!status.HasValue || i.Status == status.Value);
            }

            return await base.CountAsync(filter, cancellationToken);
        }

        public Task<int> CountByApproverAsync(Guid approverId, CancellationToken cancellationToken = default)
            => base.CountAsync(i => i.UpdatedBy == approverId, cancellationToken);

        public Task<int> CountByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
            => base.CountAsync(i => i.InvoiceId == invoiceId, cancellationToken);

        // ----------- [Get/Query - Single & Details] -----------

        public Task<InvoiceApproval?> GetByIdAsync(Guid approvalId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == approvalId, cancellationToken: cancellationToken);

        public Task<InvoiceApproval?> GetByIdWithDetailsAsync(Guid approvalId, CancellationToken cancellationToken = default)
            => base.GetAsync(i => i.ID == approvalId, cancellationToken);

        // ----------- [Get/Query - Bulk/Advanced] -----------
        public async Task<IReadOnlyList<InvoiceApproval>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken: cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
        }
        public async Task<IReadOnlyList<InvoiceApproval>> GetByApproverAsync(Guid approverId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(i => i.UpdatedBy == approverId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }
        public async Task<IReadOnlyList<InvoiceApproval>> GetByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(i => i.InvoiceId == invoiceId, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }
        public async Task<IReadOnlyList<InvoiceApproval>> GetByStatusAsync(InvoiceApprovalStatus status, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(i => i.Status == status, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }
        public async Task<IReadOnlyList<InvoiceApproval>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            Expression<Func<InvoiceApproval, bool>> filter = i => i.UpdatedAt >= from && i.UpdatedAt <= to;
            var list = await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }
        public async Task<IReadOnlyList<InvoiceApproval>> GetPendingAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(i => i.Status == InvoiceApprovalStatus.Pending, cancellationToken, DefaultIncludes);
            return list.ToList().AsReadOnly();
        }
    }
}