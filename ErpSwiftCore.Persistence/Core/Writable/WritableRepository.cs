using ErpSwiftCore.Domain.ICore.Writable;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ErpSwiftCore.Persistence.Core.Writable
{
    /// <summary>
    /// EF Core–based implementation of IWriteRepository<T> (Single-Tenant).
    /// – يضبط حقول الإنشاء CreatedBy/CreatedAt تلقائيًا عند Add.
    /// – يحافظ على حقول الإنشاء الأصلية عند Update، ويضبط حقول التعديل UpdatedBy/UpdatedAt.
    /// – يدعم RemoveAsync/RemoveRangeAsync بصيغتها غير المتزامنة، مع تمرير CancellationToken.
    /// – لا يتعامل مع الحذف المنطقي هنا (الحذف حقيقي).
    /// </summary>
    /// <typeparam name="T">نوع مشتق من BaseEntity</typeparam>
    public class WritableRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly DbContext _db;
        private readonly IUserProvider _userProvider;

        public WritableRepository(DbContext db, IUserProvider userProvider)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public virtual async Task AddAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // ضبط حقول الإنشاء
            var now = DateTime.UtcNow;
            var userId = _userProvider.GetUserId();
            entity.CreatedAt = now;
            entity.CreatedBy = userId;

            await _db.Set<T>().AddAsync(entity, cancellationToken);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task AddRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var now = DateTime.UtcNow;
            var userId = _userProvider.GetUserId();

            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "Collection contains null element");
                e.CreatedAt = now;
                e.CreatedBy = userId;
            }

            await _db.Set<T>().AddRangeAsync(entities, cancellationToken);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // استرجاع النسخة الأصلية من القاعدة لضمان عدم فقدان CreatedAt/CreatedBy
            var existing = await _db.Set<T>()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.ID == entity.ID, cancellationToken);

            if (existing == null)
                throw new InvalidOperationException(
                    $"لا يمكن تحديث الكائن من النوع '{typeof(T).Name}' بالمعرّف '{entity.ID}' لأنه غير موجود.");

            // الحفاظ على حقول الإنشاء الأصلية
            entity.CreatedAt = existing.CreatedAt;
            entity.CreatedBy = existing.CreatedBy;

            // ضبط حقول التعديل
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _userProvider.GetUserId();

            _db.Set<T>().Update(entity);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RemoveAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // الحذف هنا حقيقي (Physical Delete)
            _db.Set<T>().Remove(entity);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RemoveRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "Collection contains null element");
            }

            // الحذف هنا حقيقي (Physical Delete)
            _db.Set<T>().RemoveRange(entities);

            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
