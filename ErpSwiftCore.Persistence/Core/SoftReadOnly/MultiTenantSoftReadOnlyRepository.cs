using ErpSwiftCore.Domain.ICore.SoftReadOnly;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Core.SoftReadOnly
{
    /// <summary>
    /// Multi-tenant, soft-read-only repository:
    /// - يتحقق من صلاحية includes
    /// - يضيف تلقائيًا تصفية TenantID و IsDeleted = true
    /// </summary>
    public class MultiTenantSoftReadOnlyRepository<T> : IMultiTenantReadSoftDeletedRepository<T>
        where T : AuditableEntity
    {
        private readonly AppDbContext _db;
        private readonly ITenantProvider _tenantProvider;
        private readonly IIncludeValidator<T> _validator;

        public MultiTenantSoftReadOnlyRepository(
            AppDbContext db,
            ITenantProvider tenantProvider,
            IIncludeValidator<T> validator)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<T?> GetOneSoftDeletedAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            EnsureFilterNotNull(filter);
            _validator.Validate(includes);

            var query = BuildBaseQuery(includes)
                .Where(filter);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            _validator.Validate(includes);

            var query = BuildBaseQuery(includes);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetPagedSoftDeletedAsync(
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

            _validator.Validate(includes);

            var query = BuildBaseQuery(includes);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);

            return await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public Task<int> CountSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            var baseQuery = _db.Set<T>()
                .IgnoreQueryFilters()
                .Where(e => e.TenantID == _tenantProvider.GetTenantId() && e.IsDeleted);

            if (filter != null)
                baseQuery = baseQuery.Where(filter);

            return baseQuery
                .AsNoTracking()
                .CountAsync(cancellationToken);
        }

        #region Helper Methods

        private IQueryable<T> BuildBaseQuery(params Expression<Func<T, object>>[] includes)
        {
            var tenantId = _tenantProvider.GetTenantId();

            // نبدأ بالكيانات مع تجاهل أي فلتر افتراضي وتصفية حسب التيننت و IsDeleted
            IQueryable<T> query = _db.Set<T>()
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(e => e.TenantID == tenantId && e.IsDeleted);

            // نضيف الـ Includes بعد التحقق من صحتها
            foreach (var include in includes)
            {
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
