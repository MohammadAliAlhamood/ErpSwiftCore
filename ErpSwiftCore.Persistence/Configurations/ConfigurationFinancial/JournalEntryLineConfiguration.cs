using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationFinancial
{
    public class JournalEntryLineConfiguration : AuditableEntityConfiguration<JournalEntryLine> 
    {
        public override void Configure(EntityTypeBuilder<JournalEntryLine> builder)
        {
            base.Configure(builder);

            builder.Property(l => l.Amount).IsRequired();
            builder.Property(l => l.IsDebit).IsRequired();

            builder.HasOne(l => l.JournalEntry)
                   .WithMany(j => j.Lines)
                   .HasForeignKey(l => l.JournalEntryId);

            builder.HasOne(l => l.Account)
                   .WithMany(a => a.JournalEntryLines)
                   .HasForeignKey(l => l.AccountId);

            builder.HasOne(l => l.CostCenter)
                   .WithMany()
                   .HasForeignKey(l => l.CostCenterId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}