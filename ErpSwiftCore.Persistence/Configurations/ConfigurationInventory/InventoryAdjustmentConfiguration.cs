using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ErpSwiftCore.Persistence.Configurations.ConfigurationInventory
{
    public class InventoryAdjustmentConfiguration
      : AuditableEntityConfiguration<InventoryAdjustment>,
        IEntityTypeConfiguration<InventoryAdjustment>
    {
        public override void Configure(EntityTypeBuilder<InventoryAdjustment> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.QuantityChanged)
                   .IsRequired();

            builder.Property(a => a.Reason)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(a => a.AdjustmentDate)
                   .IsRequired();

            builder.HasOne(a => a.Product)
                   .WithMany()
                   .HasForeignKey(a => a.ProductID);

            builder.HasOne(a => a.Warehouse)
                   .WithMany()
                   .HasForeignKey(a => a.WarehouseID);
        }
    }
}