using ErpSwiftCore.Domain.Entities.EntityCRM;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.ICRMRepositories
{
    /// <summary>
    /// IRepository خاص بكيان Customer (يرث من IPersonRepository ليحصل على البحث وحالات NationalID/Email/Phone).
    /// يضيف عمليات CRUD، الحذف المنطقي، التصفية متعددة الصفحات، والتحقق من عمليات الوجود
    /// </summary>
    public interface ICustomerRepository : IPersonRepository<Customer>
    {
        Task<Guid> CreateAsync(Customer customer, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Customer> customers, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> customerIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> customerIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
 
        Task<Customer?> GetByIdAsync(Guid customerId,  CancellationToken cancellationToken = default);

        Task<Customer?> GetByFilterAsync(Expression<Func<Customer, bool>> filter,  CancellationToken cancellationToken = default);
         
        Task<bool> ExistsByIdAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCustomerCodeAsync(string customerCode, CancellationToken cancellationToken = default);
    }
}