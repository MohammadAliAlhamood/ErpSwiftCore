using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ErpSwiftCore.Domain.IRepositories.IUnitOfMeasurementRepositories;
namespace ErpSwiftCore.Persistence.Repositories.CompanyRepositories
{
    public class UnitOfMeasurementRepository : Repository<UnitOfMeasurement>, IUnitOfMeasurementRepository
    {
        public UnitOfMeasurementRepository(AppDbContext db, IUserProvider userProvider, IIncludeValidator<UnitOfMeasurement> includeValidator) : base(db, userProvider, includeValidator)
        {
        }

        public async Task<Guid> AddAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(unit, true, cancellationToken);
                return unit.ID;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid unitId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(u => u.ID == unitId, cancellationToken: cancellationToken);
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

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> unitIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var entities = await base.GetAllAsync(u => unitIds.Contains(u.ID), cancellationToken);
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
                var allIds = (await base.GetAllAsync(null, cancellationToken)).Select(u => u.ID);
                return await DeleteRangeAsync(allIds, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> ExistsAsync(Guid unitId, CancellationToken cancellationToken = default)
        {
            try
            {
                return base.ExistsAsync(u => u.ID == unitId, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<UnitOfMeasurement>> GetAllAsync(CancellationToken cancellationToken = default)
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

        public Task<UnitOfMeasurement?> GetByIdAsync(Guid unitId, CancellationToken cancellationToken = default)
        {
            try
            {
                return base.GetAsync(u => u.ID == unitId, cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(unit, true, cancellationToken);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
