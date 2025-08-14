using ErpSwiftCore.Domain.Enums;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Queries
{
    public class GetTotalBalanceByTypeQuery : IRequest<APIResponseDto>
    {
        public TransactionType TransactionType { get; }
        public GetTotalBalanceByTypeQuery(TransactionType transactionType) => TransactionType = transactionType;
    }


}
