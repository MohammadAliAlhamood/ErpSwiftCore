using ErpSwiftCore.Domain.ICore.Readable;
using ErpSwiftCore.Domain.ICore.SoftDelete;
using ErpSwiftCore.Domain.ICore.SoftReadOnly;
using ErpSwiftCore.Domain.ICore.Writable;
using ErpSwiftCore.SharedKernel.Base; 

namespace ErpSwiftCore.Domain.ICore
{
    /// <summary>
    /// واجهة عامة تجمع جميع العمليات الأربع:
    ///   IReadRepository<T>, IWriteRepository<T>,
    ///   ISoftDeleteRepository<T>, IReadSoftDeletedRepository<T>
    /// </summary>
    public interface IRepository<T> :
        IReadRepository<T>,
        IWriteRepository<T>,
        ISoftDeleteRepository<T>,
        IReadSoftDeletedRepository<T>
        where T : BaseEntity
    {
        // لا حاجة لتعريف أيّ دوال إضافية هنا،
        // لأنه سيتم توريث كل التواقيع (signatures) من الواجهات الأربع.
    }
}