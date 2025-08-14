using ErpSwiftCore.Domain.ICore.Readable;
using ErpSwiftCore.Domain.ICore.SoftDelete;
using ErpSwiftCore.Domain.ICore.SoftReadOnly;
using ErpSwiftCore.Domain.ICore.Writable;
using ErpSwiftCore.SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.ICore
{
    public interface IMultiTenantRepository<T> :
       IMultiTenantReadRepository<T>,
       IMultiTenantWriteRepository<T>,
       IMultiTenantSoftDeleteRepository<T>,
       IMultiTenantReadSoftDeletedRepository<T>
       where T : AuditableEntity
    {
        // لا حاجة لتعريف دوال إضافية هنا
        // ستُرث جميع التواقيع من الواجهات الأربع الخاصة بالـ Multi-Tenant
    }
}