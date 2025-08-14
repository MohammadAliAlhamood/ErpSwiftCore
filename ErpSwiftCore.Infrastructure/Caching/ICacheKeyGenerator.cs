using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.Infrastructure.Caching
{
    /// <summary>
    /// Generates deterministic, collision-resistant cache keys for repository operations in a (possibly) multi-tenant ERP.
    /// </summary>
    public interface ICacheKeyGenerator
    {
        /// <summary>
        /// Generates a cache key based on:
        /// - اسم النوع (FullName of T)
        /// - معرّف الشركة (اختياري) TenantId
        /// - اسم الميثود (method)
        /// - Id (اختياري)
        /// - Filter object (اختياري) يتم تحويله إلى نصّ حتمي (Deterministic)
        /// - OrderBy object (اختياري) يتم تحويله إلى نصّ حتمي (Deterministic)
        /// - اتجاه الترتيب (Ascending/Descending)
        /// - قائمة خصائص الـ Includes (اختياري)
        ///
        /// إذا كان TenantId فارغًا أو null، يتم تجاهله (دعم للكيانات المشتركة
        /// بين جميع الشركات).
        /// </summary>
        /// <param name="tenantId">المعرّف النصّي للشركة (اختياري).</param>
        /// <param name="method">The repository method name (e.g., “GetByIdAsync”).</param>
        /// <param name="id">Optional entity GUID.</param>
        /// <param name="filter">Optional filter object (serialized deterministically).</param>
        /// <param name="orderBy">Optional ordering object (serialized deterministically).</param>
        /// <param name="ascending">Optional sort direction.</param>
        /// <param name="includes">Optional include property names.</param>
        /// <typeparam name="T">نوع الكيان (يجب أن يرث من BaseEntity).</typeparam>
        /// <returns>Hash (hex) طولها ثابت باستخدام SHA-256.</returns>
        string GenerateKey<T>(
            string? tenantId,
            string method,
            Guid? id = null,
            object? filter = null,
            object? orderBy = null,
            bool? ascending = null,
            params string[] includes
        ) where T : BaseEntity;
    }
}