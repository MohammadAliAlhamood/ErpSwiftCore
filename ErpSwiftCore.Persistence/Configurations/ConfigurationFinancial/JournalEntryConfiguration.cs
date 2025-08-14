using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationFinancial
{
    public class JournalEntryConfiguration : AuditableEntityConfiguration<JournalEntry> 
    {
        public override void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            base.Configure(builder);

            builder.Property(j => j.EntryDate).IsRequired();
            builder.Property(j => j.EntryNumber).IsRequired().HasMaxLength(20);
            builder.Property(j => j.Description).HasMaxLength(500);
            builder.Property(j => j.IsPosted).IsRequired();
            builder.Property(j => j.PostedDate).IsRequired(false);
        }
    }
}