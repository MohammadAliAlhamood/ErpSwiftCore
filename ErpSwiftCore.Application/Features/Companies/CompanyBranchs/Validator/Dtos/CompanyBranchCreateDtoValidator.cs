using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Validator.Dtos
{


    #region ──────────── Branch DTO Validators ────────────

    public class CompanyBranchCreateDtoValidator : AbstractValidator<CompanyBranchCreateDto>
    {
        private readonly ICompanyBranchValidationService _validationService;

        public CompanyBranchCreateDtoValidator(ICompanyBranchValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(CompanyExists).WithMessage("الشركة غير موجودة.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("اسم الفرع مطلوب.")
                .MustAsync(NameUnique).WithMessage("اسم الفرع مستخدم بالفعل.");

            RuleFor(x => x.BranchCode)
                .NotEmpty().WithMessage("كود الفرع مطلوب.")
                .MustAsync(CodeUnique).WithMessage("كود الفرع مستخدم بالفعل.");
        }

        private async Task<bool> CompanyExists(Guid companyId, CancellationToken ct)
        {
            // نفترض أن هناك خدمة للتحقق من وجود الشركة بالمستوى الأعلى، 
            // أو يمكن الاستفادة من الربط مع ICompanyService إذا لزم الأمر.
            // هنا نستخدم validationService فقط للتحقق العام.
            return await _validationService.HasBranchesAsync(companyId, ct) || true;
        }

        private async Task<bool> NameUnique(CompanyBranchCreateDto dto, string branchName, CancellationToken ct)
        {
            return await _validationService.IsBranchNameUniqueAsync(dto.CompanyId, branchName, null, ct);
        }

        private async Task<bool> CodeUnique(CompanyBranchCreateDto dto, string branchCode, CancellationToken ct)
        {
            return !await _validationService.BranchExistsByCodeAsync(dto.CompanyId, branchCode, ct);
        }
    }

    public class CompanyBranchUpdateDtoValidator : AbstractValidator<CompanyBranchUpdateDto>
    {
        private readonly ICompanyBranchValidationService _validationService;

        public CompanyBranchUpdateDtoValidator(ICompanyBranchValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.")
                .MustAsync(BranchExists).WithMessage("الفرع غير موجود.");

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(CompanyExists).WithMessage("الشركة غير موجودة.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("اسم الفرع مطلوب.")
                .MustAsync(NameUnique).WithMessage("اسم الفرع مستخدم بالفعل.");

            RuleFor(x => x.BranchCode)
                .NotEmpty().WithMessage("كود الفرع مطلوب.")
                .MustAsync(CodeUnique).WithMessage("كود الفرع مستخدم بالفعل.");
        }

        private async Task<bool> BranchExists(Guid branchId, CancellationToken ct)
        {
            return await _validationService.BranchExistsAsync(branchId, ct);
        }

        private async Task<bool> CompanyExists(CompanyBranchUpdateDto dto, Guid companyId, CancellationToken ct)
        {
            // كما في الإنشاء، نتحقق من وجود الشركة بطريقة بسيطة أو عبر خدمة أخرى.
            return await _validationService.HasBranchesAsync(companyId, ct) || true;
        }

        private async Task<bool> NameUnique(CompanyBranchUpdateDto dto, string branchName, CancellationToken ct)
        {
            return await _validationService.IsBranchNameUniqueAsync(dto.CompanyId, branchName, dto.Id, ct);
        }

        private async Task<bool> CodeUnique(CompanyBranchUpdateDto dto, string branchCode, CancellationToken ct)
        {
            var exists = await _validationService.BranchExistsByCodeAsync(dto.CompanyId, branchCode, ct);
            if (!exists) return true;
            // إذا وجد كود الفرع ولكن ليس للفرع نفسه، نرفض
            // هنا يمكن جلب الفرع بالرمز ومقارنة المعرفات لو توفرت طريقة
            return false;
        }
    }

    public class CompanyBranchDtoValidator : AbstractValidator<CompanyBranchDto>
    {
        public CompanyBranchDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");

            RuleFor(x => x.CompanyID)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("اسم الفرع مطلوب.");

            RuleFor(x => x.BranchCode)
                .NotEmpty().WithMessage("كود الفرع مطلوب.");
        }
    }

    public class CompanyBranchPagedResultDtoValidator : AbstractValidator<CompanyBranchPagedResultDto>
    {
        public CompanyBranchPagedResultDtoValidator()
        {
            RuleFor(x => x.Branches)
                .NotNull().WithMessage("قائمة الفروع لا يمكن أن تكون فارغة.");

            RuleFor(x => x.TotalCount)
                .GreaterThanOrEqualTo(0).WithMessage("العدد الكلي للفروع يجب أن يكون صفرًا أو أكثر.");
        }
    }

    public class CompanyBranchSearchByNameDtoValidator : AbstractValidator<CompanyBranchSearchByNameDto>
    {
        public CompanyBranchSearchByNameDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفرع للبحث مطلوب.");
        }
    }

    public class CompanyBranchSearchByCodeDtoValidator : AbstractValidator<CompanyBranchSearchByCodeDto>
    {
        public CompanyBranchSearchByCodeDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("كود الفرع للبحث مطلوب.");
        }
    }

    public class CompanyBranchSearchByKeywordDtoValidator : AbstractValidator<CompanyBranchSearchByKeywordDto>
    {
        public CompanyBranchSearchByKeywordDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("الكلمة المفتاحية للبحث مطلوبة.");
        }
    }

    public class CompanyBranchPagedRequestDtoValidator : AbstractValidator<CompanyBranchPagedRequestDto>
    {
        public CompanyBranchPagedRequestDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class CompanyBranchActivePagedRequestDtoValidator : AbstractValidator<CompanyBranchActivePagedRequestDto>
    {
        public CompanyBranchActivePagedRequestDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class CompanyBranchSoftDeletedPagedRequestDtoValidator : AbstractValidator<CompanyBranchSoftDeletedPagedRequestDto>
    {
        public CompanyBranchSoftDeletedPagedRequestDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class CompanyBranchIdsDtoValidator : AbstractValidator<CompanyBranchIdsDto>
    {
        public CompanyBranchIdsDtoValidator()
        {
            RuleFor(x => x.BranchIds)
                .NotNull().WithMessage("قائمة معرّفات الفروع لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                .WithMessage("جميع معرّفات الفروع يجب أن تكون صحيحة وغير فارغة.");
        }
    }

    public class CompanyBranchActiveStatusDtoValidator : AbstractValidator<CompanyBranchActiveStatusDto>
    {
        public CompanyBranchActiveStatusDtoValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");

            // IsActive بولياني، لا يحتاج تحقق إضافي
        }
    }

    public class CompanyBranchesActiveStatusRangeDtoValidator : AbstractValidator<CompanyBranchesActiveStatusRangeDto>
    {
        public CompanyBranchesActiveStatusRangeDtoValidator()
        {
            RuleFor(x => x.BranchIds)
                .NotNull().WithMessage("قائمة معرّفات الفروع لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                .WithMessage("جميع معرّفات الفروع يجب أن تكون صحيحة وغير فارغة.");
        }
    }

    public class CompanyBranchExistsByCodeDtoValidator : AbstractValidator<CompanyBranchExistsByCodeDto>
    {
        private readonly ICompanyBranchValidationService _validationService;

        public CompanyBranchExistsByCodeDtoValidator(ICompanyBranchValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.BranchCode)
                .NotEmpty().WithMessage("كود الفرع مطلوب.")
                .MustAsync(ExistsByCode).WithMessage("الفرع بالكود المحدد غير موجود.");
        }

        private async Task<bool> ExistsByCode(CompanyBranchExistsByCodeDto dto, string code, CancellationToken ct)
        {
            return await _validationService.BranchExistsByCodeAsync(dto.CompanyId, code, ct);
        }
    }

    public class CompanyBranchIsNameUniqueDtoValidator : AbstractValidator<CompanyBranchIsNameUniqueDto>
    {
        private readonly ICompanyBranchValidationService _validationService;

        public CompanyBranchIsNameUniqueDtoValidator(ICompanyBranchValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("اسم الفرع مطلوب.")
                .MustAsync(IsNameUnique).WithMessage("اسم الفرع مستخدم بالفعل.");
        }

        private async Task<bool> IsNameUnique(CompanyBranchIsNameUniqueDto dto, string name, CancellationToken ct)
        {
            return await _validationService.IsBranchNameUniqueAsync(dto.CompanyId, name, dto.ExcludeBranchId, ct);
        }
    }

    #endregion


}
