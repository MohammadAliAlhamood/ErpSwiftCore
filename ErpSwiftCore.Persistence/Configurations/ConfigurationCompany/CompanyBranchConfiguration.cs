using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationCompany
{
    public class CompanyBranchConfiguration
        : BaseEntityConfiguration<CompanyBranch>,
          IEntityTypeConfiguration<CompanyBranch>
    {
        public override void Configure(EntityTypeBuilder<CompanyBranch> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.BranchName)
                   .IsRequired()
                   .HasMaxLength(100);
            // Owned type

            // Owned types
            builder.OwnsOne(c => c.Address);
            builder.OwnsOne(c => c.ContactInfo);
        }
    }
}