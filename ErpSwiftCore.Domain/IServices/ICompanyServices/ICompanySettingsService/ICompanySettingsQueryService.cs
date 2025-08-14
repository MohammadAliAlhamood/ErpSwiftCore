using ErpSwiftCore.Domain.EntityCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService
{
    /// <summary>
    /// واجهة استعلامات إدارة إعدادات الشركة - تشمل جميع الدوال التي تُرجع بيانات دون تعديل،
    /// مثل الحصول على إعدادات واحدة أو قائمة إعدادات، الترميز، الفرز، البحث، والعدّ.
    /// </summary>
    public interface ICompanySettingsQueryService
    {
        // -------------------- [Get/Query Operations - Single] --------------------

        /// <summary>
        /// الحصول على إعدادات شركة بواسطة معرف الشركة.
        /// </summary>
        Task<CompanySettings?> GetCompanySettingsByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------

        /// <summary>
        /// الحصول على جميع إعدادات الشركات.
        /// </summary>
        Task<IReadOnlyList<CompanySettings>> GetAllCompanySettingsAsync(CancellationToken cancellationToken = default);
 
        /// <summary>
        /// الحصول على جميع إعدادات الشركات المؤرشفة.
        /// </summary>
        Task<IReadOnlyList<CompanySettings>> GetSoftDeletedCompanySettingsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على إعدادات الشركات حسب معرف العملة.
        /// </summary>
        Task<IReadOnlyList<CompanySettings>> GetCompanySettingsByCurrencyAsync(Guid currencyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على إعدادات الشركات حسب المنطقة الزمنية.
        /// </summary>
        Task<IReadOnlyList<CompanySettings>> GetCompanySettingsByTimeZoneAsync(string timeZone, CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering/Searching - Specialized] --------------------

        /// <summary>
        /// الحصول على قائمة إعدادات شركات بترقيم الصفحات (Paging).
        /// </summary>
        Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetCompanySettingsPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على قائمة إعدادات شركات بترقيم الصفحات حسب العملة.
        /// </summary>
        Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetCompanySettingsPagedByCurrencyAsync(Guid currencyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على قائمة إعدادات شركات بترقيم الصفحات حسب المنطقة الزمنية.
        /// </summary>
        Task<(IReadOnlyList<CompanySettings> Settings, int TotalCount)> GetCompanySettingsPagedByTimeZoneAsync(string timeZone, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        // -------------------- [Search Operations] --------------------

        /// <summary>
        /// البحث ضمن إعدادات الشركات باستخدام كلمة مفتاحية (Keyword).
        /// </summary>
        Task<IReadOnlyList<CompanySettings>> SearchCompanySettingsByKeywordAsync(string keyword, CancellationToken cancellationToken = default);

        // -------------------- [Counts & Stats] --------------------

        /// <summary>
        /// الحصول على إجمالي عدد إعدادات الشركات (بما في ذلك النشطة والمؤرشفة).
        /// </summary>
        Task<int> GetCompanySettingsCountAsync(CancellationToken cancellationToken = default); 
    }
}