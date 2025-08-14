using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class InvoiceTaxConfiguration : AuditableEntityConfiguration<InvoiceTax>, IEntityTypeConfiguration<InvoiceTax>
    {
        public override void Configure(EntityTypeBuilder<InvoiceTax> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.TaxName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(t => t.TaxRate)
                   .IsRequired();
            builder.Property(t => t.TaxAmount)
                   .IsRequired();
            builder.HasOne(t => t.Invoice)
                   .WithMany(i => i.Taxes)
                   .HasForeignKey(t => t.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}