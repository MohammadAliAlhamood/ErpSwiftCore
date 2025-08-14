namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels
{
    /// <summary>
    /// بيانات نتيجة تسوية حساب لفترة محددة
    /// (يستخدم في ReconcileAccountAsync)
    /// </summary>
    public class AccountReconcileDto
    {
        public Guid AccountId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal BalanceChange { get; set; }
    }

}
