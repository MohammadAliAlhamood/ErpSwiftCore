using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
using ErpSwiftCore.Application.Features.Financials.Accounts.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.QueriesHandler
{
    public class GetAccountsCountByTypeQueryHandler : BaseHandler<GetAccountsCountByTypeQuery>
    {
        private readonly IAccountQueryService _svc;
        public GetAccountsCountByTypeQueryHandler(IAccountQueryService svc, ILogger<BaseHandler<GetAccountsCountByTypeQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(GetAccountsCountByTypeQuery req, CancellationToken ct)
        {
            var count = await _svc.GetAccountsCountByTypeAsync(req.TransactionType, ct);
            return new AccountCountByTypeDto
            {
                TransactionType = req.TransactionType,
                Count = count
            };
        }
    }


}
