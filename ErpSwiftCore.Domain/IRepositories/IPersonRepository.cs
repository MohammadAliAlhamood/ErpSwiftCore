using ErpSwiftCore.Domain.Abstractions;
using ErpSwiftCore.Domain.ICore;
using System.Linq.Expressions; 
namespace ErpSwiftCore.Domain.IRepositories
{
    /// <summary>
    /// واجهة عامة لمستودعات الكيانات التي ترث من Person (مثل Customer, Supplier, Lead).
    /// ترث من IMultiTenantRepository بحيث تغطي العمليات الأساسية متعددة المستأجرين،
    /// وتضيف وظائف خاصة بالتحقق من صحتها والبحث عن الحقول التعريفية للشخص (NationalID, Email, Phone).
    /// </summary>
    public interface IPersonRepository<T> : IMultiTenantRepository<T> where T : Person
    {
        Task<Guid> CreateAsync(T entity, CancellationToken ct = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
        Task<int> BulkImportAsync(IEnumerable<T> entities, CancellationToken ct = default);
        Task<bool> UpdateAsync(T entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
        Task<bool> DeleteAllAsync(CancellationToken ct = default);

        Task<bool> RestoreAsync(Guid id, CancellationToken ct = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
        Task<bool> RestoreAllAsync(CancellationToken ct = default);


        Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
        Task<bool> ExistsByNationalIdAsync(string nationalId, CancellationToken ct = default);
        Task<bool> ExistsByPhoneAsync(string phone, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(string email, Guid? excludingId = null, CancellationToken ct = default);
        Task<bool> ExistsByNationalIdAsync(string nationalId, Guid? excludingId = null, CancellationToken ct = default);
        Task<bool> ExistsByPhoneAsync(string phone, Guid? excludingId = null, CancellationToken ct = default);
        bool IsValidEmail(string email);
        bool IsValidPhoneNumber(string phone);
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetDeletedAllAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    }
}
