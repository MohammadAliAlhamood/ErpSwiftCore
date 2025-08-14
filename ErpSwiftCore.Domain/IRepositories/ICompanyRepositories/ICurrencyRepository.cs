using ErpSwiftCore.Domain.ICore;
using ErpSwiftCore.SharedKernel.Entities; 
namespace ErpSwiftCore.Domain.IRepositories.ICompanyRepositories
{
    public interface ICurrencyRepository : IRepository<Currency>
    { 
        Task<bool> DeleteAsync(Guid CurrencyId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> CurrencyIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<Guid> AddAsync(Currency Currency, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Currency Currency, CancellationToken cancellationToken = default);

        Task<Currency?> GetByIdAsync(Guid CurrencyId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Currency>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid CurrencyId, CancellationToken cancellationToken = default);
    }
}
