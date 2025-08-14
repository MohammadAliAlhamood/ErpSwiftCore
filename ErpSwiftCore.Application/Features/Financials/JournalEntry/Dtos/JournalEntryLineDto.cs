using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos
{ 
    public class JournalEntryLineDto : AuditableEntityDto
    {
        public Guid JournalEntryId { get; set; }
        public Guid AccountId { get; set; }
        public AccountLookupDto? Account { get; set; }
        public decimal Amount { get; set; }
        public bool IsDebit { get; set; }
        public Guid? CostCenterId { get; set; }
        public CostCenterDto? CostCenter { get; set; }
    } 
}
