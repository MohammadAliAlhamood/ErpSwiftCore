using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System.Linq.Expressions;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IFinancialRepositories
{
    public interface ICostCenterRepository : IMultiTenantRepository<CostCenter>
    {
        Task<Guid> CreateAsync(CostCenter center, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CostCenter> centers, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(CostCenter center, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid centerId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid centerId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);
         
        Task<CostCenter?> GetByIdAsync(Guid centerId, bool tracked = false, CancellationToken cancellationToken = default);

        Task<CostCenter?> GetByFilterAsync(Expression<Func<CostCenter, bool>> filter, bool tracked = false, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CostCenter>> GetAllAsync(Expression<Func<CostCenter, bool>>? filter = null, CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<CostCenter>> GetByIdsAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<CostCenter> Centers, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<CostCenter, bool>>? filter = null, CancellationToken cancellationToken = default);
         
        Task<IReadOnlyList<CostCenter>> SearchByNameAsync(string partialName, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CostCenter>> GetByNameAsync(string centerName, CancellationToken cancellationToken = default);
         

        Task<bool> ExistsByIdAsync(Guid centerId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string centerName, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCodeAsync(string costCenterCode, CancellationToken cancellationToken = default);

        Task<Dictionary<char, int>> GetCountByInitialAsync(CancellationToken cancellationToken = default);

        Task<int> CountAllAsync(CancellationToken cancellationToken = default);
    }
}