using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationFinancial
{
    public class CostCenterConfiguration : AuditableEntityConfiguration<CostCenter> 
    {
        public override void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.CenterName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
        }
    }
}