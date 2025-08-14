// ISecureSoftDeleteRepository.cs
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.ICore.SoftDelete
{
    /// <summary>
    /// Defines soft delete and restore operations over BaseEntity types.
    /// يشمل وضع IsDeleted = true وإعادة الاستعادة وحذف فعلي للسجلات المحذوفة منطقيًا.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من BaseEntity).</typeparam>
    public interface ISoftDeleteRepository<T> where T : BaseEntity
    {
        Task SoftDeleteAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task SoftDeleteRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task RestoreAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task RestoreRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task RemoveSoftDeletedAsync(
            T entity,
            bool autoSave = true,
            CancellationToken cancellationToken = default);

        Task RemoveSoftDeletedRangeAsync(
            IEnumerable<T> entities,
            bool autoSave = true,
            CancellationToken cancellationToken = default);
    }
}