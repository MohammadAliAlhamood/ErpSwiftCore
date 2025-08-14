using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService
{
    /// <summary>
    /// واجهة التحقق (Validation) لإدارة الشركات - تشمل دوال التحقق من الوجود، التفرد،
    /// والفحصات المرتبطة بالتبعيات (كالفروع والإعدادات).
    /// </summary>
    public interface ICompanyValidationService
    {
        // -------------------- [Existence Checks] --------------------
        /// <summary>
        /// التحقق مما إذا كانت شركة موجودة حسب المُعرّف.
        /// </summary>
        Task<bool> CompanyExistsByIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// التحقق مما إذا كانت شركة موجودة حسب كود الشركة.
        /// </summary>
        Task<bool> CompanyExistsByCodeAsync(string companyCode, CancellationToken cancellationToken = default);

        // -------------------- [Uniqueness Validations] --------------------
        /// <summary>
        /// التحقق من تفرد اسم الشركة (قد يستثني شركة معينة من الفحص).
        /// </summary>
        Task<bool> IsCompanyNameUniqueAsync(string companyName, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// التحقق من تفرد البريد الإلكتروني للشركة (قد يستثني شركة معينة من الفحص).
        /// </summary>
        Task<bool> IsCompanyEmailUniqueAsync(string email, Guid? excludeCompanyId = null, CancellationToken cancellationToken = default);

        // -------------------- [Dependency/Integrity Checks] --------------------
        /// <summary>
        /// التحقق مما إذا كانت هناك فروع مرتبطة بالشركة قبل الحذف أو الإجراء الآخر.
        /// </summary>
        Task<bool> HasBranchesAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// التحقق مما إذا كانت هناك إعدادات مرتبطة بالشركة قبل الحذف أو الإجراء الآخر.
        /// </summary>
        Task<bool> HasSettingsAsync(Guid companyId, CancellationToken cancellationToken = default);
    }
}