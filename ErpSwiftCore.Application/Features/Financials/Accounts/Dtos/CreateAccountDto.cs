using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Dtos
{ 
    public class CreateAccountDto
    {
        public string AccountNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public AccountType AccountType { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CurrencyId { get; set; }
        public Guid? ParentAccountId { get; set; }
    } 
} 