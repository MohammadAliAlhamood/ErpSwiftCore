using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Dtos
{ 
    public class UpdateAccountDto
    {
        public Guid ID { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public AccountType AccountType { get; set; }
        public bool IsActive { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid? ParentAccountId { get; set; }
    }
}
