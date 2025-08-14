// MultiTenantWritableRepository.cs
using ErpSwiftCore.Domain.ICore.Writable;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ErpSwiftCore.Persistence.Core.Writable
{
    /// <summary>
    /// Multi-tenant read/write repository:
    /// – يعزل البيانات حسب TenantID
    /// – يضبط حقول Audit عند الإنشاء والتحديث (CreatedBy/CreatedAt، UpdatedBy/UpdatedAt)
    /// – يدعم الحذف المنطقي (SoftDelete)، الاسترجاع (Restore)، والحذف النهائي (HardDelete)
    /// – يرث من MultiTenantSoftReadOnlyRepository للحصول على وظائف القراءة المؤمّنة ودعم قراءة المحذوفات منطقيًا
    /// – ينفذ واجهة IMultiTenantWriteRepository التي تجمع بين القراءة والكتابة متعدد الشركات
    /// </summary>
    /// <typeparam name="T">نوع يرث AuditableEntity</typeparam>
    public class MultiTenantWritableRepository<T> : IMultiTenantWriteRepository<T>
        where T : AuditableEntity
    {
        private readonly AppDbContext _db;
        private readonly ITenantProvider _tenantProvider;
        private readonly IUserProvider _userProvider; 
        public MultiTenantWritableRepository(
            AppDbContext db,
            ITenantProvider tenantProvider,
            IUserProvider userProvider)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        /// <summary>
        /// يضيف كيانًا جديدًا ويضبط الحقول المطلوبة:
        /// – TenantID من ITenantProvider
        /// – CreatedBy و CreatedAt
        /// </summary>
        /// <param name="entity">الكيان المراد إضافته</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task AddAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var tenantId = _tenantProvider.GetTenantId();
            var userId = _userProvider.GetUserId();
            var now = DateTime.UtcNow;

            entity.TenantID = tenantId;
            entity.CreatedBy = userId;
            entity.CreatedAt = now;

            await _db.Set<T>().AddAsync(entity, cancellationToken);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// يضيف مجموعة كيانات جديدة ويضبط الحقول المطلوبة لكل كيان:
        /// – TenantID من ITenantProvider
        /// – CreatedBy و CreatedAt دفعة واحدة
        /// </summary>
        /// <param name="entities">مجموعة الكيانات المراد إضافتها</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task AddRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var tenantId = _tenantProvider.GetTenantId();
            var userId = _userProvider.GetUserId();
            var now = DateTime.UtcNow;

            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "تحتوي القائمة على عنصر فارغ.");

                e.TenantID = tenantId;
                e.CreatedBy = userId;
                e.CreatedAt = now;
            }

            await _db.Set<T>().AddRangeAsync(entities, cancellationToken);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// يحدّث كيانًا موجودًا:
        /// – يتحقّق من وجود الكيان وانتمائه للـ Tenant الحالي
        /// – يحافظ على TenantID وحقول الإنشاء الأصلية
        /// – يضبط UpdatedBy و UpdatedAt
        /// </summary>
        /// <param name="entity">الكيان المحدث</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task UpdateAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var original = await _db.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == entity.ID, cancellationToken);

            if (original == null)
                throw new InvalidOperationException($"لا يمكن تحديث الكائن من النوع '{typeof(T).Name}' بالمعرّف '{entity.ID}' لأنه غير موجود.");

            if (original.TenantID != _tenantProvider.GetTenantId())
                throw new InvalidOperationException("لا يمكن تحديث كائن تابع لتنانت مختلفة.");

            // الحفاظ على الحقول الأصلية
            entity.TenantID = original.TenantID;
            entity.CreatedAt = original.CreatedAt;
            entity.CreatedBy = original.CreatedBy;

            // ضبط حقول التعديل
            entity.UpdatedBy = _userProvider.GetUserId();
            entity.UpdatedAt = DateTime.UtcNow;

            _db.Set<T>().Update(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// يحذف كيانًا فعليًا (Hard-Delete):
        /// – يتحقّق من تنانته
        /// </summary>
        /// <param name="entity">الكيان المراد حذفه نهائيًا</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task RemoveAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.TenantID != _tenantProvider.GetTenantId())
                throw new InvalidOperationException("لا يمكن حذف كائن تابع لتنانت مختلفة.");

            _db.Set<T>().Remove(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// يحذف مجموعة كيانات فعليًا (Hard-Delete) دفعة واحدة:
        /// – يتحقّق من تنانتها
        /// </summary>
        /// <param name="entities">مجموعة الكيانات المراد حذفها نهائيًا</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task RemoveRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var tenantId = _tenantProvider.GetTenantId();
            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "تحتوي القائمة على عنصر فارغ.");

                if (e.TenantID != tenantId)
                    throw new InvalidOperationException("لا يمكن حذف كائن تابع لتنانت مختلفة.");
            }

            _db.Set<T>().RemoveRange(entities);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// إجراء حذف منطقي (SoftDelete):
        /// – يتحقّق من تنانته
        /// – يضبط IsDeleted = true، UpdatedBy، و UpdatedAt
        /// ثم يحفظ التغيير.
        /// </summary>
        /// <param name="entity">الكيان المراد حذفه منطقيًا</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task SoftDeleteAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.TenantID != _tenantProvider.GetTenantId())
                throw new InvalidOperationException("لا يمكن حذف كائن تابع لتنانت مختلفة.");

            entity.IsDeleted = true;
            entity.UpdatedBy = _userProvider.GetUserId();
            entity.UpdatedAt = DateTime.UtcNow;

            _db.Set<T>().Update(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// إجراء حذف منطقي (SoftDelete) لمجموعة كيانات:
        /// – يتحقّق من تنانتها
        /// – يضبط IsDeleted = true، UpdatedBy، و UpdatedAt لكل كائن
        /// ثم يحفظ التغيير دفعة واحدة.
        /// </summary>
        /// <param name="entities">مجموعة الكيانات المراد حذفها منطقيًا</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task SoftDeleteRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var tenantId = _tenantProvider.GetTenantId();
            var userId = _userProvider.GetUserId();
            var now = DateTime.UtcNow;

            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "تحتوي القائمة على عنصر فارغ.");

                if (e.TenantID != tenantId)
                    throw new InvalidOperationException("لا يمكن حذف كائن تابع لتنانت مختلفة.");

                e.IsDeleted = true;
                e.UpdatedBy = userId;
                e.UpdatedAt = now;
            }

            _db.Set<T>().UpdateRange(entities);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// إجراء استرجاع (Restore) لكائن محذوف منطقيًا:
        /// – يتحقّق من تنانته
        /// – يضبط IsDeleted = false، UpdatedBy، و UpdatedAt
        /// ثم يحفظ التغيير.
        /// </summary>
        /// <param name="entity">الكيان المراد استرجاعه</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task RestoreAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.TenantID != _tenantProvider.GetTenantId())
                throw new InvalidOperationException("لا يمكن استرجاع كائن تابع لتنانت مختلفة.");

            entity.IsDeleted = false;
            entity.UpdatedBy = _userProvider.GetUserId();
            entity.UpdatedAt = DateTime.UtcNow;

            _db.Set<T>().Update(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// إجراء استرجاع (Restore) لمجموعة كيانات محذوفة منطقيًا:
        /// – يتحقّق من تنانتها
        /// – يضبط IsDeleted = false، UpdatedBy، و UpdatedAt لكل كائن
        /// ثم يحفظ التغيير دفعة واحدة.
        /// </summary>
        /// <param name="entities">مجموعة الكيانات المراد استرجاعها</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task RestoreRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var tenantId = _tenantProvider.GetTenantId();
            var userId = _userProvider.GetUserId();
            var now = DateTime.UtcNow;

            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "تحتوي القائمة على عنصر فارغ.");

                if (e.TenantID != tenantId)
                    throw new InvalidOperationException("لا يمكن استرجاع كائن تابع لتنانت مختلفة.");

                e.IsDeleted = false;
                e.UpdatedBy = userId;
                e.UpdatedAt = now;
            }

            _db.Set<T>().UpdateRange(entities);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// إجراء حذف نهائي (HardDelete) لكائن كان محذوفًا منطقيًا:
        /// – يتحقّق من تنانته
        /// </summary>
        /// <param name="entity">الكيان المراد حذفه نهائيًا</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task RemoveSoftDeletedAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.TenantID != _tenantProvider.GetTenantId())
                throw new InvalidOperationException("لا يمكن حذف كائن تابع لتنانت مختلفة.");

            _db.Set<T>().Remove(entity);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// إجراء حذف نهائي (HardDelete) لمجموعة كيانات كانت محذوفة منطقيًا:
        /// – يتحقّق من تنانتها
        /// </summary>
        /// <param name="entities">مجموعة الكيانات المراد حذفها نهائيًا</param>
        /// <param name="autoSave">إذا كانت true، يتم حفظ التغييرات فورًا</param>
        /// <param name="cancellationToken">رمز الإلغاء</param>
        public async Task RemoveSoftDeletedRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var tenantId = _tenantProvider.GetTenantId();
            foreach (var e in entities)
            {
                if (e == null)
                    throw new ArgumentNullException(nameof(entities), "تحتوي القائمة على عنصر فارغ.");

                if (e.TenantID != tenantId)
                    throw new InvalidOperationException("لا يمكن حذف كائن تابع لتنانت مختلفة.");
            }

            _db.Set<T>().RemoveRange(entities);
            if (autoSave)
                await _db.SaveChangesAsync(cancellationToken);
        }
    }
}