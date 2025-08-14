using ErpSwiftCore.Domain.ICore;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.IRepositories.ICompanyRepositories
{
    /// <summary>
    /// مستودع فروع الشركات - يدعم جميع السيناريوهات الاحترافية (CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, إلخ)
    /// </summary>
    public interface ICompanyBranchRepository : IRepository<CompanyBranch>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> AddAsync(CompanyBranch branch, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<CompanyBranch> branches, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(CompanyBranch branch, CancellationToken cancellationToken = default);

        // ----------- [Delete/Archive/Restore] -----------
        Task<bool> SoftDeleteAsync(Guid branchId, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteAllByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid branchId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        // ----------- [State Management] -----------


        // ----------- [Get/Query - Single] -----------
        Task<CompanyBranch?> GetByIdAsync(Guid branchId, CancellationToken cancellationToken = default);

        Task<CompanyBranch?> GetWithCompanyAsync(Guid branchId, CancellationToken cancellationToken = default);

        // ----------- [Get/Query - Bulk/Advanced] -----------
        Task<IReadOnlyList<CompanyBranch>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CompanyBranch>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);


        Task<IReadOnlyList<CompanyBranch>> GetSoftDeletedByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        // ----------- [Paging/Filtering/Searching] -----------
        Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetPagedAsync(Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

 
        Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetSoftDeletedPagedAsync(Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        // ----------- [Search] -----------
        Task<IReadOnlyList<CompanyBranch>> SearchByNameAsync(Guid companyId, string name, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CompanyBranch>> SearchByCodeAsync(Guid companyId, string code, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CompanyBranch>> SearchByKeywordAsync(Guid companyId, string keyword, CancellationToken cancellationToken = default);

        // ----------- [Existence & Validation] -----------
        Task<bool> ExistsAsync(Guid branchId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCodeAsync(Guid companyId, string branchCode, CancellationToken cancellationToken = default);

        Task<bool> IsNameUniqueAsync(Guid companyId, string branchName, Guid? excludeBranchId = null, CancellationToken cancellationToken = default);

        // ----------- [Integrity/Dependency] -----------
        Task<bool> HasBranchesAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<bool> IsLinkedToCompanyAsync(Guid branchId, Guid companyId, CancellationToken cancellationToken = default);

        // ----------- [Counts & Stats] -----------
        Task<int> GetCountAsync(Guid companyId, CancellationToken cancellationToken = default);

    }
}