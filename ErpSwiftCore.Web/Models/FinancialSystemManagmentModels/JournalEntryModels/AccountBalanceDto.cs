namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels
{
    /// <summary>
    /// بيانات تجميعية لمجموع المدين والدائن لحساب معين
    /// (يستخدم في عمليات SumDebitByAccount و SumCreditByAccount)
    /// </summary>
    public class AccountBalanceDto
    {
        public Guid AccountId { get; set; }
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }
        public decimal NetChange => DebitTotal - CreditTotal;
    }

}
