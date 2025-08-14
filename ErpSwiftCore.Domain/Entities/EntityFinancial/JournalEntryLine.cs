using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.Domain.Entities.EntityFinancial
{
    public class JournalEntryLine : AuditableEntity
    {
        public Guid JournalEntryId { get; set; }
        public JournalEntry  JournalEntry { get; set; }
        public Guid AccountId { get; set; }
        public Account  Account { get; set; }
        public decimal Amount { get; set; }
        public bool IsDebit { get; set; }
        public Guid? CostCenterId { get; set; }
        public CostCenter  CostCenter { get; set; }
    }
}