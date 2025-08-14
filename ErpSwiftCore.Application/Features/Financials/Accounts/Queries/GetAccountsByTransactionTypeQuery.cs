using ErpSwiftCore.Domain.Enums;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Queries
{
    public class GetAccountsByTransactionTypeQuery : IRequest<APIResponseDto>
    {
        public TransactionType TransactionType { get; }
        public GetAccountsByTransactionTypeQuery(TransactionType transactionType) => TransactionType = transactionType;
    }


}
