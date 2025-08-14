using ErpSwiftCore.Domain.Entities.EntityCRM;
using System.Linq.Expressions;

namespace ErpSwiftCore.Domain.IRepositories.ICRMRepositories
{
    /// <summary>
    /// IRepository خاص بكيان Supplier (يرث من IPersonRepository ليحصل على البحث وحالات NationalID/Email/Phone).
    /// يضيف عمليات CRUD، الحذف المنطقي، التصفية متعددة الصفحات، والتحقق من وجود SupplierCode.
    /// </summary>
    public interface ISupplierRepository : IPersonRepository<Supplier>
    {
          Task<bool> ExistsBySupplierCodeAsync(string supplierCode, CancellationToken ct = default);
    }
}