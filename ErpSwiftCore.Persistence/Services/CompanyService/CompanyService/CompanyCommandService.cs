using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using ErpSwiftCore.SharedKernel.Entities;
namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanyService
{
    /// <summary>
    /// تنفيذ أوامر إدارة الشركات - يشمل جميع العمليات التي تُحدث تغييرات (إنشاء، تعديل، حذف/أرشفة، استعادة،
    /// إدارة الفروع، إدارة الإعدادات، وتغيير الحالة). يعتمد على IUnitOfWork و ICompanyValidationService.
    /// </summary>
    public class CompanyCommandService : ICompanyCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyValidationService _validationService;
        public CompanyCommandService(IUnitOfWork unitOfWork, ICompanyValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }
        // -------------------- [Create/Update] --------------------
        public async Task<Guid> CreateCompanyAsync(Company company, CancellationToken cancellationToken = default)
        {
            // تحقق من تفرد الاسم، البريد، والكود قبل الإنشاء
            if (!await _validationService.IsCompanyNameUniqueAsync(company.CompanyName, null, cancellationToken))
                throw new InvalidOperationException("اسم الشركة مستخدم بالفعل.");

            if (!await _validationService.IsCompanyEmailUniqueAsync(company.ContactInfo.Email, null, cancellationToken))
                throw new InvalidOperationException("البريد الإلكتروني مستخدم بالفعل.");


            return await _unitOfWork.Company.AddAsync(company, cancellationToken);
        }
        public async Task<IEnumerable<Guid>> AddCompaniesRangeAsync(IEnumerable<Company> companies, CancellationToken cancellationToken = default)
        {
            var validCompanies = new List<Company>();

            foreach (var company in companies)
            {
                if (!await _validationService.IsCompanyNameUniqueAsync(company.CompanyName, null, cancellationToken))
                    throw new InvalidOperationException($"اسم الشركة '{company.CompanyName}' مستخدم بالفعل.");

                if (!await _validationService.IsCompanyEmailUniqueAsync(company.ContactInfo.Email, null, cancellationToken))
                    throw new InvalidOperationException($"البريد الإلكتروني '{company.ContactInfo.Email}' مستخدم بالفعل.");


                validCompanies.Add(company);
            }

            return await _unitOfWork.Company.AddRangeAsync(validCompanies, cancellationToken);
        }
        public async Task<bool> UpdateCompanyAsync(Company company, CancellationToken cancellationToken = default)
        {
            // تحقق من تفرد الاسم والبريد مع استثناء الكيان الحالي
            if (!await _validationService.IsCompanyNameUniqueAsync(company.CompanyName, company.ID, cancellationToken))
                throw new InvalidOperationException("اسم الشركة مستخدم بالفعل.");

            if (!await _validationService.IsCompanyEmailUniqueAsync(company.ContactInfo.Email, company.ID, cancellationToken))
                throw new InvalidOperationException("البريد الإلكتروني مستخدم بالفعل.");

            await _unitOfWork.Company.UpdateAsync(company, true, cancellationToken);
            return true;
        }
        public async Task<bool> SoftDeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            //if (await _unitOfWork.Company.ExistsAsync(c => c.ID == companyId, cancellationToken)) return false;
            return await _unitOfWork.Company.SoftDeleteAsync(companyId, cancellationToken);
        }
        public async Task<bool> SoftDeleteCompaniesRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        {

            if (companyIds.Count() <= 0)
            {
                return false;
            }
            return await _unitOfWork.Company.SoftDeleteRangeAsync(companyIds, cancellationToken);
        }
        public async Task<bool> SoftDeleteAllCompaniesAsync(CancellationToken cancellationToken = default)
        {
            if (await _unitOfWork.Company.CountAsync() > 0)
            {
                return await _unitOfWork.Company.SoftDeleteAllAsync(cancellationToken);
            }
            return false;
        }
        public async Task<bool> DeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            //if (await _unitOfWork.Company.ExistsAsync(c => c.ID == companyId, cancellationToken)) return false;
            return await _unitOfWork.Company.DeleteAsync(companyId, cancellationToken);
        }
        public async Task<bool> DeleteCompaniesRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        {
            var result = true;
            foreach (var id in companyIds)
                result &= await DeleteCompanyAsync(id, cancellationToken);
            return result;
        }
        public async Task<bool> DeleteAllCompaniesAsync(CancellationToken cancellationToken = default)
        {
            if (await _unitOfWork.Company.CountAsync() > 0)
            {
                return await _unitOfWork.Company.DeleteAllAsync(cancellationToken);
            }
            return false;
        }
        public async Task<bool> RestoreCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var company = await _unitOfWork.Company.GetOneSoftDeletedAsync(c => c.ID == companyId, cancellationToken);
            if (company is null)
                return false;

            await _unitOfWork.Company.RestoreAsync(company, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreCompaniesRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default)
        {
            var result = true;
            foreach (var id in companyIds)
                result &= await RestoreCompanyAsync(id, cancellationToken);
            return result;
        }
        public async Task<bool> RestoreAllCompaniesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Company.RestoreAllAsync(cancellationToken);
        }
       
        // -------------------- [Company-Branch Commands] --------------------
        public async Task<Guid> AddBranchToCompanyAsync(Guid companyId, CompanyBranch branch, CancellationToken cancellationToken = default)
        {
            // تحقق من وجود الشركة
            if (!await _validationService.CompanyExistsByIdAsync(companyId, cancellationToken))
                throw new InvalidOperationException("الشركة غير موجودة.");

            // تحقق من تفرد اسم الفرع داخل نفس الشركة
            if (!await _unitOfWork.CompanyBranch.IsNameUniqueAsync(companyId, branch.BranchName, null, cancellationToken))
                throw new InvalidOperationException("اسم الفرع مستخدم بالفعل.");

            branch.CompanyID = companyId;
            return await _unitOfWork.CompanyBranch.AddAsync(branch, cancellationToken);
        }
        public async Task<IEnumerable<Guid>> AddBranchesToCompanyAsync(Guid companyId, IEnumerable<CompanyBranch> branches, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var branch in branches)
            {
                var newId = await AddBranchToCompanyAsync(companyId, branch, cancellationToken);
                ids.Add(newId);
            }
            return ids;
        }
        public async Task<bool> UpdateBranchOfCompanyAsync(Guid companyId, CompanyBranch branch, CancellationToken cancellationToken = default)
        {
            // تحقق من وجود الشركة
            if (!await _validationService.CompanyExistsByIdAsync(companyId, cancellationToken))
                throw new InvalidOperationException("الشركة غير موجودة.");
            // تحقق من تفرد اسم الفرع مع استثناء الكيان الحالي
            if (!await _unitOfWork.CompanyBranch.IsNameUniqueAsync(companyId, branch.BranchName, branch.ID, cancellationToken))
                throw new InvalidOperationException("اسم الفرع مستخدم بالفعل.");
            branch.CompanyID = companyId;
            return await _unitOfWork.CompanyBranch.UpdateAsync(branch, cancellationToken);
        }
        public async Task<bool> DeleteBranchFromCompanyAsync(Guid companyId, Guid branchId, CancellationToken cancellationToken = default)
        {
            var branch = await _unitOfWork.CompanyBranch.GetByIdAsync(branchId, cancellationToken);
            if (branch == null || branch.CompanyID != companyId)
                return false;
            return await _unitOfWork.CompanyBranch.SoftDeleteAsync(branchId, cancellationToken);
        }
        public async Task<bool> DeleteBranchesFromCompanyAsync(Guid companyId, IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            var result = true;
            foreach (var id in branchIds)
                result &= await DeleteBranchFromCompanyAsync(companyId, id, cancellationToken);
            return result;
        }
        public async Task<CompanySettings> UpdateCompanySettingsAsync(Guid companyId, CompanySettings settings, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.CompanySettings.GetByCompanyIdAsync(companyId, cancellationToken);
            if (existing is null)
            {
                settings.CompanyID = companyId;
                await _unitOfWork.CompanySettings.AddAsync(settings, cancellationToken);
            }
            else
            {
                settings.ID = existing.ID;
                await _unitOfWork.CompanySettings.UpdateAsync(settings, cancellationToken);
            }
            return settings;
        }
    }
}