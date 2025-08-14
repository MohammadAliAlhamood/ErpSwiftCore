using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Dtos
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
        public Guid? ParentAccountId { get; set; }
        public AccountDto ParentAccount { get; set; }

    }
}