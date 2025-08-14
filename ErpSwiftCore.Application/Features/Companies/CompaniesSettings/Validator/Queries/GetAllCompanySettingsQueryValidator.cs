using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Queries;
using FluentValidation;
using System;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Validator.Queries
{
    #region ──────────── CompanySettings Query Validators ────────────

     

    /// <summary>
    /// Validator لـ GetCompanySettingsByCompanyIdQuery: يتضمّن CompanyId.
    /// </summary>
    public class GetCompanySettingsByCompanyIdQueryValidator : AbstractValidator<GetCompanySettingsByCompanyIdQuery>
    {
        public GetCompanySettingsByCompanyIdQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage("معرّف الشركة مطلوب.");
        }
    }

    /// <summary>
    /// Validator لـ GetCompanySettingsByCurrencyQuery: يتضمّن CurrencyId.
    /// </summary>
    public class GetCompanySettingsByCurrencyQueryValidator : AbstractValidator<GetCompanySettingsByCurrencyQuery>
    {
        public GetCompanySettingsByCurrencyQueryValidator()
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithMessage("معرّف العملة مطلوب.");
        }
    }

    /// <summary>
    /// Validator لـ GetCompanySettingsByTimeZoneQuery: يتضمّن TimeZone.
    /// </summary>
    public class GetCompanySettingsByTimeZoneQueryValidator : AbstractValidator<GetCompanySettingsByTimeZoneQuery>
    {
        public GetCompanySettingsByTimeZoneQueryValidator()
        {
            RuleFor(x => x.TimeZone)
                .NotEmpty()
                .WithMessage("المنطقة الزمنية مطلوبة.");
        }
    }

    /// <summary>
    /// Validator لـ GetCompanySettingsPagedQuery: يتضمّن PageIndex و PageSize.
    /// </summary>
    public class GetCompanySettingsPagedQueryValidator : AbstractValidator<GetCompanySettingsPagedQuery>
    {
        public GetCompanySettingsPagedQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    /// <summary>
    /// Validator لـ GetCompanySettingsPagedByCurrencyQuery: يتضمّن CurrencyId، PageIndex، PageSize.
    /// </summary>
    public class GetCompanySettingsPagedByCurrencyQueryValidator : AbstractValidator<GetCompanySettingsPagedByCurrencyQuery>
    {
        public GetCompanySettingsPagedByCurrencyQueryValidator()
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithMessage("معرّف العملة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    /// <summary>
    /// Validator لـ GetCompanySettingsPagedByTimeZoneQuery: يتضمّن TimeZone، PageIndex، PageSize.
    /// </summary>
    public class GetCompanySettingsPagedByTimeZoneQueryValidator : AbstractValidator<GetCompanySettingsPagedByTimeZoneQuery>
    {
        public GetCompanySettingsPagedByTimeZoneQueryValidator()
        {
            RuleFor(x => x.TimeZone)
                .NotEmpty()
                .WithMessage("المنطقة الزمنية مطلوبة.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }

    /// <summary>
    /// Validator لـ SearchCompanySettingsByKeywordQuery: يتضمّن Keyword.
    /// </summary>
    public class SearchCompanySettingsByKeywordQueryValidator : AbstractValidator<SearchCompanySettingsByKeywordQuery>
    {
        public SearchCompanySettingsByKeywordQueryValidator()
        {
            RuleFor(x => x.Keyword)
                .NotEmpty()
                .WithMessage("الكلمة المفتاحية للبحث مطلوبة.");
        }
    }

    /// <summary>
    /// Validator لـ SettingsExistQuery: يتضمّن CompanyId.
    /// </summary>
    public class SettingsExistQueryValidator : AbstractValidator<SettingsExistQuery>
    {
        public SettingsExistQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage("معرّف الشركة مطلوب.");
        }
    }

    /// <summary>
    /// Validator لـ IsCompanySettingsUniqueQuery: يتضمّن CompanyId.
    /// </summary>
    public class IsCompanySettingsUniqueQueryValidator : AbstractValidator<IsCompanySettingsUniqueQuery>
    {
        public IsCompanySettingsUniqueQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .WithMessage("معرّف الشركة مطلوب.");
        }
    }
     

    #endregion
}
