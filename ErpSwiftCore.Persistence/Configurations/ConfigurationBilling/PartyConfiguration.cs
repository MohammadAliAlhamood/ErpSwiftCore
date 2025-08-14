using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class PartyConfiguration
        : AuditableEntityConfiguration<Party>,
          IEntityTypeConfiguration<Party>
    {
        public override void Configure(EntityTypeBuilder<Party> builder)
        {
            // إعدادات BaseEntity + AuditableEntity
            base.Configure(builder);

            builder.ToTable("Parties");

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Email)
                   .HasMaxLength(100);

            builder.Property(p => p.Phone)
                   .HasMaxLength(20);

            builder.Property(p => p.TaxNumber)
                   .HasMaxLength(50);

            builder.Property(p => p.Address)
                   .HasMaxLength(250);

            // الربط الاختياري مع Customer و Supplier
            builder.HasOne(p => p.Customer)
                   .WithOne()
                   .HasForeignKey<Party>(p => p.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Supplier)
                   .WithOne()
                   .HasForeignKey<Party>(p => p.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);

            // روابط الطلبات والفواتير
            builder.HasMany(p => p.Orders)
                   .WithOne(o => o.Party)
                   .HasForeignKey(o => o.PartyId)
                   .OnDelete(DeleteBehavior.Cascade);
             
        }
    }
}
