using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels
{ 
    public class AccountDto : AuditableEntityDto
    {
        public string AccountNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public AccountType AccountType { get; set; }
        public bool IsActive { get; set; }
        public Guid CurrencyId { get; set; }
        public CurrencyDto Currency { get; set; } 

    }
}