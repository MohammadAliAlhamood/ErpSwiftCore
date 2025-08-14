namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos
{ 
    public class AccountBalanceDto
    {
        public Guid AccountId { get; set; }
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }
        public decimal NetChange => DebitTotal - CreditTotal;
    }

}
