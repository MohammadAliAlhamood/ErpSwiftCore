using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationCRM
{
    public class SupplierConfiguration
        : AuditableEntityConfiguration<Supplier>,
          IEntityTypeConfiguration<Supplier>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.SupplierCode)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(x => x.MaxSupplyLimit)
                   .HasColumnType("decimal(18,2)");

            // Owned types
            builder.OwnsOne(s => s.Address);
            builder.OwnsOne(s => s.ContactInfo);
        }
    }
}