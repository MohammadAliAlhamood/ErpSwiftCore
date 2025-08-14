using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService
{

    public interface ICompanyCommandService
    {

        Task<Guid> CreateCompanyAsync(Company company, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddCompaniesRangeAsync(IEnumerable<Company> companies, CancellationToken cancellationToken = default);
        Task<bool> UpdateCompanyAsync(Company company, CancellationToken cancellationToken = default);
        
        Task<bool> DeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
        Task<bool> DeleteCompaniesRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllCompaniesAsync(CancellationToken cancellationToken = default);


        Task<bool> SoftDeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteCompaniesRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllCompaniesAsync(CancellationToken cancellationToken = default);



        Task<bool> RestoreCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
        Task<bool> RestoreCompaniesRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllCompaniesAsync(CancellationToken cancellationToken = default); 
        Task<Guid> AddBranchToCompanyAsync(Guid companyId, CompanyBranch branch, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddBranchesToCompanyAsync(Guid companyId, IEnumerable<CompanyBranch> branches, CancellationToken cancellationToken = default);
        Task<bool> UpdateBranchOfCompanyAsync(Guid companyId, CompanyBranch branch, CancellationToken cancellationToken = default);
        Task<bool> DeleteBranchFromCompanyAsync(Guid companyId, Guid branchId, CancellationToken cancellationToken = default);
        Task<bool> DeleteBranchesFromCompanyAsync(Guid companyId, IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default); 
        Task<CompanySettings> UpdateCompanySettingsAsync(Guid companyId, CompanySettings settings, CancellationToken cancellationToken = default);
    }
}