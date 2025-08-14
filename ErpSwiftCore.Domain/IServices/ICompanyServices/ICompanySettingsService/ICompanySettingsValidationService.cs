using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService
{
    /// <summary>
    /// واجهة التحقق (Validation) لإدارة إعدادات الشركة - تشمل دوال التحقق من الوجود،
    /// التفرد، والفحصات المرتبطة بالتبعيات.
    /// </summary>
    public interface ICompanySettingsValidationService
    {
        // -------------------- [Existence & Validation] --------------------

        /// <summary>
        /// التحقق مما إذا كانت إعدادات الشركة موجودة بواسطة معرف الشركة.
        /// </summary>
        Task<bool> SettingsExistAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// التحقق من تفرد إعدادات الشركة (إذا كانت الإعدادات غير موجودة مسبقًا).
        /// </summary>
        Task<bool> IsCompanySettingsUniqueAsync(Guid companyId, CancellationToken cancellationToken = default);
    }
}