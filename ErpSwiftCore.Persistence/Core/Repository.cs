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
    /// Repository موحد لكل كيان T (نقي وخالٍ من الكاش):
    ///   - القراءة (ReadableRepository)
    ///   - الكتابة (WritableRepository)
    ///   - الحذف اللين (SoftDeleteRepository)
    ///   - قراءة المحذوف (ReadSoftDeletedRepository)
    /// ويطبق واجهة IRepository<T>.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ReadableRepository<T> _readable;
        private readonly WritableRepository<T> _writable;
        private readonly SoftDeleteRepository<T> _softDelete;
        private readonly ReadSoftDeletedRepository<T> _softReadOnly;

        public Repository(
            AppDbContext db,
            IUserProvider userProvider,
            IIncludeValidator<T> includeValidator)
        {
            _readable = new ReadableRepository<T>(db, includeValidator);
            _writable = new WritableRepository<T>(db, userProvider);
            _softDelete = new SoftDeleteRepository<T>(db, userProvider);
            _softReadOnly = new ReadSoftDeletedRepository<T>(db, includeValidator);
        }

        // ====================== IReadRepository<T> ======================
        public Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            return _readable.GetAsync(filter, cancellationToken, includes);
        }

        public Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            return _readable.GetAllAsync(filter, cancellationToken, includes);
        }

        public Task<IReadOnlyList<T>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            return _readable.GetPagedAsync(
                pageIndex, pageSize,
                filter, orderBy, ascending,
                cancellationToken, includes);
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            return _readable.ExistsAsync(filter, cancellationToken);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            return _readable.CountAsync(filter, cancellationToken);
        }

        // ====================== IWriteRepository<T> ======================
        public Task AddAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _writable.AddAsync(entity, autoSave, cancellationToken);
        }

        public Task AddRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _writable.AddRangeAsync(entities, autoSave, cancellationToken);
        }

        public Task UpdateAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _writable.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public Task RemoveAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _writable.RemoveAsync(entity, autoSave, cancellationToken);
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _writable.RemoveRangeAsync(entities, autoSave, cancellationToken);
        }

        // ====================== ISoftDeleteRepository<T> ======================
        public Task SoftDeleteAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _softDelete.SoftDeleteAsync(entity, autoSave, cancellationToken);
        }

        public Task SoftDeleteRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _softDelete.SoftDeleteRangeAsync(entities, autoSave, cancellationToken);
        }

        public Task RestoreAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _softDelete.RestoreAsync(entity, autoSave, cancellationToken);
        }

        public Task RestoreRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _softDelete.RestoreRangeAsync(entities, autoSave, cancellationToken);
        }

        public Task RemoveSoftDeletedAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _softDelete.RemoveSoftDeletedAsync(entity, autoSave, cancellationToken);
        }

        public Task RemoveSoftDeletedRangeAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return _softDelete.RemoveSoftDeletedRangeAsync(entities, autoSave, cancellationToken);
        }

        // ====================== IReadSoftDeletedRepository<T> ======================
        public Task<T?> GetOneSoftDeletedAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            return _softReadOnly.GetOneSoftDeletedAsync(filter, cancellationToken, includes);
        }

        public Task<IReadOnlyList<T>> GetAllSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            return _softReadOnly.GetAllSoftDeletedAsync(filter, cancellationToken, includes);
        }

        public Task<IReadOnlyList<T>> GetPagedSoftDeletedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            return _softReadOnly.GetPagedSoftDeletedAsync(
                pageIndex, pageSize,
                filter, orderBy, ascending,
                cancellationToken, includes);
        }

        public Task<int> CountSoftDeletedAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            return _softReadOnly.CountSoftDeletedAsync(filter, cancellationToken);
        }
    }
}