using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService; 
namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanyBranchService
{
    /// <summary>
    /// تنفيذ عمليات التحقق (Validation) المتعلقة بإدارة فروع الشركات - تشمل دوال التحقق من الوجود،
    /// التفرد، والفحصات المرتبطة بالتبعيات (مثل التحقق من ارتباط الفرع بالشركة).
    /// يعتمد على IUnitOfWork.
    /// </summary>
    public class CompanyBranchValidationService : ICompanyBranchValidationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyBranchValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<bool> BranchExistsAsync(Guid branchId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.ExistsAsync(branchId, cancellationToken);
        public Task<bool> BranchExistsByCodeAsync(Guid companyId, string branchCode, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.ExistsByCodeAsync(companyId, branchCode, cancellationToken);
        public Task<bool> IsBranchNameUniqueAsync(Guid companyId, string branchName, Guid? excludeBranchId = null, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.IsNameUniqueAsync(companyId, branchName, excludeBranchId, cancellationToken);
        public Task<bool> HasBranchesAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.HasBranchesAsync(companyId, cancellationToken);
        public Task<bool> IsBranchLinkedToCompanyAsync(Guid branchId, Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.IsLinkedToCompanyAsync(branchId, companyId, cancellationToken);
    }
}