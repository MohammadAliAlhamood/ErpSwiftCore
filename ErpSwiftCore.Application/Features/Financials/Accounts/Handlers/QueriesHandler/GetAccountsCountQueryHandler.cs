using ErpSwiftCore.Application.Features.Financials.Accounts.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.QueriesHandler
{
    public class GetAccountsCountQueryHandler : BaseHandler<GetAccountsCountQuery>
    {
        private readonly IAccountQueryService _svc;
        public GetAccountsCountQueryHandler(IAccountQueryService svc, ILogger<BaseHandler<GetAccountsCountQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(GetAccountsCountQuery req, CancellationToken ct)
        {
            return await _svc.GetAccountsCountAsync(ct);
        }
    }


}
