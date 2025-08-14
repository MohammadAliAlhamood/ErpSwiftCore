using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Validator.Queries
{


    #region ──────────── CompanyBranch Query Validators ────────────

    public class GetBranchByIdQueryValidator : AbstractValidator<GetBranchByIdQuery>
    {
        public GetBranchByIdQueryValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");
        }
    }

    public class GetBranchWithCompanyQueryValidator : AbstractValidator<GetBranchWithCompanyQuery>
    {
        public GetBranchWithCompanyQueryValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");
        }
    }

    public class GetAllBranchesQueryValidator : AbstractValidator<GetAllBranchesQuery>
    {
        public GetAllBranchesQueryValidator()
        {
            // لا توجد حقول تحقق
        }
    }

    public class GetBranchesByCompanyIdQueryValidator : AbstractValidator<GetBranchesByCompanyIdQuery>
    {
        public GetBranchesByCompanyIdQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class GetActiveBranchesByCompanyIdQueryValidator : AbstractValidator<GetActiveBranchesByCompanyIdQuery>
    {
        public GetActiveBranchesByCompanyIdQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class GetSoftDeletedBranchesByCompanyIdQueryValidator : AbstractValidator<GetSoftDeletedBranchesByCompanyIdQuery>
    {
        public GetSoftDeletedBranchesByCompanyIdQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class GetBranchesPagedQueryValidator : AbstractValidator<GetBranchesPagedQuery>
    {
        public GetBranchesPagedQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class GetActiveBranchesPagedQueryValidator : AbstractValidator<GetActiveBranchesPagedQuery>
    {
        public GetActiveBranchesPagedQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class GetSoftDeletedBranchesPagedQueryValidator : AbstractValidator<GetSoftDeletedBranchesPagedQuery>
    {
        public GetSoftDeletedBranchesPagedQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class SearchBranchesByNameQueryValidator : AbstractValidator<SearchBranchesByNameQuery>
    {
        public SearchBranchesByNameQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفرع للبحث مطلوب.");
        }
    }

    public class SearchBranchesByCodeQueryValidator : AbstractValidator<SearchBranchesByCodeQuery>
    {
        public SearchBranchesByCodeQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("كود الفرع للبحث مطلوب.");
        }
    }

    public class SearchBranchesByKeywordQueryValidator : AbstractValidator<SearchBranchesByKeywordQuery>
    {
        public SearchBranchesByKeywordQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("الكلمة المفتاحية للبحث مطلوبة.");
        }
    }

    public class BranchExistsQueryValidator : AbstractValidator<BranchExistsQuery>
    {
        public BranchExistsQueryValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");
        }
    }

    public class BranchExistsByCodeQueryValidator : AbstractValidator<BranchExistsByCodeQuery>
    {
        public BranchExistsByCodeQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.BranchCode)
                .NotEmpty().WithMessage("كود الفرع مطلوب.");
        }
    }

    public class IsBranchNameUniqueQueryValidator : AbstractValidator<IsBranchNameUniqueQuery>
    {
        public IsBranchNameUniqueQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("اسم الفرع مطلوب.");
        }
    }

    public class HasBranchesQueryValidator : AbstractValidator<HasBranchesQuery>
    {
        public HasBranchesQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class GetBranchesCountQueryValidator : AbstractValidator<GetBranchesCountQuery>
    {
        public GetBranchesCountQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class GetActiveBranchesCountQueryValidator : AbstractValidator<GetActiveBranchesCountQuery>
    {
        public GetActiveBranchesCountQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    #endregion

}
