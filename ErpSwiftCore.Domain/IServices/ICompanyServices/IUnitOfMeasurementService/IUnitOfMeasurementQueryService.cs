using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService
{
    /// <summary>
    /// واجهة الاستعلامات الخاصة بإدارة وحدات القياس.
    /// </summary>
    public interface IUnitOfMeasurementQueryService
    {
        /// <summary>
        /// الحصول على وحدة قياس بواسطة المعرف.
        /// </summary>
        Task<UnitOfMeasurement?> GetUnitOfMeasurementByIdAsync(Guid unitId, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على جميع وحدات القياس.
        /// </summary>
        Task<IReadOnlyList<UnitOfMeasurement>> GetAllUnitsOfMeasurementAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// التحقق من وجود وحدة قياس بواسطة المعرف.
        /// </summary>
        Task<bool> ExistsUnitOfMeasurementAsync(Guid unitId, CancellationToken cancellationToken = default);
    }
}
