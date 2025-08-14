using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.ICore.Readable
{
    /// <summary>
    /// Read-only repository for AuditableEntity في بيئة متعدد المستأجرين.
    /// يرث فقط من IReadRepository لتوفير أساليب القراءة (غير المحذوفة منطقيًا).
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من AuditableEntity).</typeparam>
    public interface IMultiTenantReadRepository<T> : IReadRepository<T>
        where T : AuditableEntity  { }
}