using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels
{
    /// <summary>
    /// تمثيل سطر في قيد اليومية
    /// </summary>
    public class JournalEntryLineDto : AuditableEntityDto
    {
        public Guid JournalEntryId { get; set; }

        public Guid AccountId { get; set; }
        // إذا أردنا تضمين تفاصيل الحساب:
        public AccountDto? Account { get; set; }

        public decimal Amount { get; set; }
        public bool IsDebit { get; set; }

        public Guid? CostCenterId { get; set; }
        // إذا أردنا تضمين تفاصيل مركز التكلفة:
        public CostCenterDto? CostCenter { get; set; }
    }

}
