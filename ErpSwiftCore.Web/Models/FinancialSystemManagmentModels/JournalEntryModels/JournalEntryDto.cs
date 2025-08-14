using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels; 
namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels
{ 
    public class JournalEntryDto : AuditableEntityDto
    {
        public DateTime EntryDate { get; set; }
        public string EntryNumber { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPosted { get; set; }
        public DateTime? PostedDate { get; set; }
        public string ReferenceNumber { get; set; } = null!; 
        public List<JournalEntryLineDto> Lines { get; set; } = new();
    } 
}