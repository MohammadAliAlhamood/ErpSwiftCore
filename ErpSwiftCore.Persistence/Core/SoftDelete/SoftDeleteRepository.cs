using ErpSwiftCore.Domain.ICore.SoftDelete;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ErpSwiftCore.Persistence.Core.SoftDelete
{
    public class SoftDeleteRepository<T> : ISoftDeleteRepository<T> where T : BaseEntity
    {
        private readonly DbContext _db;
        private readonly IUserProvider _userProvider;

        public SoftDeleteRepository(DbContext db, IUserProvider userProvider)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public virtual async Task SoftDeleteAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            EnsureEntityNotNull(entity);

            MarkAsDeleted(entity);
            _db.Set<T>().Update(entity);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task SoftDeleteRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            EnsureEntitiesNotNull(entities);

            var now = DateTime.UtcNow;
            var userId = _userProvider.GetUserId();

            foreach (var e in entities)
            {
                e.IsDeleted = true;
                e.UpdatedAt = now;
                e.UpdatedBy = userId;
                _db.Set<T>().Update(e);
            }

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RestoreAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            EnsureEntityNotNull(entity);

            MarkAsRestored(entity);
            _db.Set<T>().Update(entity);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RestoreRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            EnsureEntitiesNotNull(entities);

            var now = DateTime.UtcNow;
            var userId = _userProvider.GetUserId();

            foreach (var e in entities)
            {
                e.IsDeleted = false;
                e.UpdatedAt = now;
                e.UpdatedBy = userId;
                _db.Set<T>().Update(e);
            }

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RemoveSoftDeletedAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            EnsureEntityNotNull(entity);

            _db.Set<T>().Remove(entity);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RemoveSoftDeletedRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            EnsureEntitiesNotNull(entities);

            _db.Set<T>().RemoveRange(entities);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        #region Helper Methods

        private void MarkAsDeleted(T entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _userProvider.GetUserId();
        }

        private void MarkAsRestored(T entity)
        {
            entity.IsDeleted = false;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _userProvider.GetUserId();
        }

        private static void EnsureEntityNotNull(T? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
        }

        private static void EnsureEntitiesNotNull(IEnumerable<T>? entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "Contains null element");
            }
        }

        #endregion
    }
}
