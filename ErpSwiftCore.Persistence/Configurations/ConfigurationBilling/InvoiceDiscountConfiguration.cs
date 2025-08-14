using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class InvoiceDiscountConfiguration : AuditableEntityConfiguration<InvoiceDiscount>, IEntityTypeConfiguration<InvoiceDiscount>
    {
        public override void Configure(EntityTypeBuilder<InvoiceDiscount> builder)
        {
            base.Configure(builder);

            builder.Property(d => d.DiscountName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(d => d.DiscountRate)
                   .IsRequired();
            builder.Property(d => d.DiscountAmount)
                   .IsRequired();
            builder.HasOne(d => d.Invoice)
                   .WithMany(i => i.Discounts)
                   .HasForeignKey(d => d.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}