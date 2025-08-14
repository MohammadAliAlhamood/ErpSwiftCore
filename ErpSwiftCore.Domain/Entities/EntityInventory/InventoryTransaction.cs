using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
namespace ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots
{
    public class InventoryTransaction : AuditableEntity
    {
        public Guid InventoryID { get; set; }
        public Inventory Inventory { get; set; } = null!;
        public InventoryTransactionType TransactionType { get; set; } = InventoryTransactionType.Unknown;
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; }
        public int RunningBalance { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? Notes { get; set; }
        public Guid? RelatedJournalEntryID { get; set; }
        public JournalEntry? RelatedJournalEntry { get; set; }
    }
}