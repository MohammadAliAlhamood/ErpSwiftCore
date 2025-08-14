using ErpSwiftCore.Domain.ICore;
using ErpSwiftCore.SharedKernel.Entities; 
namespace ErpSwiftCore.Domain.IRepositories.IUnitOfMeasurementRepositories
{
    public interface IUnitOfMeasurementRepository : IRepository<UnitOfMeasurement>
    {  
        Task<bool> DeleteAsync(Guid UnitOfMeasurementId, CancellationToken cancellationToken = default); 
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> UnitOfMeasurementIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<Guid> AddAsync(UnitOfMeasurement UnitOfMeasurement, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(UnitOfMeasurement UnitOfMeasurement, CancellationToken cancellationToken = default);

        Task<UnitOfMeasurement?> GetByIdAsync(Guid UnitOfMeasurementId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<UnitOfMeasurement>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid UnitOfMeasurementId, CancellationToken cancellationToken = default);
    }
}
