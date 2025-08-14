using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ErpSwiftCore.Domain.IRepositories.ICompanyRepositories;
namespace ErpSwiftCore.Persistence.Repositories.CompanyRepositories
{
    /// <summary>
    /// Repository for Currency entity with simple try/catch and no custom exceptions.
    /// </summary>
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext db, IUserProvider userProvider, IIncludeValidator<Currency> includeValidator) : base(db, userProvider, includeValidator)
        {
        }

        public async Task<Guid> AddAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(currency, true, cancellationToken);
                return currency.ID;
            }
            catch
            {
                // ريثرو الاستثناء الأصلي
                throw;
            }
        }

        public async Task<Currency?> GetByIdAsync(Guid currencyId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetAsync(c => c.ID == currencyId, cancellationToken: cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetAllAsync(null, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(currency, true, cancellationToken);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid currencyId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(c => c.ID == currencyId, cancellationToken: cancellationToken);
                if (entity == null)
                    return false;

                await base.RemoveAsync(entity, true, cancellationToken);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> currencyIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var entities = await base.GetAllAsync(c => currencyIds.Contains(c.ID), cancellationToken);
                if (!entities.Any())
                    return false;

                await base.RemoveRangeAsync(entities, true, cancellationToken);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var allIds = (await base.GetAllAsync(null, cancellationToken)).Select(c => c.ID);
                return await DeleteRangeAsync(allIds, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> ExistsAsync(Guid currencyId, CancellationToken cancellationToken = default)
        {
            try
            {
                return base.ExistsAsync(c => c.ID == currencyId, cancellationToken);
            }
            catch
            {
                throw;
            }
        }
    }
}
