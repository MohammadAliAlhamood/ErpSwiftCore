using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Commands;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Validator.Commands
{


    #region ──────────── CompanyBranch Command Validators ────────────

    public class CreateCompanyBranchCommandValidator : AbstractValidator<CreateCompanyBranchCommand>
    {
        public CreateCompanyBranchCommandValidator(IValidator<CompanyBranchCreateDto> branchCreateValidator)
        {
           
            RuleFor(x => x.Branch)
                .NotNull().WithMessage("محتوى إنشاء الفرع لا يمكن أن يكون فارغًا.")
                .SetValidator(branchCreateValidator);
        }
    }

    public class BulkCreateCompanyBranchesCommandValidator : AbstractValidator<BulkCreateCompanyBranchesCommand>
    {
        public BulkCreateCompanyBranchesCommandValidator(IValidator<CompanyBranchCreateDto> branchCreateValidator)
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.Branches)
                .NotNull().WithMessage("قائمة الفروع لا يمكن أن تكون فارغة.")
                .Must(list => list != null && list.Any())
                    .WithMessage("يجب إدخال فرع واحد على الأقل.");

            RuleForEach(x => x.Branches)
                .SetValidator(branchCreateValidator);
        }
    }

    public class UpdateCompanyBranchCommandValidator : AbstractValidator<UpdateCompanyBranchCommand>
    {
        public UpdateCompanyBranchCommandValidator(IValidator<CompanyBranchUpdateDto> branchUpdateValidator)
        { 
            RuleFor(x => x.Branch)
                .NotNull().WithMessage("محتوى تحديث الفرع لا يمكن أن يكون فارغًا.")
                .SetValidator(branchUpdateValidator);
        }
    }

    public class DeleteCompanyBranchCommandValidator : AbstractValidator<DeleteCompanyBranchCommand>
    {
        public DeleteCompanyBranchCommandValidator()
        { 
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");
        }
    }

    public class BulkDeleteCompanyBranchesCommandValidator : AbstractValidator<BulkDeleteCompanyBranchesCommand>
    {
        public BulkDeleteCompanyBranchesCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.BranchIds)
                .NotNull().WithMessage("قائمة معرّفات الفروع لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الفروع يجب أن تكون صحيحة وغير فارغة.");
        }
    }

    public class DeleteAllCompanyBranchesCommandValidator : AbstractValidator<DeleteAllCompanyBranchesCommand>
    {
        public DeleteAllCompanyBranchesCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class RestoreCompanyBranchCommandValidator : AbstractValidator<RestoreCompanyBranchCommand>
    {
        public RestoreCompanyBranchCommandValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");
        }
    }

    public class BulkRestoreCompanyBranchesCommandValidator : AbstractValidator<BulkRestoreCompanyBranchesCommand>
    {
        public BulkRestoreCompanyBranchesCommandValidator()
        {
            RuleFor(x => x.BranchIds)
                .NotNull().WithMessage("قائمة معرّفات الفروع لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الفروع يجب أن تكون صحيحة وغير فارغة.");
        }
    }

    public class RestoreAllCompanyBranchesCommandValidator : AbstractValidator<RestoreAllCompanyBranchesCommand>
    {
        public RestoreAllCompanyBranchesCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class SetCompanyBranchActiveStatusCommandValidator : AbstractValidator<SetCompanyBranchActiveStatusCommand>
    {
        public SetCompanyBranchActiveStatusCommandValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("معرّف الفرع مطلوب.");
        }
    }

    public class BulkSetCompanyBranchesActiveStatusCommandValidator : AbstractValidator<BulkSetCompanyBranchesActiveStatusCommand>
    {
        public BulkSetCompanyBranchesActiveStatusCommandValidator()
        {
            RuleFor(x => x.BranchIds)
                .NotNull().WithMessage("قائمة معرّفات الفروع لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الفروع يجب أن تكون صحيحة وغير فارغة.");
        }
    }

    #endregion



}
