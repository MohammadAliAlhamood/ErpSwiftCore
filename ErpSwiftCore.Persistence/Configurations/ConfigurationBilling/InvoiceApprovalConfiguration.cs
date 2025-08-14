using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationBilling
{
    public class InvoiceApprovalConfiguration : AuditableEntityConfiguration<InvoiceApproval>, IEntityTypeConfiguration<InvoiceApproval>
    {
        public override void Configure(EntityTypeBuilder<InvoiceApproval> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Status).IsRequired().HasConversion<int>();
            builder.Property(a => a.Notes).HasMaxLength(500);
            builder.HasOne(a => a.Invoice)
                   .WithMany(i => i.Approvals)
                   .HasForeignKey(a => a.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}