using ErpSwiftCore.Domain.ICore.Readable;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;    // <-- AppDbContext
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ErpSwiftCore.Persistence.Core.Readable
{
    public class MultiTenantReadableRepository<T>
        : ReadableRepository<T>, IMultiTenantReadRepository<T>
        where T : AuditableEntity
    {
        private readonly ITenantProvider _tenantProvider;
        private readonly IIncludeValidator<T> _includeValidator;

        // 1) غيّرنا النوع من DbContext إلى AppDbContext
        public MultiTenantReadableRepository(
            AppDbContext db,
            ITenantProvider tenantProvider,
            IIncludeValidator<T> includeValidator)
            : base(db, includeValidator)
        {
            _tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
            _includeValidator = includeValidator ?? throw new ArgumentNullException(nameof(includeValidator));
        }

        public Guid TenantId => _tenantProvider.GetTenantId();

        public override Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            _includeValidator.Validate(includes);

            var combinedFilter = BuildCombinedFilter(filter);
            return base.GetAsync(combinedFilter, cancellationToken, includes);
        }

        public override Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            _includeValidator.Validate(includes);

            var combinedFilter = BuildCombinedFilter(filter);
            return base.GetAllAsync(combinedFilter, cancellationToken, includes);
        }

        public override Task<IReadOnlyList<T>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes)
        {
            _includeValidator.Validate(includes);

            var combinedFilter = BuildCombinedFilter(filter);
            return base.GetPagedAsync(
                pageIndex,
                pageSize,
                combinedFilter,
                orderBy,
                ascending,
                cancellationToken,
                includes
            );
        }

        public override Task<bool> ExistsAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var combinedFilter = BuildCombinedFilter(filter);
            return base.ExistsAsync(combinedFilter, cancellationToken);
        }

        public override Task<int> CountAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            var combinedFilter = BuildCombinedFilter(filter);
            return base.CountAsync(combinedFilter, cancellationToken);
        }

        // يجمع فلاتر TenantId و IsDeleted مع الفلتر الخارجي (إن وجد)
        private Expression<Func<T, bool>> BuildCombinedFilter(Expression<Func<T, bool>>? externalFilter)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var param = Expression.Parameter(typeof(T), "e");

            var tenantCheck = Expression.Equal(
                Expression.Property(param, nameof(AuditableEntity.TenantID)),
                Expression.Constant(tenantId)
            );
            var isNotDeletedCheck = Expression.Equal(
                Expression.Property(param, nameof(BaseEntity.IsDeleted)),
                Expression.Constant(false)
            );

            Expression body = Expression.AndAlso(tenantCheck, isNotDeletedCheck);

            if (externalFilter != null)
            {
                var replaced = ReplaceParameter(externalFilter.Body, externalFilter.Parameters[0], param);
                body = Expression.AndAlso(body, replaced!);
            }

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        private static Expression ReplaceParameter(
            Expression expression,
            ParameterExpression oldParam,
            ParameterExpression newParam)
        {
            return new ReplaceParameterVisitor(oldParam, newParam).Visit(expression)!;
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParam;
            private readonly ParameterExpression _newParam;

            public ReplaceParameterVisitor(ParameterExpression oldParam, ParameterExpression newParam)
            {
                _oldParam = oldParam;
                _newParam = newParam;
            }

            protected override Expression VisitParameter(ParameterExpression node)
                => node == _oldParam ? _newParam : base.VisitParameter(node);
        }
    }
}
