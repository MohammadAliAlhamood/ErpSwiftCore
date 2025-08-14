namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels
{ 
    public class AccountLookupDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

}
