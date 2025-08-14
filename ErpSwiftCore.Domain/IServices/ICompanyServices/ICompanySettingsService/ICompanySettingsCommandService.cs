using ErpSwiftCore.Domain.EntityCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService
{
    /// <summary>
    /// واجهة أوامر إدارة إعدادات الشركة - تشمل جميع العمليات التي تُحدث تغييرات
    /// (إنشاء، تعديل، حذف/أرشفة، استعادة، تغيير الحالة، تحديث حقول محددة، ودعم الإضافة بالجملة).
    /// </summary>
    public interface ICompanySettingsCommandService
    {
        // -------------------- [Create/Update] --------------------

        /// <summary>
        /// إنشاء إعدادات لشركة جديدة.
        /// </summary>
        Task<Guid> CreateCompanySettingsAsync(CompanySettings settings, CancellationToken cancellationToken = default);

        /// <summary>
        /// إضافة مجموعة إعدادات شركات دفعة واحدة.
        /// </summary>
        Task<IEnumerable<Guid>> AddCompanySettingsRangeAsync(IEnumerable<CompanySettings> settingsList, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحديث إعدادات شركة موجودة.
        /// </summary>
        Task<bool> UpdateCompanySettingsAsync(CompanySettings settings, CancellationToken cancellationToken = default);

        // -------------------- [Delete/Archive/Restore] --------------------

        /// <summary>
        /// حذف (أرشفة) إعدادات شركة بواسطة معرف الشركة.
        /// </summary>
        Task<bool> DeleteCompanySettingsAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف (أرشفة) مجموعة إعدادات شركات دفعة واحدة.
        /// </summary>
        Task<bool> DeleteCompanySettingsRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف (أرشفة) إعدادات كافة الشركات.
        /// </summary>
        Task<bool> DeleteAllCompanySettingsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة إعدادات شركة محذوفة (مرشّفة).
        /// </summary>
        Task<bool> RestoreCompanySettingsAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة إعدادات مجموعة شركات محذوفة (مرشّفة) دفعة واحدة.
        /// </summary>
        Task<bool> RestoreCompanySettingsRangeAsync(IEnumerable<Guid> companyIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة إعدادات كافة الشركات المحذوفة (المرشّفة).
        /// </summary>
        Task<bool> RestoreAllCompanySettingsAsync(CancellationToken cancellationToken = default);

       
        // -------------------- [Update Specific Fields] --------------------

        /// <summary>
        /// تحديث العملة الخاصة بإعدادات شركة معينة.
        /// </summary>
        Task<bool> UpdateCompanyCurrencyAsync(Guid companyId, Guid currencyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحديث المنطقة الزمنية الخاصة بإعدادات شركة معينة.
        /// </summary>
        Task<bool> UpdateCompanyTimeZoneAsync(Guid companyId, string timeZone, CancellationToken cancellationToken = default);
    }
}