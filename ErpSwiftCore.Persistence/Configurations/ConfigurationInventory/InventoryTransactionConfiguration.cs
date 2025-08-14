using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationInventory
{
    public class InventoryTransactionConfiguration
       : AuditableEntityConfiguration<InventoryTransaction>,
         IEntityTypeConfiguration<InventoryTransaction>
    {
        public override void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.TransactionType)
                   .IsRequired();

            builder.Property(t => t.TransactionDate)
                   .IsRequired();

            builder.Property(t => t.Quantity)
                   .IsRequired();

            builder.Property(t => t.RunningBalance)
                   .IsRequired();

            builder.Property(t => t.ReferenceNumber)
                   .HasMaxLength(100);

            builder.Property(t => t.Notes)
                   .HasMaxLength(500);

            builder.HasOne(t => t.Inventory)
                   .WithMany(i => i.Transactions)
                   .HasForeignKey(t => t.InventoryID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.RelatedJournalEntry)
                   .WithMany()
                   .HasForeignKey(t => t.RelatedJournalEntryID);
        }
    }
}