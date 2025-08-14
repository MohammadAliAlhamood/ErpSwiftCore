using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService;
using ErpSwiftCore.SharedKernel.Entities; 
namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanyBranchService
{
    /// <summary>
    /// تنفيذ استعلامات إدارة فروع الشركات - تشمل جميع الدوال التي تُرجع بيانات دون تعديل،
    /// مثل الحصول على فرع واحد أو قائمة فروع، دعم الترقيم، الفرز، والبحث.
    /// يعتمد على IUnitOfWork فقط.
    /// </summary>
    public class CompanyBranchQueryService : ICompanyBranchQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyBranchQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<CompanyBranch?> GetBranchByIdAsync(Guid branchId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetByIdAsync(branchId, cancellationToken);
        public Task<CompanyBranch?> GetBranchWithCompanyAsync(Guid branchId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetWithCompanyAsync(branchId, cancellationToken);
        public Task<IReadOnlyList<CompanyBranch>> GetAllBranchesAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetAllAsync(cancellationToken);





        public Task<IReadOnlyList<CompanyBranch>> GetBranchesByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetByCompanyIdAsync(companyId, cancellationToken);
         public Task<IReadOnlyList<CompanyBranch>> GetSoftDeletedBranchesByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetSoftDeletedByCompanyIdAsync(companyId, cancellationToken);
        public Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetBranchesPagedAsync(
            Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetPagedAsync(companyId, pageIndex, pageSize, cancellationToken); 
        public Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetSoftDeletedBranchesPagedAsync(
            Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetSoftDeletedPagedAsync(companyId, pageIndex, pageSize, cancellationToken);
        public Task<IReadOnlyList<CompanyBranch>> SearchBranchesByNameAsync(Guid companyId, string name, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.SearchByNameAsync(companyId, name, cancellationToken);
        public Task<IReadOnlyList<CompanyBranch>> SearchBranchesByCodeAsync(Guid companyId, string code, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.SearchByCodeAsync(companyId, code, cancellationToken);
        public Task<IReadOnlyList<CompanyBranch>> SearchBranchesByKeywordAsync(Guid companyId, string keyword, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.SearchByKeywordAsync(companyId, keyword, cancellationToken);
        public Task<int> GetBranchesCountAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetCountAsync(companyId, cancellationToken); 
    }
}