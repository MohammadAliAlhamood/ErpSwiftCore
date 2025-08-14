// IMultiTenantWritableRepository.cs
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.ICore.Writable
{
    /// <summary>
    /// Write repository (إنشاء، تحديث، حذف فعلي) لـ AuditableEntity في بيئة متعدد المستأجرين.
    /// يرث فقط من IWriteRepository لتوفير أساليب الكتابة.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يجب أن يرث من AuditableEntity).</typeparam>
    public interface IMultiTenantWriteRepository<T> : IWriteRepository<T>
        where T : AuditableEntity
    {
        // لا تعريفات إضافية، لأن IWriteRepository<T> يغطي:
        // AddAsync, AddRangeAsync, UpdateAsync, RemoveAsync, RemoveRangeAsync
    }
}