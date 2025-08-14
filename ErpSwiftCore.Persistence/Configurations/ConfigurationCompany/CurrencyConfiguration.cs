using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationCompany
{
    public class CurrencyConfiguration
           : BaseEntityConfiguration<Currency>
    {
        public override void Configure(EntityTypeBuilder<Currency> builder)
        {
            // إعداد الخصائص العامّة من BaseEntity
            base.Configure(builder);

            // إعداد خصائص الـ Currency
            builder.Property(c => c.CurrencyCode)
                   .IsRequired()
                   .HasMaxLength(3);
            builder.Property(c => c.CurrencyName)
                   .HasMaxLength(50);
        }
    }
}