using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationInventory
{
    public class InventoryPolicyConfiguration
       : AuditableEntityConfiguration<InventoryPolicy>,
         IEntityTypeConfiguration<InventoryPolicy>
    {
        public override void Configure(EntityTypeBuilder<InventoryPolicy> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.PolicyType)
                   .IsRequired();

            builder.Property(p => p.ReorderLevel)
                   .IsRequired();

            builder.Property(p => p.MaxStockLevel)
                   .IsRequired();

            builder.Property(p => p.AutoReorderEnabled)
                   .IsRequired();

            builder.Property(p => p.AutoReorderQuantity);

            builder.Property(p => p.Notes)
                   .HasMaxLength(500);

            builder.HasOne(p => p.Inventory)
                   .WithOne(i => i.Policy)
                   .HasForeignKey<InventoryPolicy>(p => p.InventoryID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}