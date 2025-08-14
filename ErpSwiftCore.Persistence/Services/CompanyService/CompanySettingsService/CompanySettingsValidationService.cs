using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanySettingsService
{
    /// <summary>
    /// تنفيذ عمليات التحقق (Validation) المتعلقة بإدارة إعدادات الشركة - تشمل دوال التحقق من الوجود،
    /// التفرد، والفحصات المرتبطة بالتبعيات.
    /// يعتمد على IUnitOfWork.
    /// </summary>
    public class CompanySettingsValidationService : ICompanySettingsValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanySettingsValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // -------------------- [Existence & Validation] --------------------

        public Task<bool> SettingsExistAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.ExistsForCompanyAsync(companyId, cancellationToken);

        public Task<bool> IsCompanySettingsUniqueAsync(Guid companyId, CancellationToken cancellationToken = default)
            => _unitOfWork.CompanySettings.IsUniqueForCompanyAsync(companyId, cancellationToken);
    }
}