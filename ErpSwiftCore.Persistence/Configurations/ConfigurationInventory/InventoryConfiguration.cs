using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ErpSwiftCore.Persistence.Configurations.ConfigurationInventory
{
    public class InventoryConfiguration
        : AuditableEntityConfiguration<Inventory>,
          IEntityTypeConfiguration<Inventory>
    {
        public override void Configure(EntityTypeBuilder<Inventory> builder)
        {
            base.Configure(builder);

            // خريطة QuantityOnHand (مع الوصول من خلال الحقل الداخلي)
            builder.Property<int>("_quantityOnHand")
                   .HasColumnName("QuantityOnHand")
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            // خريطة QuantityReserved
            builder.Property(i => i.QuantityReserved)
                   .IsRequired();

            // علاقات
            builder.HasOne(i => i.Product)
                   .WithMany()
                   .HasForeignKey(i => i.ProductID);

            builder.HasOne(i => i.Warehouse)
                   .WithMany(w => w.Inventories)
                   .HasForeignKey(i => i.WarehouseID);

            builder.HasMany(i => i.Transactions)
                   .WithOne(t => t.Inventory)
                   .HasForeignKey(t => t.InventoryID)
                   .OnDelete(DeleteBehavior.Cascade);
             

            builder.HasOne(i => i.Policy)
                   .WithOne(p => p.Inventory)
                   .HasForeignKey<InventoryPolicy>(p => p.InventoryID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}