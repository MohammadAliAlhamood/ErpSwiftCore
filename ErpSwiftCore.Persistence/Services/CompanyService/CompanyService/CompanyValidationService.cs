using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanyService
{
    /// <summary>
    /// تنفيذ عمليات التحقق (Validation) المتعلقة بإدارة الشركات - تشمل دوال التحقق من الوجود،
    /// التفرد، والفحصات المرتبطة بالتبعيات (مثل الفروع والإعدادات). يعتمد على IUnitOfWork.
    /// </summary>
    public class CompanyValidationService : ICompanyValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // -------------------- [Existence Checks] --------------------

        public Task<bool> CompanyExistsByIdAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.ExistsAsync(companyId, cancellationToken);

        public Task<bool> CompanyExistsByCodeAsync(string companyCode, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.ExistsByCodeAsync(companyCode, cancellationToken);

        // -------------------- [Uniqueness Validations] --------------------

        public Task<bool> IsCompanyNameUniqueAsync(string companyName, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.IsNameUniqueAsync(companyName, excludeCompanyId, cancellationToken);

        public Task<bool> IsCompanyEmailUniqueAsync(string email, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default)
            => _unitOfWork.Company.IsEmailUniqueAsync(email, excludeCompanyId, cancellationToken);

        // -------------------- [Dependency/Integrity Checks] --------------------

        public Task<bool> HasBranchesAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanyBranch.HasBranchesAsync(companyId, cancellationToken);

        public Task<bool> HasSettingsAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.ExistsForCompanyAsync(companyId, cancellationToken);
    }
}