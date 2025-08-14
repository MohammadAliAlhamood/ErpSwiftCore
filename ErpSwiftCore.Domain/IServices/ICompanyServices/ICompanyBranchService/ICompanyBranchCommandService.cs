using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService
{
    /// <summary>
    /// واجهة أوامر إدارة فروع الشركات - تشمل جميع العمليات التي تُحدث تغييرات
    /// (إنشاء، تعديل، حذف/أرشفة، استعادة، تغيير الحالة) ودعم الإضافة بالجملة.
    /// جميع الدوال تعيد قيمة مناسبة حسب أفضل الممارسات.
    /// </summary>
    public interface ICompanyBranchCommandService
    {
        // -------------------- [Create/Update] --------------------
        /// <summary>
        /// إنشاء فرع جديد.
        /// </summary>
        Task<Guid> CreateBranchAsync(CompanyBranch branch, CancellationToken cancellationToken = default);

        /// <summary>
        /// إضافة مجموعة فروع دفعة واحدة.
        /// </summary>
        Task<IEnumerable<Guid>> AddBranchesRangeAsync(IEnumerable<CompanyBranch> branches, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحديث بيانات فرع موجود.
        /// </summary>
        Task<bool> UpdateBranchAsync(CompanyBranch branch, CancellationToken cancellationToken = default);

        // -------------------- [Delete/Archive/Restore] --------------------
        /// <summary>
        /// حذف (أرشفة) فرع بواسطة معرفه.
        /// </summary>
        Task<bool> DeleteBranchAsync(Guid branchId, CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف (أرشفة) مجموعة فروع دفعة واحدة.
        /// </summary>
        Task<bool> DeleteBranchesRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف (أرشفة) جميع الفروع التابعة لشركة معينة.
        /// </summary>
        Task<bool> DeleteAllBranchesOfCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة فرع محذوف (مرشّف) بواسطة معرفه.
        /// </summary>
        Task<bool> RestoreBranchAsync(Guid branchId, CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة مجموعة فروع محذوفة (مرشّفة) دفعة واحدة.
        /// </summary>
        Task<bool> RestoreBranchesRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة جميع الفروع المحذوفة (المرشّفة) التابعة لشركة معينة.
        /// </summary>
        Task<bool> RestoreAllBranchesOfCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
 
    }
}