using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService
{
    /// <summary>
    /// واجهة التحقق (Validation) لإدارة فروع الشركات - تشمل دوال التحقق من الوجود،
    /// التفرد، والفحصات المرتبطة بالتبعيات (كالتأكد من ارتباط الفرع بالشركة).
    /// </summary>
    public interface ICompanyBranchValidationService
    {
        // -------------------- [Existence Checks] --------------------
        /// <summary>
        /// التحقق مما إذا كان فرع موجودًا حسب معرفه.
        /// </summary>
        Task<bool> BranchExistsAsync(Guid branchId, CancellationToken cancellationToken = default);

        /// <summary>
        /// التحقق مما إذا كان هناك فرع بنفس الكود ضمن شركة معينة.
        /// </summary>
        Task<bool> BranchExistsByCodeAsync(Guid companyId, string branchCode, CancellationToken cancellationToken = default);

        // -------------------- [Uniqueness Validation] --------------------
        /// <summary>
        /// التحقق من تفرد اسم الفرع ضمن شركة معينة (قد يستثني فرعًا من الفحص).
        /// </summary>
        Task<bool> IsBranchNameUniqueAsync(Guid companyId, string branchName, Guid? excludeBranchId = null, CancellationToken cancellationToken = default);

        // -------------------- [Integrity/Dependency Checks] --------------------
        /// <summary>
        /// التحقق مما إذا كانت شركة معينة تحتوي على فروع (قبل تنفيذ بعض العمليات).
        /// </summary>
        Task<bool> HasBranchesAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// التحقق مما إذا كان فرع مرتبطًا بالفعل بشركة معينة.
        /// </summary>
        Task<bool> IsBranchLinkedToCompanyAsync(Guid branchId, Guid companyId, CancellationToken cancellationToken = default);
    }
}