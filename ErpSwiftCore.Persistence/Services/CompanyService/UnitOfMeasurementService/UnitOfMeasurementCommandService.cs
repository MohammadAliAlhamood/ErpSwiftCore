using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using ErpSwiftCore.SharedKernel.Entities;
namespace ErpSwiftCore.Persistence.Services.CompanyService.UnitOfMeasurementService
{
    /// <summary>
    /// تنفيذ أوامر إدارة وحدات القياس – إنشاء، تعديل، حذف دفعات أو مفرد،
    /// مع الاعتماد الكامل على واجهة IUnitOfWork وطرق المستودع المعرفة.
    /// </summary>
    public class UnitOfMeasurementCommandService : IUnitOfMeasurementCommandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfMeasurementCommandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        public async Task<Guid> CreateUnitOfMeasurementAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UnitOfMeasurement.AddAsync(unit, cancellationToken);
        } 
        public async Task<IEnumerable<Guid>> AddUnitsOfMeasurementRangeAsync(IEnumerable<UnitOfMeasurement> units, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var u in units)
            {
                ids.Add(await _unitOfWork.UnitOfMeasurement.AddAsync(u, cancellationToken));
            }
             return ids;
        } 
        public async Task<bool> UpdateUnitOfMeasurementAsync(UnitOfMeasurement unit, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UnitOfMeasurement.UpdateAsync(unit, cancellationToken);
        } 
        public async Task<bool> DeleteUnitOfMeasurementAsync(Guid unitId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UnitOfMeasurement.DeleteAsync(unitId, cancellationToken);
        } 
        public async Task<bool> DeleteUnitsOfMeasurementRangeAsync(IEnumerable<Guid> unitIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UnitOfMeasurement.DeleteRangeAsync(unitIds, cancellationToken);
        } 
        public async Task<bool> DeleteAllUnitsOfMeasurementAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UnitOfMeasurement.DeleteAllAsync(cancellationToken);
        }
    }
}
