using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService
{
    /// <summary>
    /// واجهة الأوامر الخاصة بإدارة وحدات القياس.
    /// </summary>
    public interface IUnitOfMeasurementCommandService
    {
        Task<Guid> CreateUnitOfMeasurementAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddUnitsOfMeasurementRangeAsync(IEnumerable<UnitOfMeasurement> units, CancellationToken cancellationToken = default);
        Task<bool> UpdateUnitOfMeasurementAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default);
        Task<bool> DeleteUnitOfMeasurementAsync(Guid unitId, CancellationToken cancellationToken = default);
        Task<bool> DeleteUnitsOfMeasurementRangeAsync(IEnumerable<Guid> unitIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllUnitsOfMeasurementAsync(CancellationToken cancellationToken = default);
    }
}
