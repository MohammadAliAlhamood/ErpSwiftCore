using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationInventory
{
    public class WarehouseConfiguration
     : AuditableEntityConfiguration<Warehouse>,
       IEntityTypeConfiguration<Warehouse>
    {
        public override void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            base.Configure(builder);

            builder.Property(w => w.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(w => w.Location)
                   .HasMaxLength(255);

            builder.HasOne(w => w.Branch)
                   .WithMany()
                   .HasForeignKey(w => w.BranchID);
             

            builder.HasMany(w => w.Inventories)
                   .WithOne(i => i.Warehouse)
                   .HasForeignKey(i => i.WarehouseID);
        }
    }
}