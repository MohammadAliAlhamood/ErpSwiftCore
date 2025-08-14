using ErpSwiftCore.Domain.ICore;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.IRepositories.ICompanyRepositories
{
    /// <summary>
    /// مستودع الشركات - يدعم جميع السيناريوهات الاحترافية (CRUD, Bulk, Paging, Search, State, Archive, Restore, Integrity, إلخ)
    /// </summary>
    public interface ICompanyRepository : IRepository<Company>
    {
        // ----------- [CRUD & Bulk] -----------
        Task<Guid> AddAsync(Company company, CancellationToken cancellationToken = default);

        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Company> companies, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(Company company, CancellationToken cancellationToken = default);

        // ----------- [Delete/Archive/Restore] -----------
        Task<bool> DeleteAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<bool> DeleteRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);

        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);



        Task<bool> SoftDeleteAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<bool> RestoreRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);

        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

     
        // ----------- [Get/Query - Single] -----------
        Task<Company?> GetByIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<Company?> GetByCodeAsync(string companyCode, CancellationToken cancellationToken = default);

        Task<Company?> GetByNameAsync(string companyName, CancellationToken cancellationToken = default);

        // ----------- [Get/Query - Bulk/Advanced] -----------
        Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken cancellationToken = default);


        Task<IReadOnlyList<Company>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Company>> GetByCountryAsync(string country, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Company>> GetByIndustryAsync(string industry, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Company>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);

        // ----------- [Paging/Filtering/Searching] -----------
        Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetPagedByCountryAsync(string country, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetPagedByIndustryAsync(string industry, int pageIndex, int pageSize, CancellationToken cancellationToken = default);


        // ----------- [Search] -----------
        Task<IReadOnlyList<Company>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Company>> SearchByTaxIdAsync(string taxId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Company>> SearchByKeywordAsync(string keyword, CancellationToken cancellationToken = default);

      
        // ----------- [Existence & Validation] -----------
        Task<bool> ExistsAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCodeAsync(string companyCode, CancellationToken cancellationToken = default);

        Task<bool> IsNameUniqueAsync(string companyName, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default);

        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default);

        // ----------- [Counts & Stats] -----------
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
         
   
    
    
    }
}