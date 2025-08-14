using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CompanyService.UnitOfMeasurementService
{
    /// <summary>
    /// تنفيذ استعلامات إدارة وحدات القياس - جلب مفرد، جلب شامل، والتحقق من الوجود.
    /// يعتمد على IUnitOfWork فقط.
    /// </summary>
    public class UnitOfMeasurementQueryService : IUnitOfMeasurementQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfMeasurementQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<UnitOfMeasurement?> GetUnitOfMeasurementByIdAsync(Guid unitId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.UnitOfMeasurement.GetAsync(u => u.ID == unitId, cancellationToken);
        }

        public Task<IReadOnlyList<UnitOfMeasurement>> GetAllUnitsOfMeasurementAsync(CancellationToken cancellationToken = default)
        {
            return _unitOfWork.UnitOfMeasurement.GetAllAsync(cancellationToken);
        }

        public Task<bool> ExistsUnitOfMeasurementAsync(Guid unitId, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.UnitOfMeasurement.ExistsAsync(u => u.ID == unitId, cancellationToken);
        }
    }
}
