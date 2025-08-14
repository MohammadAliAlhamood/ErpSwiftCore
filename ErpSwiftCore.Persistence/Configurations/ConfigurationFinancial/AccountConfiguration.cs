using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationFinancial
{
    public class AccountConfiguration  : AuditableEntityConfiguration<Account> 
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            // Base configuration (ID, auditing, soft-delete, tenant)
            base.Configure(builder);

            // AccountNumber
            builder.Property(a => a.AccountNumber)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasIndex(a => a.AccountNumber)
                   .HasDatabaseName("IX_Account_AccountNumber");

            // Name
            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.HasIndex(a => a.Name)
                   .HasDatabaseName("IX_Account_Name");

            // Description (اختياري)
            builder.Property(a => a.Description)
                   .HasMaxLength(500);

            // TransactionType (تصنيف محاسبي)
            builder.Property(a => a.TransactionType)
                   .IsRequired()
                   .HasConversion<int>();

            // AccountType (تصنيف تشغيلي)
            builder.Property(a => a.AccountType)
                   .IsRequired()
                   .HasConversion<int>();

            // IsActive (حالة الحساب)
            builder.Property(a => a.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.HasOne(p => p.Currency)
                   .WithMany()
                   .HasForeignKey(p => p.CurrencyId);

 
             
        }
    }
}
