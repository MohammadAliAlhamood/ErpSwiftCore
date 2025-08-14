using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ErpSwiftCore.Persistence.Configurations.ConfigurationCRM
{
    public class CustomerConfiguration
          : AuditableEntityConfiguration<Customer>,
            IEntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.CustomerCode)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(x => x.CreditLimit)
                   .HasColumnType("decimal(18,2)"); 
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.NationalID)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(x => x.MiddleName)
                   .HasMaxLength(50);
            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(50);
            // Owned types
            builder.OwnsOne(c => c.Address);
            builder.OwnsOne(c => c.ContactInfo);
        }
    }
}