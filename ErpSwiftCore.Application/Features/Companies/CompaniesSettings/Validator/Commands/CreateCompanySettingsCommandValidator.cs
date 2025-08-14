using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Validator.Commands
{
    #region ──────────── CompanySettings Command Validators ────────────

    /// <summary>
    /// Validator لـ CreateCompanySettingsCommand: يتضمّن CompanySettingsCreateDto.
    /// </summary>
    public class CreateCompanySettingsCommandValidator : AbstractValidator<CreateCompanySettingsCommand>
    {
        public CreateCompanySettingsCommandValidator()
        {
            RuleFor(x => x.Settings)
                .NotNull()
                .WithMessage("محتوى إنشاء الإعدادات لا يمكن أن يكون فارغًا.")
                .SetValidator(new CompanySettingsCreateDtoValidator(null!));
        }
    }

    /// <summary>
    /// Validator لـ BulkCreateCompanySettingsCommand: يتضمّن قائمة CompanySettingsCreateDto.
    /// </summary>
    public class BulkCreateCompanySettingsCommandValidator : AbstractValidator<BulkCreateCompanySettingsCommand>
    {
        public BulkCreateCompanySettingsCommandValidator()
        {
            RuleFor(x => x._Dto.SettingsList)
                .NotNull()
                .WithMessage("قائمة إعدادات الشركة لا يمكن أن تكون فارغة.")
                .Must(list => list != null && list.Any())
                    .WithMessage("يجب إدخال إعداد واحد على الأقل.");

            RuleForEach(x => x._Dto.SettingsList)
                .SetValidator(new CompanySettingsCreateDtoValidator(null!));
        }
    }

    /// <summary>
    /// Validator لـ UpdateCompanySettingsCommand: يتضمّن CompanySettingsUpdateDto.
    /// </summary>
    public class UpdateCompanySettingsCommandValidator : AbstractValidator<UpdateCompanySettingsCommand>
    {
        public UpdateCompanySettingsCommandValidator()
        {
            RuleFor(x => x.Settings)
                .NotNull()
                .WithMessage("محتوى تحديث الإعدادات لا يمكن أن يكون فارغًا.")
                .SetValidator(new CompanySettingsUpdateDtoValidator(null!));
        }
    }

    /// <summary>
    /// Validator لـ UpdateCompanySettingsCurrencyCommand: يتضمّن CompanySettingsCurrencyUpdateDto.
    /// </summary>
    public class UpdateCompanySettingsCurrencyCommandValidator : AbstractValidator<UpdateCompanySettingsCurrencyCommand>
    {
        public UpdateCompanySettingsCurrencyCommandValidator()
        {
            RuleFor(x => x.Payload)
                .NotNull()
                .WithMessage("محتوى تحديث العملة لا يمكن أن يكون فارغًا.")
                .SetValidator(new CompanySettingsCurrencyUpdateDtoValidator(null!));
        }
    }

    /// <summary>
    /// Validator لـ UpdateCompanySettingsTimeZoneCommand: يتضمّن CompanySettingsTimeZoneUpdateDto.
    /// </summary>
    public class UpdateCompanySettingsTimeZoneCommandValidator : AbstractValidator<UpdateCompanySettingsTimeZoneCommand>
    {
        public UpdateCompanySettingsTimeZoneCommandValidator()
        {
            RuleFor(x => x.Payload)
                .NotNull()
                .WithMessage("محتوى تحديث المنطقة الزمنية لا يمكن أن يكون فارغًا.")
                .SetValidator(new CompanySettingsTimeZoneUpdateDtoValidator(null!));
        }
    }

    /// <summary>
    /// Validator لـ DeleteCompanySettingsCommand.
    /// </summary>
    public class DeleteCompanySettingsCommandValidator : AbstractValidator<DeleteCompanySettingsCommand>
    {
        public DeleteCompanySettingsCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage("معرّف الشركة مطلوب.");
        }
    }

    /// <summary>
    /// Validator لـ BulkDeleteCompanySettingsCommand.
    /// </summary>
    public class BulkDeleteCompanySettingsCommandValidator : AbstractValidator<BulkDeleteCompanySettingsCommand>
    {
        public BulkDeleteCompanySettingsCommandValidator()
        {
            RuleFor(x => x._Dto.CompanyIds)
                .NotNull()
                .WithMessage("قائمة معرّفات الشركات لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الشركات يجب أن تكون صحيحة وغير فارغة.");
        }
    }

    /// <summary>
    /// Validator لـ DeleteAllCompanySettingsCommand.
    /// </summary>
    public class DeleteAllCompanySettingsCommandValidator : AbstractValidator<DeleteAllCompanySettingsCommand>
    {
        public DeleteAllCompanySettingsCommandValidator()
        {
            // لا حقول للتحقق
        }
    }

    /// <summary>
    /// Validator لـ RestoreCompanySettingsCommand.
    /// </summary>
    public class RestoreCompanySettingsCommandValidator : AbstractValidator<RestoreCompanySettingsCommand>
    {
        public RestoreCompanySettingsCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage("معرّف الشركة مطلوب.");
        }
    }

    /// <summary>
    /// Validator لـ BulkRestoreCompanySettingsCommand.
    /// </summary>
    public class BulkRestoreCompanySettingsCommandValidator : AbstractValidator<BulkRestoreCompanySettingsCommand>
    {
        public BulkRestoreCompanySettingsCommandValidator()
        {
            RuleFor(x => x._Dto.CompanyIds)
                .NotNull()
                .WithMessage("قائمة معرّفات الشركات لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الشركات يجب أن تكون صحيحة وغير فارغة.");
        }
    }

  

   

    #endregion
}
