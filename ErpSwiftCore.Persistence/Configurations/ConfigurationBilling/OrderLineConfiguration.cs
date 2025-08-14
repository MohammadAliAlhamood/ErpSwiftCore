using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class OrderLineConfiguration
        : AuditableEntityConfiguration<OrderLine>,
          IEntityTypeConfiguration<OrderLine>
    {
        public override void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            base.Configure(builder);
            builder.Property(ol => ol.Quantity)
                   .IsRequired();
            builder.Property(ol => ol.UnitPrice)
                   .IsRequired();
            builder.Property(ol => ol.Discount)
                   .IsRequired();
            builder.Ignore(ol => ol.SubTotal);
            builder.HasOne(ol => ol.Order)
                   .WithMany(o => o.OrderLines)
                   .HasForeignKey(ol => ol.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ol => ol.Product)
                   .WithMany()
                   .HasForeignKey(ol => ol.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}