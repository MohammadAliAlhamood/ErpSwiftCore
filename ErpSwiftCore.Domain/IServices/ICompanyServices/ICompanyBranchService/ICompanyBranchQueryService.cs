using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService
{
    /// <summary>
    /// واجهة استعلامات إدارة فروع الشركات - تشمل جميع الدوال التي تُرجع بيانات دون تعديل،
    /// مثل الحصول على فرع واحد أو قائمة فروع، دعم الترقيم والفرز، والبحث.
    /// </summary>
    public interface ICompanyBranchQueryService
    {
        // -------------------- [Get/Query Operations - Single] --------------------
        /// <summary>
        /// الحصول على فرع بواسطة معرفه.
        /// </summary>
        Task<CompanyBranch?> GetBranchByIdAsync(Guid branchId, CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على فرع مع بيانات الشركة المرتبطة به.
        /// </summary>
        Task<CompanyBranch?> GetBranchWithCompanyAsync(Guid branchId, CancellationToken cancellationToken = default);

        // -------------------- [Get/Query Operations - Bulk/Advanced] --------------------
        /// <summary>
        /// الحصول على جميع الفروع.
        /// </summary>
        Task<IReadOnlyList<CompanyBranch>> GetAllBranchesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// الحصول على جميع الفروع التابعة لشركة معينة.
        /// </summary>
        Task<IReadOnlyList<CompanyBranch>> GetBranchesByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

           /// <summary>
        /// الحصول على جميع الفروع المؤرشفة التابعة لشركة معينة.
        /// </summary>
        Task<IReadOnlyList<CompanyBranch>> GetSoftDeletedBranchesByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        // -------------------- [Paging/Filtering] --------------------
        /// <summary>
        /// الحصول على قائمة فروع بترقيم الصفحات (Paging) لشركة معينة.
        /// </summary>
        Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetBranchesPagedAsync(
            Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
         
        /// <summary>
        /// الحصول على قائمة الفروع المؤرشفة بترقيم الصفحات لشركة معينة.
        /// </summary>
        Task<(IReadOnlyList<CompanyBranch> Branches, int TotalCount)> GetSoftDeletedBranchesPagedAsync(
            Guid companyId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        // -------------------- [Search Operations] --------------------
        /// <summary>
        /// البحث عن فروع حسب الاسم ضمن شركة معينة.
        /// </summary>
        Task<IReadOnlyList<CompanyBranch>> SearchBranchesByNameAsync(Guid companyId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// البحث عن فروع حسب الكود ضمن شركة معينة.
        /// </summary>
        Task<IReadOnlyList<CompanyBranch>> SearchBranchesByCodeAsync(Guid companyId, string code, CancellationToken cancellationToken = default);

        /// <summary>
        /// البحث عن فروع باستخدام كلمة مفتاحية (Keyword) ضمن شركة معينة.
        /// </summary>
        Task<IReadOnlyList<CompanyBranch>> SearchBranchesByKeywordAsync(Guid companyId, string keyword, CancellationToken cancellationToken = default);

        // -------------------- [Counts & Stats] --------------------
        /// <summary>
        /// الحصول على عدد الفروع الكلي التابعة لشركة معينة.
        /// </summary>
        Task<int> GetBranchesCountAsync(Guid companyId, CancellationToken cancellationToken = default);
         
    }
}