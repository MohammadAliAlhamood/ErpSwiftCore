using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanyService
{
    /// <summary>
    /// تنفيذ استعلامات إدارة الشركات - يشمل جميع الدوال التي تُرجع بيانات دون تعديل،
    /// مثل الحصول على شركة أو مجموعة شركات، البحث، والترقيم (Paging).
    /// يعتمد على IUnitOfWork فقط.
    /// </summary>
    public class CompanyQueryService : ICompanyQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // -------------------- [Single Company Retrieval] --------------------

        public Task<Company?> GetCompanyByIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAsync(c => c.ID == companyId, cancellationToken);
         
        public Task<Company?> GetCompanyByNameAsync(string companyName, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAsync(c => c.CompanyName == companyName, cancellationToken);

        // -------------------- [Bulk Retrieval] --------------------

        public Task<IReadOnlyList<Company>> GetAllCompaniesAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAllAsync(cancellationToken);
         
        public Task<IReadOnlyList<Company>> GetSoftDeletedCompaniesAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAllSoftDeletedAsync(cancellationToken: cancellationToken);

        public Task<IReadOnlyList<Company>> GetCompaniesByCountryAsync(string country, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAllAsync(c => c.Address != null && c.Address.Country == country, cancellationToken);

        public Task<IReadOnlyList<Company>> GetCompaniesByIndustryAsync(string industry, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAllAsync(c => c.IndustryType.ToString() == industry, cancellationToken);

      
        // -------------------- [Paging & Filtering] --------------------

        public async Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetCompaniesPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var total = await _unitOfWork.Company.CountAsync(null, cancellationToken);
            var items = await _unitOfWork.Company.GetPagedAsync(pageIndex, pageSize, null, c => c.CompanyName, true, cancellationToken);
            return (items, total);
        }

        public async Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetCompaniesPagedByCountryAsync(string country, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<Company, bool>> filter = c => c.Address != null && c.Address.Country == country;
            var total = await _unitOfWork.Company.CountAsync(filter, cancellationToken);
            var items = await _unitOfWork.Company.GetPagedAsync(pageIndex, pageSize, filter, c => c.CompanyName, true, cancellationToken);
            return (items.ToList().AsReadOnly(), total);
        }

        public async Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetCompaniesPagedByIndustryAsync(string industry, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<Company, bool>> filter = c => c.IndustryType.ToString() == industry;
            var total = await _unitOfWork.Company.CountAsync(filter, cancellationToken);
            var items = await _unitOfWork.Company.GetPagedAsync(pageIndex, pageSize, filter, c => c.CompanyName, true, cancellationToken);
            return (items.ToList().AsReadOnly(), total);
        }
         

        // -------------------- [Search] --------------------

        public async Task<IReadOnlyList<Company>> SearchCompaniesByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Array.Empty<Company>();

            var items = await _unitOfWork.Company.GetAllAsync(
                c => c.CompanyName != null && c.CompanyName.Contains(name),
                cancellationToken);

            return items.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Company>> SearchCompaniesByTaxIdAsync(string taxId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(taxId))
                return Array.Empty<Company>();

            var items = await _unitOfWork.Company.GetAllAsync(
                c => c.TaxID != null && c.TaxID.Contains(taxId),
                cancellationToken);

            return items.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Company>> SearchCompaniesByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Array.Empty<Company>();

            var lowered = keyword.ToLower();
            var items = await _unitOfWork.Company.GetAllAsync(
                c => (c.CompanyName != null && c.CompanyName.ToLower().Contains(lowered)) ||
                     (c.ContactInfo.Email != null && c.ContactInfo.Email.ToLower().Contains(lowered)) ||
                     (c.TaxID != null && c.TaxID.ToLower().Contains(lowered)),
                cancellationToken);

            return items.ToList().AsReadOnly();
        }

        // -------------------- [Company-Branch Queries] --------------------

        public Task<Company?> GetCompanyWithBranchesAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAsync(c => c.ID == companyId, cancellationToken);

        public Task<IReadOnlyList<CompanyBranch>> GetBranchesByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetByCompanyIdAsync(companyId, cancellationToken);

        // -------------------- [Company-Settings Queries] --------------------

        public Task<Company?> GetCompanyWithSettingsAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.GetAsync(c => c.ID == companyId, cancellationToken);

        public Task<CompanySettings?> GetSettingsByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken);

        // -------------------- [Counts & Stats] --------------------

        public Task<int> GetCompaniesCountAsync(CancellationToken cancellationToken = default)
            => _unitOfWork.Company.CountAsync(null, cancellationToken);

        public Task<int> GetBranchesCountAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.GetCountAsync(companyId, cancellationToken);
    }
}