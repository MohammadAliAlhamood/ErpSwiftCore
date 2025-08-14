using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompaniesSettings;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;



namespace ErpSwiftCore.Web.IService.ICompaniesService
{
    public interface ICompanySettingService
    {
        #region ──────────── Command Methods ────────────

        /// <summary>
        /// إنشاء إعدادات شركة جديدة
        /// </summary>
        Task<APIResponseDto?> CreateCompanySettingsAsync(CompanySettingsCreateDto dto);

        /// <summary>
        /// إنشاء مجموعة إعدادات شركات دفعة واحدة
        /// </summary>
        Task<APIResponseDto?> BulkCreateCompanySettingsAsync(CompanySettingsBulkCreateDto dto);

        /// <summary>
        /// تعديل إعدادات شركة بالكامل
        /// </summary>
        Task<APIResponseDto?> UpdateCompanySettingsAsync(CompanySettingsUpdateDto dto);

        /// <summary>
        /// تحديث العملة لإعدادات شركة
        /// </summary>
        Task<APIResponseDto?> UpdateCompanySettingsCurrencyAsync(CompanySettingsCurrencyUpdateDto dto);

        /// <summary>
        /// تحديث المنطقة الزمنية لإعدادات شركة
        /// </summary>
        Task<APIResponseDto?> UpdateCompanySettingsTimeZoneAsync(CompanySettingsTimeZoneUpdateDto dto);

        /// <summary>
        /// حذف إعدادات شركة واحدة
        /// </summary>
        Task<APIResponseDto?> DeleteCompanySettingsAsync(Guid companyId);

        /// <summary>
        /// حذف إعدادات شركات متعددة
        /// </summary>
        Task<APIResponseDto?> BulkDeleteCompanySettingsAsync(CompanySettingsBulkDeleteDto dto);

        /// <summary>
        /// حذف جميع إعدادات الشركات
        /// </summary>
        Task<APIResponseDto?> DeleteAllCompanySettingsAsync();

        /// <summary>
        /// استرجاع إعدادات شركة واحدة
        /// </summary>
        Task<APIResponseDto?> RestoreCompanySettingsAsync(Guid companyId);

        /// <summary>
        /// استرجاع إعدادات شركات متعددة
        /// </summary>
        Task<APIResponseDto?> BulkRestoreCompanySettingsAsync(CompanySettingsBulkRestoreDto dto);

        /// <summary>
        /// استرجاع جميع إعدادات الشركات
        /// </summary>
        Task<APIResponseDto?> RestoreAllCompanySettingsAsync();

        
        #endregion

        #region ──────────── Query Methods ────────────

        /// <summary>
        /// جلب جميع إعدادات الشركات
        /// </summary>
        Task<APIResponseDto?> GetAllCompanySettingsAsync();

        /// <summary>
        /// جلب إعدادات الشركات النشطة فقط
        /// </summary>
        Task<APIResponseDto?> GetActiveCompanySettingsAsync();

        /// <summary>
        /// جلب إعدادات الشركات المؤرشفة فقط
        /// </summary>
        Task<APIResponseDto?> GetSoftDeletedCompanySettingsAsync();

        /// <summary>
        /// جلب إعدادات شركة بواسطة المعرّف
        /// </summary>
        Task<APIResponseDto?> GetCompanySettingsByCompanyIdAsync(Guid companyId);

        /// <summary>
        /// جلب إعدادات الشركات حسب العملة
        /// </summary>
        Task<APIResponseDto?> GetCompanySettingsByCurrencyAsync(Guid currencyId);

        /// <summary>
        /// جلب إعدادات الشركات حسب المنطقة الزمنية
        /// </summary>
        Task<APIResponseDto?> GetCompanySettingsByTimeZoneAsync(string timeZone);

        /// <summary>
        /// جلب إعدادات الشركات بترقيم الصفحات
        /// </summary>
        Task<APIResponseDto?> GetCompanySettingsPagedAsync(int pageIndex, int pageSize);

        /// <summary>
        /// جلب إعدادات الشركات بترقيم الصفحات حسب العملة
        /// </summary>
        Task<APIResponseDto?> GetCompanySettingsPagedByCurrencyAsync(Guid currencyId, int pageIndex, int pageSize);

        /// <summary>
        /// جلب إعدادات الشركات بترقيم الصفحات حسب المنطقة الزمنية
        /// </summary>
        Task<APIResponseDto?> GetCompanySettingsPagedByTimeZoneAsync(string timeZone, int pageIndex, int pageSize);

        /// <summary>
        /// البحث عن إعدادات الشركات بالكلمة المفتاحية
        /// </summary>
        Task<APIResponseDto?> SearchCompanySettingsByKeywordAsync(string keyword);

        /// <summary>
        /// التحقق من وجود إعدادات شركة بواسطة المعرّف
        /// </summary>
        Task<APIResponseDto?> CompanySettingsExistAsync(Guid companyId);

        /// <summary>
        /// التحقق من تفرد إعدادات شركة بواسطة المعرّف
        /// </summary>
        Task<APIResponseDto?> IsCompanySettingsUniqueAsync(Guid companyId);

        /// <summary>
        /// جلب عدد جميع إعدادات الشركات
        /// </summary>
        Task<APIResponseDto?> GetCompanySettingsCountAsync();

        /// <summary>
        /// جلب عدد إعدادات الشركات النشطة
        /// </summary>
        Task<APIResponseDto?> GetActiveCompanySettingsCountAsync();

        #endregion
    }
}
