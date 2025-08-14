using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationCompany
{
    public class CompanyConfiguration
        : BaseEntityConfiguration<Company>,
          IEntityTypeConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.CompanyName)
                   .IsRequired()
                   .HasMaxLength(100); 
            builder.Property(c => c.WebsiteURL)
                   .HasMaxLength(255);
            builder.Property(c => c.TaxID)
                   .HasMaxLength(50);
            builder.Property(c => c.LogoURL)
                   .HasMaxLength(255);
            builder.Property(c => c.Notes)
                   .HasMaxLength(1000);
            builder.Property(c => c.IndustryType)
                   .IsRequired();

            // Owned types
            builder.OwnsOne(c => c.Address);
            builder.OwnsOne(c => c.ContactInfo);
        }
    }
}