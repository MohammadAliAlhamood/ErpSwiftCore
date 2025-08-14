// IMultiTenantSoftDeleteRepository.cs
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.ICore.SoftDelete
{
    /// <summary>
    /// Soft-delete repository لـ AuditableEntity في بيئة متعدد المستأجرين.
    /// يرث فقط من ISoftDeleteRepository لتوفير أساليب الحذف المنطقي والاستعادة.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من AuditableEntity).</typeparam>
    public interface IMultiTenantSoftDeleteRepository<T> : ISoftDeleteRepository<T>
        where T : AuditableEntity
    { }
}