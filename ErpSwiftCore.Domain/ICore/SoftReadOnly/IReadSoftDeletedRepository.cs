using ErpSwiftCore.SharedKernel.Base;
using System.Linq.Expressions;

namespace ErpSwiftCore.Domain.ICore.SoftReadOnly
{
    /// <summary>
    /// Defines read-only operations over soft-deleted BaseEntity types.
    /// يدعم جلب السجلات المحذوفة منطقيًا (IsDeleted = true) مع تضمين العلاقات.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من BaseEntity).</typeparam>
    public interface IReadSoftDeletedRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<IReadOnlyList<T>> GetPagedSoftDeletedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<T?> GetOneSoftDeletedAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] includes);

        Task<int> CountSoftDeletedAsync(
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default);
    }
}
