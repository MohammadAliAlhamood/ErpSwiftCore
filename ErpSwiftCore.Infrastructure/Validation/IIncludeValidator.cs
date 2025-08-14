using ErpSwiftCore.SharedKernel.Base;
using System.Linq.Expressions;

namespace ErpSwiftCore.Infrastructure.Validation
{
    /// <summary>
    /// Validates that requested include expressions target valid navigation properties on T.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من BaseEntity).</typeparam>
    public interface IIncludeValidator<T> where T : BaseEntity
    {
        /// <param name="includes">Include expressions to validate.</param>
        /// <exception cref="System.Security.SecurityException">
        /// إذا كانت أي خاصية غير موجودة أو المسار المتداخل غير صالح.
        /// </exception>
        void Validate(params Expression<Func<T, object>>[] includes);
    }
}