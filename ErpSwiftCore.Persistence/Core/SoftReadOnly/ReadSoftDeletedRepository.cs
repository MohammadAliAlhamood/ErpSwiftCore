using ErpSwiftCore.Domain.ICore.SoftReadOnly;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.SharedKernel.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Core.SoftReadOnly
{
    public class ReadSoftDeletedRepository<T> : IReadSoftDeletedRepository<T> where T : BaseEntity
    {
        private readonly DbContext _db;
        private readonly IIncludeValidator<T>? _validator;

        public ReadSoftDeletedRepository(DbContext db, IIncludeValidator<T> validator)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public ReadSoftDeletedRepository(DbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public virtual async Task<T?> GetOneSoftDeletedAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            EnsureFilterNotNull(filter);
            var query = BuildBaseQuery(includes)
                .Where(filter);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            var query = BuildBaseQuery(includes);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedSoftDeletedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            var query = BuildBaseQuery(includes);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<int> CountSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            var query = _db.Set<T>()
                .IgnoreQueryFilters()
                .Where(e => e.IsDeleted);

            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync(cancellationToken);
        }

        #region Helper Methods

        private IQueryable<T> BuildBaseQuery(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(e => e.IsDeleted);

            if (includes?.Any() == true)
            {
                _validator?.Validate(includes);
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query;
        }

        private static void EnsureFilterNotNull(Expression<Func<T, bool>>? filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
        }

        #endregion
    }
}
