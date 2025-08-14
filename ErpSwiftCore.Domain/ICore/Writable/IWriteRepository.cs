// ISecureWritableRepository.cs
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.ICore.Writable
{
    /// <summary>
    /// Defines basic write operations over BaseEntity types:
    /// إضافة، تحديث، حذف (فعلي).
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من BaseEntity).</typeparam>
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task AddAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task AddRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task RemoveRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default);
    }
}