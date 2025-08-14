using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationInventory
{
    public class StockTransferConfiguration
    : AuditableEntityConfiguration<StockTransfer>,
      IEntityTypeConfiguration<StockTransfer>
    {
        public override void Configure(EntityTypeBuilder<StockTransfer> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Quantity)
                   .IsRequired();

            builder.Property(s => s.TransferDate)
                   .IsRequired();

            builder.Property(s => s.ReferenceNumber)
                   .HasMaxLength(100);

            builder.Property(s => s.Notes)
                   .HasMaxLength(250);

            builder.HasOne(s => s.Product)
                   .WithMany()
                   .HasForeignKey(s => s.ProductID);

            builder.HasOne(s => s.FromWarehouse)
                   .WithMany()
                   .HasForeignKey(s => s.FromWarehouseID);

            builder.HasOne(s => s.ToWarehouse)
                   .WithMany()
                   .HasForeignKey(s => s.ToWarehouseID);
        }
    }
}