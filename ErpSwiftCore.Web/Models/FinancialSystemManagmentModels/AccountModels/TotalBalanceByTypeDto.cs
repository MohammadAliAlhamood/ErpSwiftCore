using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels
{
    /// <summary>
    /// DTO لإجمالي الأرصدة حسب نوع المعاملة
    /// </summary>
    public class TotalBalanceByTypeDto
    {
        public TransactionType TransactionType { get; set; }
        public decimal TotalBalance { get; set; }
    }

}
