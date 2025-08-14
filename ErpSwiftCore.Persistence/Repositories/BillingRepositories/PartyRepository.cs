using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IRepositories.IBillingRepositories;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Repositories.BillingRepositories
{
    public class PartyRepository :
        MultiTenantRepository<Party>,
        IPartyRepository
    {
        private static readonly Expression<Func<Party, object>>[] DefaultIncludes =
         {
             x => x.Supplier,
             x => x.Customer
         };

        public PartyRepository(AppDbContext db, ITenantProvider tenantProvider,
            IUserProvider userProvider, IIncludeValidator<Party> includeValidator)
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // ----------- [CRUD & Bulk] -----------

        public async Task<Guid> CreateAsync(Party Party, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(Party, true, cancellationToken);
            return Party.ID;
        }

        public async Task<IEnumerable<Guid>> CreateRangeAsync(IEnumerable<Party> Partys, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var p in Partys)
            {
                ids.Add(await CreateAsync(p, cancellationToken));
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(Party Party, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(Party, true, cancellationToken);
            return true;
        }

        // ----------- [Delete/Archive/Restore] -----------

        public async Task<bool> SoftDeleteAsync(Guid PartyId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(p => p.ID == PartyId, cancellationToken, DefaultIncludes);
            if (entity is not null)
            {
                await base.SoftDeleteAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> PartyIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in PartyIds)
            {
                ok &= await SoftDeleteAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAsync(Guid PartyId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(p => p.ID == PartyId, cancellationToken);
            if (entity is not null)
            {
                await base.RestoreAsync(entity, true, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> PartyIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in PartyIds)
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        // ----------- [Existence & Validation] -----------

        public Task<bool> ExistsAsync(Guid PartyId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(p => p.ID == PartyId, cancellationToken);

        // ----------- [Get/Query - Single] -----------

        public Task<Party?> GetByIdAsync(Guid PartyId, CancellationToken cancellationToken = default)
            => base.GetAsync(p => p.ID == PartyId, cancellationToken, DefaultIncludes);

        // ----------- [Get/Query - Bulk/Advanced] -----------

        public async Task<IReadOnlyList<Party>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await base.GetAllAsync(filter: null, cancellationToken, DefaultIncludes);
            return all.ToList().AsReadOnly();
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