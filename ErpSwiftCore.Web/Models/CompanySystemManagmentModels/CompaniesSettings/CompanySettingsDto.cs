using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompaniesSettings
{

    #region ──────────── Settings DTOs ────────────

    public class CompanySettingsDto
    {
        public Guid CompanyId { get; set; }
        public Guid CurrencyId { get; set; }
        public string? TimeZone { get; set; }
        public string? DefaultLanguage { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal TaxRate { get; set; }

        public Guid DefaultARAccountId { get; set; }
        public Guid DefaultRevenueAccountId { get; set; }
        public Guid DefaultTaxPayableAccountId { get; set; }
        public Guid DefaultDiscountsAccountId { get; set; }

        public Guid SalaryExpenseAccountId { get; set; }
        public Guid PayrollPayableAccountId { get; set; }
        public Guid PayrollDeductionsAccountId { get; set; }
        public Guid DefaultCashAccountId { get; set; }
    }

    public class CompanySettingsUpdateDto
    {
        public Guid Id { get; set; }
        public Guid CurrencyId { get; set; }
        public string? TimeZone { get; set; }
        public string? DefaultLanguage { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal TaxRate { get; set; }

        public Guid DefaultARAccountId { get; set; }
        public Guid DefaultRevenueAccountId { get; set; }
        public Guid DefaultTaxPayableAccountId { get; set; }
        public Guid DefaultDiscountsAccountId { get; set; }

        public Guid SalaryExpenseAccountId { get; set; }
        public Guid PayrollPayableAccountId { get; set; }
        public Guid PayrollDeductionsAccountId { get; set; }
        public Guid DefaultCashAccountId { get; set; }
    }

    #endregion

    #region ──────────── Create / BulkCreate DTOs ────────────
    public class CompanySettingsCreateDto
    {
        public Guid CompanyId { get; set; }
        public Guid CurrencyId { get; set; }
        public string? TimeZone { get; set; }
        public string? DefaultLanguage { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal TaxRate { get; set; }

        public Guid DefaultARAccountId { get; set; }
        public Guid DefaultRevenueAccountId { get; set; }
        public Guid DefaultTaxPayableAccountId { get; set; }
        public Guid DefaultDiscountsAccountId { get; set; }

        public Guid SalaryExpenseAccountId { get; set; }
        public Guid PayrollPayableAccountId { get; set; }
        public Guid PayrollDeductionsAccountId { get; set; }
        public Guid DefaultCashAccountId { get; set; }
    }

    public class CompanySettingsBulkCreateDto
    {
        public IEnumerable<CompanySettingsCreateDto> SettingsList { get; set; } = new List<CompanySettingsCreateDto>();
    }

    #endregion



    #region ──────────── Read / Response DTOs ────────────
    public class CompanySettingsPagedResultDto
    {
        public IReadOnlyList<CompanySettingsDto> Settings { get; set; } = new List<CompanySettingsDto>();
        public int TotalCount { get; set; }
    }

    #endregion


    #region ──────────── Update DTOs ────────────
    public class CompanySettingsCurrencyUpdateDto
    {
        public Guid CompanyId { get; set; }
        public Guid CurrencyId { get; set; }
    }

    public class CompanySettingsTimeZoneUpdateDto
    {
        public Guid CompanyId { get; set; }
        public string TimeZone { get; set; } = string.Empty;
    }

    #endregion




    #region ──────────── Delete / Restore DTOs ────────────
    public class CompanySettingsDeleteDto
    {
        public Guid CompanyId { get; set; }
    }
    public class CompanySettingsBulkDeleteDto
    {
        public IEnumerable<Guid> CompanyIds { get; set; } = new List<Guid>();
    }
    public class CompanySettingsDeleteAllDto { }
    public class CompanySettingsRestoreDto
    {
        public Guid CompanyId { get; set; }
    }
    public class CompanySettingsBulkRestoreDto
    {
        public IEnumerable<Guid> CompanyIds { get; set; } = new List<Guid>();
    }
    public class CompanySettingsRestoreAllDto { }
    #endregion



  
    #region ──────────── Search & Filter DTOs ────────────
    public class CompanySettingsPagedRequestDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class CompanySettingsPagedByCurrencyRequestDto
    {
        public Guid CurrencyId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class CompanySettingsPagedByTimeZoneRequestDto
    {
        public string TimeZone { get; set; } = string.Empty;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class CompanySettingsSearchByKeywordDto
    {
        public string Keyword { get; set; } = string.Empty;
    }
    public class CompanySettingsSearchByCompanyIdDto
    {
        public Guid CompanyId { get; set; }
    }

    public class CompanySettingsSearchByCurrencyDto
    {
        public Guid CurrencyId { get; set; }
    }

  

    #endregion


     

     
}
