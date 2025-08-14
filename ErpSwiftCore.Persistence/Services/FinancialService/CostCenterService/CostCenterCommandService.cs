using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService; 
namespace ErpSwiftCore.Persistence.Services.FinancialService.CostCenterService
{
    public class CostCenterCommandService : ICostCenterCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly ICostCenterValidationService _validation;
        public CostCenterCommandService(IMultiTenantUnitOfWork unitOfWork, ICostCenterValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }
        public async Task<Guid> CreateCostCenterAsync(CostCenter center, CancellationToken cancellationToken = default)
        {
            if (center == null) throw new ArgumentNullException(nameof(center));
            if (string.IsNullOrWhiteSpace(center.CenterName))
                throw new InvalidOperationException("CenterName is required.");

            if (await _unitOfWork.CostCenter.ExistsByCodeAsync(center.Code!, cancellationToken))
                throw new InvalidOperationException($"Code '{center.Code}' already exists.");

            var id = await _unitOfWork.CostCenter.CreateAsync(center, cancellationToken);
            await _unitOfWork.SaveAsync();
            return id;
        }
        public async Task<IEnumerable<Guid>> AddCostCentersRangeAsync(IEnumerable<CostCenter> centers, CancellationToken cancellationToken = default)
        {
            if (centers == null) throw new ArgumentNullException(nameof(centers));
            // optional: validate duplicates...
            var ids = await _unitOfWork.CostCenter.AddRangeAsync(centers, cancellationToken);
            await _unitOfWork.SaveAsync();
            return ids;
        }
        public async Task<int> BulkImportCostCentersAsync(IEnumerable<CostCenter> centers, CancellationToken cancellationToken = default)
        {
            var ids = await AddCostCentersRangeAsync(centers, cancellationToken);
            return ids.Count();
        }
        public async Task<bool> UpdateCostCenterAsync(CostCenter center, CancellationToken cancellationToken = default)
        {
            if (center == null) throw new ArgumentNullException(nameof(center));
            if (!await _validation.CostCenterExistsByIdAsync(center.ID, cancellationToken))
                return false;
            var success = await _unitOfWork.CostCenter.UpdateAsync(center, cancellationToken);
            await _unitOfWork.SaveAsync();
            return success;
        }
        public async Task<bool> DeleteCostCenterAsync(Guid centerId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.CostCenterExistsByIdAsync(centerId, cancellationToken))
                return false;
            var success = await _unitOfWork.CostCenter.DeleteAsync(centerId, cancellationToken);
            await _unitOfWork.SaveAsync();
            return success;
        }
        public async Task<int> DeleteCostCentersRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default)
        {
            if (centerIds == null) throw new ArgumentNullException(nameof(centerIds));
            var result = await _unitOfWork.CostCenter.DeleteRangeAsync(centerIds, cancellationToken);
            await _unitOfWork.SaveAsync();
            return result ? centerIds.Count() : 0;
        }
        public async Task<int> BulkDeleteCostCentersAsync(IEnumerable<Guid> centerIds,   CancellationToken cancellationToken = default)
        {
            return await DeleteCostCentersRangeAsync(centerIds, cancellationToken);
        }
        public async Task<bool> SoftDeleteCostCenterAsync(Guid centerId, CancellationToken cancellationToken = default)
            => await DeleteCostCenterAsync(centerId, cancellationToken);
        public Task<int> SoftDeleteCostCentersRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default)
            => DeleteCostCentersRangeAsync(centerIds, cancellationToken);
        public async Task<bool> DeleteAllCostCentersAsync(CancellationToken cancellationToken = default)
        {
            var success = await _unitOfWork.CostCenter.DeleteAllAsync(cancellationToken);
            await _unitOfWork.SaveAsync();
            return success;
        }
        public async Task<bool> RestoreCostCenterAsync(Guid centerId, CancellationToken cancellationToken = default)
        {
            var success = await _unitOfWork.CostCenter.RestoreAsync(centerId, cancellationToken);
            await _unitOfWork.SaveAsync();
            return success;
        }
        public async Task<int> RestoreCostCentersRangeAsync(IEnumerable<Guid> centerIds, CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.CostCenter.RestoreRangeAsync(centerIds, cancellationToken);
            await _unitOfWork.SaveAsync();
            return result ? centerIds.Count() : 0;
        }
        public async Task<bool> RestoreAllCostCentersAsync(CancellationToken cancellationToken = default)
        {
            var success = await _unitOfWork.CostCenter.RestoreAllAsync(cancellationToken);
            await _unitOfWork.SaveAsync();
            return success;
        }
        public async Task<bool> SoftDeleteAllCostCentersAsync(CancellationToken cancellationToken = default)
        {
            // حذف منطقي لكل المراكز
            var success = await _unitOfWork.CostCenter.DeleteAllAsync(cancellationToken);
            await _unitOfWork.SaveAsync();
            return success;
        }
    }
}
