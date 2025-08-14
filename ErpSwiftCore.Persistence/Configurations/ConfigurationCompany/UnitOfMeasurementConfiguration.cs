using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationCompany
{
    public class UnitOfMeasurementConfiguration
       : BaseEntityConfiguration<UnitOfMeasurement>
    {
        public override void Configure(EntityTypeBuilder<UnitOfMeasurement> builder)
        {
            // إعداد الخصائص العامّة من BaseEntity
            base.Configure(builder);

            // إعداد خصائص الـ UnitOfMeasurement
            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(u => u.Abbreviation)
                   .HasMaxLength(10);
            builder.Property(u => u.Description)
                   .HasMaxLength(100);
        }
    }
}