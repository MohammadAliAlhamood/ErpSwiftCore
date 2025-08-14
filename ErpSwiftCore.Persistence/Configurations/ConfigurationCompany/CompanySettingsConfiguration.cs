using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationCompany
{
    public class CompanySettingsConfiguration
        : BaseEntityConfiguration<CompanySettings>,
          IEntityTypeConfiguration<CompanySettings>
    {
        public override void Configure(EntityTypeBuilder<CompanySettings> builder)
        {
            base.Configure(builder);
            builder.Property(s => s.TimeZone).HasMaxLength(50);
            builder.Property(s => s.DefaultLanguage).HasMaxLength(100);
            builder.Property(s => s.PaymentTerms).HasMaxLength(255);
            builder.HasOne(s => s.Company)
                   .WithMany()
                   .HasForeignKey(s => s.CompanyID)
                   .OnDelete(DeleteBehavior.Restrict);

            // علاقة للعملة
            builder.HasOne(s => s.Currency)
                   .WithMany()
                   .HasForeignKey(s => s.CurrencyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // --------------- Default Account Settings ---------------

            // حساب المدينين الافتراضي
            builder.HasOne(s => s.DefaultARAccount)
                   .WithMany()
                   .HasForeignKey(s => s.DefaultARAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            // حساب الإيرادات الافتراضي
            builder.HasOne(s => s.DefaultRevenueAccount)
                   .WithMany()
                   .HasForeignKey(s => s.DefaultRevenueAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            // حساب الضريبة المستحقة الافتراضي
            builder.HasOne(s => s.DefaultTaxPayableAccount)
                   .WithMany()
                   .HasForeignKey(s => s.DefaultTaxPayableAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            // حساب الخصومات الافتراضي
            builder.HasOne(s => s.DefaultDiscountsAccount)
                   .WithMany()
                   .HasForeignKey(s => s.DefaultDiscountsAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            // --------------- Payroll‐Specific Account Settings ---------------

            // حساب مصروفات الرواتب
            builder.HasOne(s => s.SalaryExpenseAccount)
                   .WithMany()
                   .HasForeignKey(s => s.SalaryExpenseAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            // حساب الذمم الدائنة للرواتب
            builder.HasOne(s => s.PayrollPayableAccount)
                   .WithMany()
                   .HasForeignKey(s => s.PayrollPayableAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            // حساب الاستقطاعات (ضرائب/تأمين/قروض)
            builder.HasOne(s => s.PayrollDeductionsAccount)
                   .WithMany()
                   .HasForeignKey(s => s.PayrollDeductionsAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            // حساب الصندوق/البنك الافتراضي
            builder.HasOne(s => s.DefaultCashAccount)
                   .WithMany()
                   .HasForeignKey(s => s.DefaultCashAccountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}