using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Dtos
{
    public class AccountCountByTypeDto
    {
        public TransactionType TransactionType { get; set; }
        public int Count { get; set; }
    } 
}
