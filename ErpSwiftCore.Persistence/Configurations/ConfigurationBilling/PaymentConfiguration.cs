using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class PaymentConfiguration
        : AuditableEntityConfiguration<Payment>,
          IEntityTypeConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.PaymentReference)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(p => p.PaymentDate)
                   .IsRequired();
            builder.Property(p => p.PaymentAmount)
                   .IsRequired();
            builder.HasOne(p => p.Invoice)
                   .WithMany(i => i.Payments)
                   .HasForeignKey(p => p.InvoiceId);
        }
    }
}