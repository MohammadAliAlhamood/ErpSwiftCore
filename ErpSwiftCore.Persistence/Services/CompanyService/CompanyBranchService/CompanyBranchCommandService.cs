using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService;
using ErpSwiftCore.SharedKernel.Entities; 

namespace ErpSwiftCore.Persistence.Services.CompanyService.CompanyBranchService
{
    /// <summary>
    /// تنفيذ أوامر إدارة فروع الشركات - يشمل جميع العمليات التي تُحدث تغييرات
    /// (إنشاء، تعديل، حذف/أرشفة، استعادة، تغيير الحالة) ودعم الإضافة بالجملة.
    /// يعتمد على IUnitOfWork و ICompanyBranchValidationService.
    /// </summary>
    public class CompanyBranchCommandService : ICompanyBranchCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyBranchValidationService _validationService;
        public CompanyBranchCommandService(IUnitOfWork unitOfWork, ICompanyBranchValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }
        public async Task<Guid> CreateBranchAsync(CompanyBranch branch, CancellationToken cancellationToken = default)
        {
            if (!await _unitOfWork.Company.ExistsAsync(c => c.ID == branch.CompanyID, cancellationToken))
                throw new InvalidOperationException("الشركة غير موجودة.");
            if (!await _validationService.IsBranchNameUniqueAsync(branch.CompanyID, branch.BranchName, null, cancellationToken))
                throw new InvalidOperationException("اسم الفرع مستخدم بالفعل.");
            if (!string.IsNullOrWhiteSpace(branch.BranchCode) &&
                await _validationService.BranchExistsByCodeAsync(branch.CompanyID, branch.BranchCode, cancellationToken))
                throw new InvalidOperationException("كود الفرع مستخدم بالفعل.");
            return await _unitOfWork.CompanyBranch.AddAsync(branch, cancellationToken);
        }
        public async Task<IEnumerable<Guid>> AddBranchesRangeAsync(IEnumerable<CompanyBranch> branches, CancellationToken cancellationToken = default)
        {
            var validBranches = new List<CompanyBranch>();
            foreach (var branch in branches)
            {
                if (!await _unitOfWork.Company.ExistsAsync(c => c.ID == branch.CompanyID, cancellationToken))
                    throw new InvalidOperationException("الشركة غير موجودة.");
                if (!await _validationService.IsBranchNameUniqueAsync(branch.CompanyID, branch.BranchName, null, cancellationToken))
                    throw new InvalidOperationException($"اسم الفرع '{branch.BranchName}' مستخدم بالفعل.");
                if (!string.IsNullOrWhiteSpace(branch.BranchCode) &&
                    await _validationService.BranchExistsByCodeAsync(branch.CompanyID, branch.BranchCode, cancellationToken))
                    throw new InvalidOperationException($"كود الفرع '{branch.BranchCode}' مستخدم بالفعل.");
                validBranches.Add(branch);
            }
            return await _unitOfWork.CompanyBranch.AddRangeAsync(validBranches, cancellationToken);
        }
        public async Task<bool> UpdateBranchAsync(CompanyBranch branch, CancellationToken cancellationToken = default)
        {
            // تحقق من وجود الشركة
            if (!await _unitOfWork.Company.ExistsAsync(c => c.ID == branch.CompanyID, cancellationToken))
                throw new InvalidOperationException("الشركة غير موجودة.");

            // تحقق من تفرّد اسم الفرع مع استثناء الفرع الحالي
            if (!await _validationService.IsBranchNameUniqueAsync(branch.CompanyID, branch.BranchName, branch.ID, cancellationToken))
                throw new InvalidOperationException("اسم الفرع مستخدم بالفعل.");

            // تحقق من تفرّد كود الفرع مع استثناء الفرع الحالي
            if (!string.IsNullOrWhiteSpace(branch.BranchCode) &&
                await _validationService.BranchExistsByCodeAsync(branch.CompanyID, branch.BranchCode, cancellationToken))
            {
                var existing = await _unitOfWork.CompanyBranch.GetByIdAsync(branch.ID, cancellationToken);
                if (existing != null && existing.BranchCode != branch.BranchCode)
                    throw new InvalidOperationException("كود الفرع مستخدم بالفعل.");
            }

            return await _unitOfWork.CompanyBranch.UpdateAsync(branch, cancellationToken);
        }
        public async Task<bool> DeleteBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            // حذف منطقي لفرع واحد مع التحقق من الوجود
            if (!await _unitOfWork.CompanyBranch.ExistsAsync(branchId, cancellationToken))
                return false;

            return await _unitOfWork.CompanyBranch.SoftDeleteAsync(branchId, cancellationToken);
        }
        public async Task<bool> DeleteBranchesRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            // حذف منطقي لمجموعة فروع
            return await _unitOfWork.CompanyBranch.SoftDeleteRangeAsync(branchIds, cancellationToken);
        }
        public async Task<bool> DeleteAllBranchesOfCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            // حذف منطقي لجميع فروع الشركة
            return await _unitOfWork.CompanyBranch.SoftDeleteAllByCompanyAsync(companyId, cancellationToken);
        }
        public async Task<bool> RestoreBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            // استرجاع فرع مؤرشف
            return await _unitOfWork.CompanyBranch.RestoreAsync(branchId, cancellationToken);
        }
        public async Task<bool> RestoreBranchesRangeAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            // استرجاع مجموعة فروع مؤرشفة
            return await _unitOfWork.CompanyBranch.RestoreRangeAsync(branchIds, cancellationToken);
        }
        public async Task<bool> RestoreAllBranchesOfCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            // استرجاع جميع الفروع المؤرشفة لشركة
            return await _unitOfWork.CompanyBranch.RestoreAllByCompanyAsync(companyId, cancellationToken);
        }
       
    }
}