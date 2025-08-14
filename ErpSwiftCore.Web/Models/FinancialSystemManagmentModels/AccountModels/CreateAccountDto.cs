using ErpSwiftCore.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels
{
    /// <summary>
    /// بيانات إنشاء حساب جديد
    /// </summary>
    public class CreateAccountDto
    {
        public string AccountNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public AccountType AccountType { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CurrencyId { get; set; } 
    }

    /// <summary>
    /// بيانات إضافة مجموعة حسابات
    /// </summary>
    public class AddAccountsRangeDto
    {
        public IEnumerable<CreateAccountDto> Accounts { get; set; }
            = new List<CreateAccountDto>();
    }

    /// <summary>
    /// بيانات حذف حساب واحد
    /// </summary>
    public class DeleteAccountDto
    {
        public Guid AccountId { get; set; }
    }

    /// <summary>
    /// بيانات حذف مجموعة حسابات
    /// </summary>
    public class DeleteAccountsRangeDto
    {
        public IEnumerable<Guid> AccountIds { get; set; }
            = new List<Guid>();
    }
     
    public class RestoreAccountDto
    {
        public Guid AccountId { get; set; }
    }

    /// <summary>
    /// بيانات استرجاع مجموعة حسابات
    /// </summary>
    public class RestoreAccountsRangeDto
    {
        public IEnumerable<Guid> AccountIds { get; set; } = new List<Guid>();
    }


    /// <summary>
    /// بيانات تحديث حساب موجود
    /// </summary>
    public class UpdateAccountDto : CreateAccountDto
    {
        public Guid ID { get; set; }
    }
}







