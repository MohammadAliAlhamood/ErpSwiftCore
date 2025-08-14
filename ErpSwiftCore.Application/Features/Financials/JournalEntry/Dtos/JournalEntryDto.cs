using ErpSwiftCore.Application.Dtos; 
namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos
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