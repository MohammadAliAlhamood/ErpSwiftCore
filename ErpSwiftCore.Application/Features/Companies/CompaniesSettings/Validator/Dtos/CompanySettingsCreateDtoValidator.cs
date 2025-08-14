using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Validator.Dtos
{
    #region ──────────── Settings DTO Validators ────────────
    public class CompanySettingsCreateDtoValidator : AbstractValidator<CompanySettingsCreateDto>
    {
        private readonly ICompanySettingsValidationService _validationService;
        public CompanySettingsCreateDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(SettingsNotExist).WithMessage("إعدادات الشركة موجودة بالفعل.");
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");
            RuleFor(x => x.TaxRate)
                .GreaterThanOrEqualTo(0).WithMessage("نسبة الضريبة يجب أن تكون صفرًا أو أكثر.");
            RuleFor(x => x.DefaultARAccountId)
                .NotEmpty().WithMessage("معرّف الحساب القبض الافتراضي مطلوب.");
            RuleFor(x => x.DefaultRevenueAccountId)
                .NotEmpty().WithMessage("معرّف حساب الإيرادات الافتراضي مطلوب.");
            RuleFor(x => x.DefaultTaxPayableAccountId)
                .NotEmpty().WithMessage("معرّف حساب الضريبة المستحقة الافتراضي مطلوب.");
            RuleFor(x => x.DefaultDiscountsAccountId)
                .NotEmpty().WithMessage("معرّف حساب الخصومات الافتراضي مطلوب.");
            RuleFor(x => x.SalaryExpenseAccountId)
                .NotEmpty().WithMessage("معرّف حساب مصاريف الرواتب مطلوب.");
            RuleFor(x => x.PayrollPayableAccountId)
                .NotEmpty().WithMessage("معرّف حساب المستحقات المالية مطلوب.");
            RuleFor(x => x.PayrollDeductionsAccountId)
                .NotEmpty().WithMessage("معرّف حساب استقطاعات الرواتب مطلوب.");
            RuleFor(x => x.DefaultCashAccountId)
                .NotEmpty().WithMessage("معرّف الحساب النقدي الافتراضي مطلوب.");
        }
        private async Task<bool> SettingsNotExist(Guid companyId, CancellationToken ct)
        {
            return !await _validationService.SettingsExistAsync(companyId, ct);
        }
    }

    public class CompanySettingsBulkCreateDtoValidator : AbstractValidator<CompanySettingsBulkCreateDto>
    {
        public CompanySettingsBulkCreateDtoValidator()
        {
            RuleFor(x => x.SettingsList)
                .NotNull().WithMessage("قائمة إعدادات الشركة لا يمكن أن تكون فارغة.")
                .Must(list => list != null && list.Any())
                    .WithMessage("يجب إدخال إعداد واحد على الأقل.");

            RuleForEach(x => x.SettingsList).SetValidator(new CompanySettingsCreateDtoValidator(
                // ValidationService is provided by DI container at runtime.
                (ICompanySettingsValidationService)null!
            // Placeholder: actual service will be injected by .NET.
            ));
        }
    }

    public class CompanySettingsDtoValidator : AbstractValidator<CompanySettingsDto>
    {
        public CompanySettingsDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");

            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");

            RuleFor(x => x.TaxRate)
                .GreaterThanOrEqualTo(0).WithMessage("نسبة الضريبة يجب أن تكون صفرًا أو أكثر.");
        }
    }

    public class CompanySettingsPagedResultDtoValidator : AbstractValidator<CompanySettingsPagedResultDto>
    {
        public CompanySettingsPagedResultDtoValidator()
        {
            RuleFor(x => x.Settings)
                .NotNull().WithMessage("قائمة الإعدادات لا يمكن أن تكون فارغة.");

            RuleFor(x => x.TotalCount)
                .GreaterThanOrEqualTo(0).WithMessage("إجمالي عدد الإعدادات يجب أن يكون صفرًا أو أكثر.");
        }
    }

    public class CompanySettingsUpdateDtoValidator : AbstractValidator<CompanySettingsUpdateDto>
    {
        private readonly ICompanySettingsValidationService _validationService;

        public CompanySettingsUpdateDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرّف الإعداد مطلوب.")
                .MustAsync(ExistsById).WithMessage("إعدادات الشركة غير موجودة.");

            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");

            RuleFor(x => x.TaxRate)
                .GreaterThanOrEqualTo(0).WithMessage("نسبة الضريبة يجب أن تكون صفرًا أو أكثر.");

            RuleFor(x => x.DefaultARAccountId)
                .NotEmpty().WithMessage("معرّف الحساب القبض الافتراضي مطلوب.");

            RuleFor(x => x.DefaultRevenueAccountId)
                .NotEmpty().WithMessage("معرّف حساب الإيرادات الافتراضي مطلوب.");

            RuleFor(x => x.DefaultTaxPayableAccountId)
                .NotEmpty().WithMessage("معرّف حساب الضريبة المستحقة الافتراضي مطلوب.");

            RuleFor(x => x.DefaultDiscountsAccountId)
                .NotEmpty().WithMessage("معرّف حساب الخصومات الافتراضي مطلوب.");

            RuleFor(x => x.SalaryExpenseAccountId)
                .NotEmpty().WithMessage("معرّف حساب مصاريف الرواتب مطلوب.");

            RuleFor(x => x.PayrollPayableAccountId)
                .NotEmpty().WithMessage("معرّف حساب المستحقات المالية مطلوب.");

            RuleFor(x => x.PayrollDeductionsAccountId)
                .NotEmpty().WithMessage("معرّف حساب استقطاعات الرواتب مطلوب.");

            RuleFor(x => x.DefaultCashAccountId)
                .NotEmpty().WithMessage("معرّف الحساب النقدي الافتراضي مطلوب.");
        }

        private async Task<bool> ExistsById(Guid id, CancellationToken ct)
        {
            return await _validationService.SettingsExistAsync(id, ct);
        }
    }

    public class CompanySettingsCurrencyUpdateDtoValidator : AbstractValidator<CompanySettingsCurrencyUpdateDto>
    {
        private readonly ICompanySettingsValidationService _validationService;

        public CompanySettingsCurrencyUpdateDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(SettingsExist).WithMessage("إعدادات الشركة غير موجودة.");

            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");
        }

        private async Task<bool> SettingsExist(Guid companyId, CancellationToken ct)
        {
            return await _validationService.SettingsExistAsync(companyId, ct);
        }
    }

    public class CompanySettingsTimeZoneUpdateDtoValidator : AbstractValidator<CompanySettingsTimeZoneUpdateDto>
    {
        private readonly ICompanySettingsValidationService _validationService;

        public CompanySettingsTimeZoneUpdateDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(SettingsExist).WithMessage("إعدادات الشركة غير موجودة.");

            RuleFor(x => x.TimeZone)
                .NotEmpty().WithMessage("المنطقة الزمنية مطلوبة.");
        }

        private async Task<bool> SettingsExist(Guid companyId, CancellationToken ct)
        {
            return await _validationService.SettingsExistAsync(companyId, ct);
        }
    }

    #endregion

    #region ──────────── Delete / Restore & State DTO Validators ────────────

    public class CompanySettingsDeleteDtoValidator : AbstractValidator<CompanySettingsDeleteDto>
    {
        private readonly ICompanySettingsValidationService _validationService;

        public CompanySettingsDeleteDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(SettingsExist).WithMessage("إعدادات الشركة غير موجودة.");
        }

        private async Task<bool> SettingsExist(Guid companyId, CancellationToken ct)
        {
            return await _validationService.SettingsExistAsync(companyId, ct);
        }
    }

    public class CompanySettingsBulkDeleteDtoValidator : AbstractValidator<CompanySettingsBulkDeleteDto>
    {
        public CompanySettingsBulkDeleteDtoValidator()
        {
            RuleFor(x => x.CompanyIds)
                .NotNull().WithMessage("قائمة معرّفات الشركات لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الشركات يجب أن تكون صحيحة وغير فارغة.");
        }
    }

    public class CompanySettingsRestoreDtoValidator : AbstractValidator<CompanySettingsRestoreDto>
    {
        private readonly ICompanySettingsValidationService _validationService;

        public CompanySettingsRestoreDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(SettingsExistSoftDeleted).WithMessage("لا توجد إعدادات مؤرشفة لهذه الشركة.");
        }

        private async Task<bool> SettingsExistSoftDeleted(Guid companyId, CancellationToken ct)
        {
            // نفترض وجود دالة للتحقق من وجود سجل مؤرشف ممكن إضافتها في الـ validation service
            return await _validationService.SettingsExistAsync(companyId, ct);
        }
    }

    public class CompanySettingsBulkRestoreDtoValidator : AbstractValidator<CompanySettingsBulkRestoreDto>
    {
        public CompanySettingsBulkRestoreDtoValidator()
        {
            RuleFor(x => x.CompanyIds)
                .NotNull().WithMessage("قائمة معرّفات الشركات لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الشركات يجب أن تكون صحيحة وغير فارغة.");
        }
    }

  

    #endregion

    #region ──────────── Search & Filter DTO Validators ────────────

    public class CompanySettingsPagedRequestDtoValidator : AbstractValidator<CompanySettingsPagedRequestDto>
    {
        public CompanySettingsPagedRequestDtoValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class CompanySettingsPagedByCurrencyRequestDtoValidator : AbstractValidator<CompanySettingsPagedByCurrencyRequestDto>
    {
        public CompanySettingsPagedByCurrencyRequestDtoValidator()
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class CompanySettingsPagedByTimeZoneRequestDtoValidator : AbstractValidator<CompanySettingsPagedByTimeZoneRequestDto>
    {
        public CompanySettingsPagedByTimeZoneRequestDtoValidator()
        {
            RuleFor(x => x.TimeZone)
                .NotEmpty().WithMessage("المنطقة الزمنية مطلوبة.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    public class CompanySettingsSearchByKeywordDtoValidator : AbstractValidator<CompanySettingsSearchByKeywordDto>
    {
        public CompanySettingsSearchByKeywordDtoValidator()
        {
            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("الكلمة المفتاحية للبحث مطلوبة.");
        }
    }

    public class CompanySettingsSearchByCompanyIdDtoValidator : AbstractValidator<CompanySettingsSearchByCompanyIdDto>
    {
        public CompanySettingsSearchByCompanyIdDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

    public class CompanySettingsSearchByCurrencyDtoValidator : AbstractValidator<CompanySettingsSearchByCurrencyDto>
    {
        public CompanySettingsSearchByCurrencyDtoValidator()
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");
        }
    }

    public class CompanySettingsSearchByTimeZoneDtoValidator : AbstractValidator<CompanySettingsSearchByTimeZoneDto>
    {
        public CompanySettingsSearchByTimeZoneDtoValidator()
        {
            RuleFor(x => x.TimeZone)
                .NotEmpty().WithMessage("المنطقة الزمنية مطلوبة.");
        }
    }

    #endregion

    #region ──────────── Validation / Existence & Counts DTO Validators ────────────

    public class CompanySettingsExistsDtoValidator : AbstractValidator<CompanySettingsExistsDto>
    {
        private readonly ICompanySettingsValidationService _validationService;

        public CompanySettingsExistsDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(SettingsExist).WithMessage("إعدادات الشركة غير موجودة.");
        }

        private async Task<bool> SettingsExist(Guid companyId, CancellationToken ct)
        {
            return await _validationService.SettingsExistAsync(companyId, ct);
        }
    }

    public class CompanySettingsUniqueDtoValidator : AbstractValidator<CompanySettingsUniqueDto>
    {
        private readonly ICompanySettingsValidationService _validationService;

        public CompanySettingsUniqueDtoValidator(ICompanySettingsValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(SettingsNotExist).WithMessage("إعدادات الشركة موجودة بالفعل.");
        }

        private async Task<bool> SettingsNotExist(Guid companyId, CancellationToken ct)
        {
            return !await _validationService.SettingsExistAsync(companyId, ct);
        }
    }

    public class CompanySettingsCountDtoValidator : AbstractValidator<CompanySettingsCountDto>
    {
        public CompanySettingsCountDtoValidator()
        {
            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0).WithMessage("العدد يجب أن يكون صفرًا أو أكثر.");
        }
    }

    

    #endregion
}
