using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService
{
    /// <summary>
    /// واجهة استعلامات إدارة الشركات - تشمل جميع الدوال التي تُرجع بيانات دون تعديل،
    /// مثل الحصول على شركة أو مجموعة شركات، البحث، وفرز/ترقيم النتائج.
    /// </summary>
    public interface ICompanyQueryService
    {
        // -------------------- [Single Company Retrieval] --------------------
        /// <summary>
        /// الحصول على شركة بواسطة المعرف.
        /// </summary>
        Task<Company?> GetCompanyByIdAsync(Guid companyId, CancellationToken cancellationToken = default);

    
        /// <summary>
        /// الحصول على شركة بواسطة اسم الشركة.
        /// </summary>
        Task<Company?> GetCompanyByNameAsync(string companyName, CancellationToken cancellationToken = default);

        // -------------------- [Bulk Retrieval] --------------------
        /// <summary>
        /// الحصول على جميع الشركات.
        /// </summary>
        Task<IReadOnlyList<Company>> GetAllCompaniesAsync(CancellationToken cancellationToken = default);
      /// <summary>
        /// الحصول على جميع الشركات المؤرشفة.
        /// </summary>
        Task<IReadOnlyList<Company>> GetSoftDeletedCompaniesAsync(CancellationToken cancellationToken = default);       

        /// <summary>
        /// الحصول على الشركات حسب الدولة.
        /// </summary>
        Task<IReadOnlyList<Company>> GetCompaniesByCountryAsync(string country, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على الشركات حسب الصناعة.
        /// </summary>
        Task<IReadOnlyList<Company>> GetCompaniesByIndustryAsync(string industry, CancellationToken cancellationToken = default);

     
        // -------------------- [Paging & Filtering] --------------------
        /// <summary>
        /// الحصول على مجموعة شركات بترقيم الصفحات (Paging).
        /// </summary>
        Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetCompaniesPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على مجموعة شركات بترقيم الصفحات حسب الدولة.
        /// </summary>
        Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetCompaniesPagedByCountryAsync(string country, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على مجموعة شركات بترقيم الصفحات حسب الصناعة.
        /// </summary>
        Task<(IReadOnlyList<Company> Companies, int TotalCount)> GetCompaniesPagedByIndustryAsync(string industry, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

       
        // -------------------- [Search] --------------------
        /// <summary>
        /// البحث عن شركات حسب الاسم.
        /// </summary>
        Task<IReadOnlyList<Company>> SearchCompaniesByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// البحث عن شركات حسب الرقم الضريبي.
        /// </summary>
        Task<IReadOnlyList<Company>> SearchCompaniesByTaxIdAsync(string taxId, CancellationToken cancellationToken = default);

        /// <summary>
        /// البحث عن شركات باستخدام كلمة مفتاحية (Keyword).
        /// </summary>
        Task<IReadOnlyList<Company>> SearchCompaniesByKeywordAsync(string keyword, CancellationToken cancellationToken = default);

        // -------------------- [Company-Branch Queries] --------------------
        /// <summary>
        /// الحصول على شركة مع الفروع المرتبطة بها.
        /// </summary>
        Task<Company?> GetCompanyWithBranchesAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على جميع الفروع التابعة لشركة معينة.
        /// </summary>
        Task<IReadOnlyList<CompanyBranch>> GetBranchesByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        // -------------------- [Company-Settings Queries] --------------------
        /// <summary>
        /// الحصول على شركة مع إعداداتها.
        /// </summary>
        Task<Company?> GetCompanyWithSettingsAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على إعدادات الشركة فقط بواسطة المعرف.
        /// </summary>
        Task<CompanySettings?> GetSettingsByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        // -------------------- [Counts & Stats] --------------------
        /// <summary>
        /// الحصول على إجمالي عدد الشركات (بما في ذلك النشطة والمؤرشفة).
        /// </summary>
        Task<int> GetCompaniesCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على عدد الفروع التابعة لشركة معينة.
        /// </summary>
        Task<int> GetBranchesCountAsync(Guid companyId, CancellationToken cancellationToken = default);
    }
}