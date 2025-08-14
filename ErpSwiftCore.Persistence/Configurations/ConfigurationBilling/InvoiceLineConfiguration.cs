using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class InvoiceLineConfiguration
        : AuditableEntityConfiguration<InvoiceLine>,
          IEntityTypeConfiguration<InvoiceLine>
    {
        public override void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            // BaseEntity + AuditableEntity
            base.Configure(builder);
            builder.Property(l => l.Description).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Quantity).IsRequired();
            builder.Property(l => l.UnitPrice).IsRequired();
            builder.Property(l => l.Discount);
            builder.Ignore(l => l.SubTotal);
            builder.HasOne(l => l.Invoice).WithMany(i => i.Lines)
                   .HasForeignKey(l => l.InvoiceId);
            builder.HasOne(l => l.Product).WithMany()
                   .HasForeignKey(l => l.ProductId);
        }
    }
}