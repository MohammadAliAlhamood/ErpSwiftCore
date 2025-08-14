using ErpSwiftCore.Domain.Enums;
using MediatR; 

namespace ErpSwiftCore.Application.Features.Financials.Accounts.Queries
{
    public class GetAccountsCountByTypeQuery : IRequest<APIResponseDto>
    {
        public TransactionType TransactionType { get; }
        public GetAccountsCountByTypeQuery(TransactionType transactionType) => TransactionType = transactionType;
    }


}
