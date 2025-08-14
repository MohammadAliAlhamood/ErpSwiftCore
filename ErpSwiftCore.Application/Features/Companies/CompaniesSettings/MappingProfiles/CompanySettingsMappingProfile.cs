using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using ErpSwiftCore.Domain.EntityCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.MappingProfiles
{

    /// <summary>
    /// يحتوي على قواعد AutoMapper الخاصة بتحويل الكائنات بين الـ Entities والـ DTOs الخاصة بإعدادات الشركة (CompanySettings).
    /// </summary>
    public class CompanySettingsMappingProfile : Profile
    {
        public CompanySettingsMappingProfile()
        {
            #region ──────────── CompanySettings Mappings ────────────

            // من CompanySettingsCreateDto إلى CompanySettings (Entity)
            CreateMap<CompanySettingsCreateDto, CompanySettings>(MemberList.None)
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.CompanyID, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.TimeZone))
                .ForMember(dest => dest.DefaultLanguage, opt => opt.MapFrom(src => src.DefaultLanguage))
                .ForMember(dest => dest.PaymentTerms, opt => opt.MapFrom(src => src.PaymentTerms))
                .ForMember(dest => dest.TaxRate, opt => opt.MapFrom(src => src.TaxRate))
                .ForMember(dest => dest.DefaultARAccountId, opt => opt.MapFrom(src => src.DefaultARAccountId))
                .ForMember(dest => dest.DefaultRevenueAccountId, opt => opt.MapFrom(src => src.DefaultRevenueAccountId))
                .ForMember(dest => dest.DefaultTaxPayableAccountId, opt => opt.MapFrom(src => src.DefaultTaxPayableAccountId))
                .ForMember(dest => dest.DefaultDiscountsAccountId, opt => opt.MapFrom(src => src.DefaultDiscountsAccountId))
                .ForMember(dest => dest.SalaryExpenseAccountId, opt => opt.MapFrom(src => src.SalaryExpenseAccountId))
                .ForMember(dest => dest.PayrollPayableAccountId, opt => opt.MapFrom(src => src.PayrollPayableAccountId))
                .ForMember(dest => dest.PayrollDeductionsAccountId, opt => opt.MapFrom(src => src.PayrollDeductionsAccountId))
                .ForMember(dest => dest.DefaultCashAccountId, opt => opt.MapFrom(src => src.DefaultCashAccountId));

            // من CompanySettingsUpdateDto إلى CompanySettings (Entity) للـ Update
            CreateMap<CompanySettingsUpdateDto, CompanySettings>(MemberList.None)
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.TimeZone))
                .ForMember(dest => dest.DefaultLanguage, opt => opt.MapFrom(src => src.DefaultLanguage))
                .ForMember(dest => dest.PaymentTerms, opt => opt.MapFrom(src => src.PaymentTerms))
                .ForMember(dest => dest.TaxRate, opt => opt.MapFrom(src => src.TaxRate))
                .ForMember(dest => dest.DefaultARAccountId, opt => opt.MapFrom(src => src.DefaultARAccountId))
                .ForMember(dest => dest.DefaultRevenueAccountId, opt => opt.MapFrom(src => src.DefaultRevenueAccountId))
                .ForMember(dest => dest.DefaultTaxPayableAccountId, opt => opt.MapFrom(src => src.DefaultTaxPayableAccountId))
                .ForMember(dest => dest.DefaultDiscountsAccountId, opt => opt.MapFrom(src => src.DefaultDiscountsAccountId))
                .ForMember(dest => dest.SalaryExpenseAccountId, opt => opt.MapFrom(src => src.SalaryExpenseAccountId))
                .ForMember(dest => dest.PayrollPayableAccountId, opt => opt.MapFrom(src => src.PayrollPayableAccountId))
                .ForMember(dest => dest.PayrollDeductionsAccountId, opt => opt.MapFrom(src => src.PayrollDeductionsAccountId))
                .ForMember(dest => dest.DefaultCashAccountId, opt => opt.MapFrom(src => src.DefaultCashAccountId));

            // من CompanySettingsCurrencyUpdateDto إلى CompanySettings (Entity)
            CreateMap<CompanySettingsCurrencyUpdateDto, CompanySettings>(MemberList.None)
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId));

            // من CompanySettingsTimeZoneUpdateDto إلى CompanySettings (Entity)
            CreateMap<CompanySettingsTimeZoneUpdateDto, CompanySettings>(MemberList.None)
                .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.TimeZone));

            // من CompanySettings إلى CompanySettingsDto (لـ Read / Response)
            CreateMap<CompanySettings, CompanySettingsDto>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyID))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.TimeZone))
                .ForMember(dest => dest.DefaultLanguage, opt => opt.MapFrom(src => src.DefaultLanguage))
                .ForMember(dest => dest.PaymentTerms, opt => opt.MapFrom(src => src.PaymentTerms))
                .ForMember(dest => dest.TaxRate, opt => opt.MapFrom(src => src.TaxRate))
                .ForMember(dest => dest.DefaultARAccountId, opt => opt.MapFrom(src => src.DefaultARAccountId))
                .ForMember(dest => dest.DefaultRevenueAccountId, opt => opt.MapFrom(src => src.DefaultRevenueAccountId))
                .ForMember(dest => dest.DefaultTaxPayableAccountId, opt => opt.MapFrom(src => src.DefaultTaxPayableAccountId))
                .ForMember(dest => dest.DefaultDiscountsAccountId, opt => opt.MapFrom(src => src.DefaultDiscountsAccountId))
                .ForMember(dest => dest.SalaryExpenseAccountId, opt => opt.MapFrom(src => src.SalaryExpenseAccountId))
                .ForMember(dest => dest.PayrollPayableAccountId, opt => opt.MapFrom(src => src.PayrollPayableAccountId))
                .ForMember(dest => dest.PayrollDeductionsAccountId, opt => opt.MapFrom(src => src.PayrollDeductionsAccountId))
                .ForMember(dest => dest.DefaultCashAccountId, opt => opt.MapFrom(src => src.DefaultCashAccountId));

            #endregion
        }
    }
}
