namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos
{
    public class AccountLookupDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
