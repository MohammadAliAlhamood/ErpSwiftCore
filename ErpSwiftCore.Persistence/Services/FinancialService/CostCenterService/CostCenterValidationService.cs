using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService; 
namespace ErpSwiftCore.Persistence.Services.FinancialService.CostCenterService
{
    public class CostCenterValidationService : ICostCenterValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public CostCenterValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        public Task<bool> CostCenterExistsByIdAsync(Guid centerId, CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.ExistsByIdAsync(centerId, cancellationToken);
        public Task<bool> CostCenterExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.ExistsByCodeAsync(code, cancellationToken);
        public Task<bool> CostCenterExistsByNameAsync(string name, CancellationToken cancellationToken = default)
            => _unitOfWork.CostCenter.ExistsByNameAsync(name, cancellationToken);

       
    }
}
