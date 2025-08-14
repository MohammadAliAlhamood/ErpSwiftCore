using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class InvoiceConfiguration
        : AuditableEntityConfiguration<Invoice>,
          IEntityTypeConfiguration<Invoice>
    {
        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(20);
            builder.Property(i => i.InvoiceDate).IsRequired();
            builder.Property(i => i.DueDate);
            builder.Property(i => i.TotalAmount).IsRequired();
            builder.Property(i => i.PaidAmount).IsRequired();
            builder.Property(i => i.InvoiceStatus).IsRequired();
            builder.Property(i => i.IsFinalized).IsRequired();
            builder.Property(i => i.CurrencyId).IsRequired();
            builder.Property(i => i.OrderId).IsRequired();
            builder.HasOne(i => i.Currency)
                   .WithMany()
                   .HasForeignKey(i => i.CurrencyId)
                   .OnDelete(DeleteBehavior.Restrict);
            // المجمّعات
            builder.HasMany(i => i.Lines)
                   .WithOne(l => l.Invoice)
                   .HasForeignKey(l => l.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.Discounts)
                   .WithOne(d => d.Invoice)
                   .HasForeignKey(d => d.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.Taxes)
                   .WithOne(t => t.Invoice)
                   .HasForeignKey(t => t.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.Approvals)
                   .WithOne(a => a.Invoice)
                   .HasForeignKey(a => a.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.Payments)
                   .WithOne(p => p.Invoice)
                   .HasForeignKey(p => p.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}