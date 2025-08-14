using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class OrderConfiguration
        : AuditableEntityConfiguration<Order>,
          IEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            // إعدادات BaseEntity + AuditableEntity
            base.Configure(builder);

            // خصائص الطلب الأساسية
            builder.Property(o => o.OrderNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.Property(o => o.OrderType)
                   .IsRequired();

            builder.Property(o => o.OrderStatus)
                   .IsRequired();

            builder.Property(o => o.TotalAmount)
                   .IsRequired();

            // العلاقة مع Party
            builder.Property(o => o.PartyId)
                   .IsRequired();

            builder.HasOne(o => o.Party)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(o => o.PartyId)
                   .OnDelete(DeleteBehavior.Restrict); // لحماية بيانات الطرف

            // العلاقة مع العملة
            builder.Property(o => o.CurrencyId)
                   .IsRequired();

            builder.HasOne(o => o.Currency)
                   .WithMany()
                   .HasForeignKey(o => o.CurrencyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع تفاصيل الطلب
            builder.HasMany(o => o.OrderLines)
                   .WithOne(ol => ol.Order)
                   .HasForeignKey(ol => ol.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
