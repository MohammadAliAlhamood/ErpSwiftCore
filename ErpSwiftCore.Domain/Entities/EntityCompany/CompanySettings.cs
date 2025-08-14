using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.EntityCompany
{
    public class CompanySettings : BaseEntity
    {
        // === معلومات الشركة الأساسية ===
        public Guid CompanyID { get; set; }

        public Company? Company { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        public string? TimeZone { get; set; }
        public string? DefaultLanguage { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal TaxRate { get; set; } = 0m;

        // === [Default Account Settings] ===
        public Guid DefaultARAccountId { get; set; }

        public Account? DefaultARAccount { get; set; }

        public Guid DefaultRevenueAccountId { get; set; }
        public Account? DefaultRevenueAccount { get; set; }

        public Guid DefaultTaxPayableAccountId { get; set; }
        public Account? DefaultTaxPayableAccount { get; set; }

        public Guid DefaultDiscountsAccountId { get; set; }
        public Account? DefaultDiscountsAccount { get; set; }

        // === [Payroll‐Specific Account Settings] ===
        public Guid SalaryExpenseAccountId { get; set; }

        public Account? SalaryExpenseAccount { get; set; }

        public Guid PayrollPayableAccountId { get; set; }
        public Account? PayrollPayableAccount { get; set; }

        public Guid PayrollDeductionsAccountId { get; set; }
        public Account? PayrollDeductionsAccount { get; set; }

        public Guid DefaultCashAccountId { get; set; }
        public Account? DefaultCashAccount { get; set; }
    }
}