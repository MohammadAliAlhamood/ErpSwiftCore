using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Dtos
{

    public class TotalBalanceByTypeDto
    {
        public TransactionType TransactionType { get; set; }
        public decimal TotalBalance { get; set; }
    }

}
