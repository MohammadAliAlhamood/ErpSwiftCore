using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.Domain.Entities.EntityFinancial
{
    public class JournalEntry : AuditableEntity
    {
        public DateTime EntryDate { get; set; }
        public string? EntryNumber { get; set; }
        public string? Description { get; set; }
        public bool IsPosted { get; set; }
        public DateTime? PostedDate { get; set; }
        public virtual ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
        public string ReferenceNumber { get; set; }
    }
}