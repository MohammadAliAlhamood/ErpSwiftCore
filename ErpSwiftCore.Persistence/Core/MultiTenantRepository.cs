using ErpSwiftCore.Domain.ICore;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core.Readable; 
using ErpSwiftCore.Persistence.Core.SoftDelete; 
using ErpSwiftCore.Persistence.Core.SoftReadOnly; 
using ErpSwiftCore.Persistence.Core.Writable; 
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.TenantManagement.Interfaces; 
using System.Linq.Expressions; 
namespace ErpSwiftCore.Persistence.Core
{
    /// <summary>
    /// يجمع العمليات متعدّدة المستأجرين لكل كيان T بدون كاش:
    ///   - القراءة (MultiTenantReadableRepository)
    ///   - الكتابة (MultiTenantWritableRepository)
    ///   - الحذف اللين (MultiTenantSoftDeleteRepository)
    ///   - القراءة من المحذوف (MultiTenantSoftReadOnlyRepository)
    /// ويطبّق واجهة IMultiTenantRepository<T>.
    /// </summary>
    public class MultiTenantRepository<T> : IMultiTenantRepository<T>
        where T : AuditableEntity
    {
        private readonly MultiTenantReadableRepository<T> _readable;
        private readonly MultiTenantWritableRepository<T> _writable;
        private readonly MultiTenantSoftDeleteRepository<T> _softDelete;
        private readonly MultiTenantSoftReadOnlyRepository<T> _softReadOnly;

        public MultiTenantRepository(
            AppDbContext db,
            ITenantProvider tenantProvider,
            IUserProvider userProvider,
            IIncludeValidator<T> includeValidator)
        {
            _readable = new MultiTenantReadableRepository<T>(db, tenantProvider, includeValidator);
            _softReadOnly = new MultiTenantSoftReadOnlyRepository<T>(db, tenantProvider, includeValidator);
            _softDelete = new MultiTenantSoftDeleteRepository<T>(db, tenantProvider, userProvider);
            _writable = new MultiTenantWritableRepository<T>(db, tenantProvider, userProvider);
        }

        // ====================== IMultiTenantReadRepository<T> ======================
        public Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
            => _readable.GetAsync(filter, cancellationToken, includes);

        public Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
            => _readable.GetAllAsync(filter, cancellationToken, includes);

        public Task<IReadOnlyList<T>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
            => _readable.GetPagedAsync(pageIndex, pageSize, filter, orderBy, ascending, cancellationToken, includes);

        public Task<bool> ExistsAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default)
            => _readable.ExistsAsync(filter, cancellationToken);

        public Task<int> CountAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
            => _readable.CountAsync(filter, cancellationToken);

        // ====================== IMultiTenantWriteRepository<T> ======================
        public Task AddAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _writable.AddAsync(entity, autoSave, cancellationToken);

        public Task AddRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _writable.AddRangeAsync(entities, autoSave, cancellationToken);

        public Task UpdateAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _writable.UpdateAsync(entity, autoSave, cancellationToken);

        public Task RemoveAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _writable.RemoveAsync(entity, autoSave, cancellationToken);

        public Task RemoveRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _writable.RemoveRangeAsync(entities, autoSave, cancellationToken);

        // ====================== IMultiTenantSoftDeleteRepository<T> ======================
        public Task SoftDeleteAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _softDelete.SoftDeleteAsync(entity, autoSave, cancellationToken);

        public Task SoftDeleteRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _softDelete.SoftDeleteRangeAsync(entities, autoSave, cancellationToken);

        public Task RestoreAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _softDelete.RestoreAsync(entity, autoSave, cancellationToken);

        public Task RestoreRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _softDelete.RestoreRangeAsync(entities, autoSave, cancellationToken);

        public Task RemoveSoftDeletedAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _softDelete.RemoveSoftDeletedAsync(entity, autoSave, cancellationToken);

        public Task RemoveSoftDeletedRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
            => _softDelete.RemoveSoftDeletedRangeAsync(entities, autoSave, cancellationToken);

        // ====================== IMultiTenantReadSoftDeletedRepository<T> ======================
        public Task<T?> GetOneSoftDeletedAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
            => _softReadOnly.GetOneSoftDeletedAsync(filter, cancellationToken, includes);

        public Task<IReadOnlyList<T>> GetAllSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
            => _softReadOnly.GetAllSoftDeletedAsync(filter, cancellationToken, includes);

        public Task<IReadOnlyList<T>> GetPagedSoftDeletedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
            => _softReadOnly.GetPagedSoftDeletedAsync(pageIndex, pageSize, filter, orderBy, ascending, cancellationToken, includes);

        public Task<int> CountSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
            => _softReadOnly.CountSoftDeletedAsync(filter, cancellationToken);
    }
}