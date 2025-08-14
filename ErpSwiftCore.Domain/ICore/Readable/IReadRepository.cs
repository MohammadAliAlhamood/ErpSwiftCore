using System.Linq.Expressions;
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.ICore.Readable
{
    /// <summary>
    /// Defines basic read-only operations over BaseEntity types,
    /// with filtering, paging, includes, existence and count support.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من BaseEntity).</typeparam>
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<IReadOnlyList<T>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<bool> ExistsAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default);
    }
}
