namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos
{ 
    public class AccountReconcileDto
    {
        public Guid AccountId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal BalanceChange { get; set; }
    }

}
