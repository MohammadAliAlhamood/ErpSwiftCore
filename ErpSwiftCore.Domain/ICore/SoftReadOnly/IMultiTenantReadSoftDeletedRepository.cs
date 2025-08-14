// IMultiTenantSoftReadOnlyRepository.cs
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.ICore.SoftReadOnly
{
    /// <summary>
    /// Read-only repository لـ AuditableEntity المحذوفة منطقيًا في بيئة متعدد المستأجرين.
    /// يرث فقط من IReadSoftDeletedRepository لتوفير أساليب قراءة السجلات المحذوفة منطقيًا.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من AuditableEntity).</typeparam>
    public interface IMultiTenantReadSoftDeletedRepository<T> : IReadSoftDeletedRepository<T>
        where T : AuditableEntity  {  }
}