using ErpSwiftCore.Domain.ICore.Readable;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace ErpSwiftCore.Persistence.Core.Readable
{
    public class ReadableRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _db;
        private readonly IIncludeValidator<T>? _validator;
        public ReadableRepository(AppDbContext db, IIncludeValidator<T> validator)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public ReadableRepository(AppDbContext db)
        {
            _db = db;
        }
        public virtual async Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            _validator?.Validate(includes);
            var combinedFilter = ApplySoftDeleteFilter(filter);
            // نبدأ بالـ DbSet مباشرةً
            IQueryable<T> query = _db.Set<T>().AsNoTracking().Where(combinedFilter);
            foreach (var inc in includes)
                query = query.Include(inc);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        public virtual Task<bool> ExistsAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            var combinedFilter = ApplySoftDeleteFilter(filter);

            IQueryable<T> baseSet = _db.Set<T>().AsNoTracking().Where(combinedFilter);
            return baseSet.AnyAsync(combinedFilter, cancellationToken);
        }

        public virtual Task<int> CountAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            var combinedFilter = ApplySoftDeleteFilter(filter);
            IQueryable<T> baseSet = _db.Set<T>().AsNoTracking().Where(combinedFilter);
            return baseSet.CountAsync(combinedFilter, cancellationToken);
        }

        public virtual Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            var combinedFilter = ApplySoftDeleteFilter(filter);
            return ExecuteListAsync(combinedFilter, null, null, 0, 0, cancellationToken, includes);
        }

        public virtual Task<IReadOnlyList<T>> GetPagedAsync(
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

            var combinedFilter = ApplySoftDeleteFilter(filter);
            return ExecuteListAsync(combinedFilter, orderBy, ascending, pageIndex, pageSize, cancellationToken, includes);
        }

        private async Task<IReadOnlyList<T>> ExecuteListAsync(
            Expression<Func<T, bool>>? filter,
            Expression<Func<T, object>>? orderBy,
            bool? ascending,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] includes)
        {
            _validator?.Validate(includes);

            IQueryable<T> query = _db.Set<T>().AsNoTracking().Where(filter);

            foreach (var inc in includes)
                query = query.Include(inc);

            if (orderBy != null)
            {
                query = ascending == true
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            if (pageSize > 0)
            {
                query = query
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
            }

            return await query.ToListAsync(cancellationToken);
        }

        private static Expression<Func<T, bool>> ApplySoftDeleteFilter(
            Expression<Func<T, bool>>? filter)
        {
            var param = Expression.Parameter(typeof(T), "e");
            var isDeletedProp = Expression.Property(param, nameof(BaseEntity.IsDeleted));
            var notDeleted = Expression.Equal(isDeletedProp, Expression.Constant(false));

            if (filter == null)
            {
                return Expression.Lambda<Func<T, bool>>(notDeleted, param);
            }

            var invokedExpr = Expression.Invoke(filter, param);
            var combined = Expression.AndAlso(notDeleted, invokedExpr);
            return Expression.Lambda<Func<T, bool>>(combined, param);
        }
    }
}
