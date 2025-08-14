using ErpSwiftCore.Domain.ICore.SoftDelete;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.TenantManagement.Interfaces;

namespace ErpSwiftCore.Persistence.Core.SoftDelete
{
    public class MultiTenantSoftDeleteRepository<T> : IMultiTenantSoftDeleteRepository<T> where T : AuditableEntity
    {
        private readonly AppDbContext _db;
        private readonly IUserProvider _userProvider;
        private readonly ITenantProvider _tenantProvider;

        public MultiTenantSoftDeleteRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task SoftDeleteAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            ValidateEntity(entity);
            EnsureTenantOwnership(entity);

            entity.IsDeleted = true;
            SetAuditFields(entity);

            _db.Set<T>().Update(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            ValidateEntities(entities);

            foreach (var e in entities)
            {
                EnsureTenantOwnership(e);
                e.IsDeleted = true;
                SetAuditFields(e);
                _db.Set<T>().Update(e);
            }

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RestoreAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            ValidateEntity(entity);
            EnsureTenantOwnership(entity);

            entity.IsDeleted = false;
            SetAuditFields(entity);

            _db.Set<T>().Update(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RestoreRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            ValidateEntities(entities);

            foreach (var e in entities)
            {
                EnsureTenantOwnership(e);
                e.IsDeleted = false;
                SetAuditFields(e);
                _db.Set<T>().Update(e);
            }

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveSoftDeletedAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            ValidateEntity(entity);
            EnsureTenantOwnership(entity);

            _db.Set<T>().Remove(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveSoftDeletedRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            ValidateEntities(entities);

            foreach (var e in entities)
                EnsureTenantOwnership(e);

            _db.Set<T>().RemoveRange(entities);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        private void EnsureTenantOwnership(T entity)
        {
            if (entity.TenantID != _tenantProvider.GetTenantId())
                throw new InvalidOperationException("Entity does not belong to the current tenant.");
        }

        private void SetAuditFields(T entity)
        {
            entity.UpdatedBy = _userProvider.GetUserId();
            entity.UpdatedAt = DateTime.UtcNow;
        }

        private void ValidateEntity(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
        }

        private void ValidateEntities(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            if (entities.Any(e => e == null))
                throw new ArgumentNullException(nameof(entities), "Collection contains null element");
        }
    }
}
