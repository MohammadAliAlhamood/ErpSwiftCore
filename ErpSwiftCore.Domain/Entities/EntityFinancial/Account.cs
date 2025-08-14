using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.Entities.EntityFinancial
{
    public class Account : AuditableEntity
    {
        public string? AccountNumber { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TransactionType TransactionType { get; set; } = TransactionType.Unknown;
        public AccountType AccountType { get; set; } = AccountType.General;
        public bool IsActive { get; set; } = true;
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }  
        public virtual ICollection<JournalEntryLine> JournalEntryLines { get; set; } = new List<JournalEntryLine>();
    }
}
