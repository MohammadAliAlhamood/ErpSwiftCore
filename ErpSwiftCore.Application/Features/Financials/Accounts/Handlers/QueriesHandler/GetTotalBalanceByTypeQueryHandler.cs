using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
using ErpSwiftCore.Application.Features.Financials.Accounts.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.QueriesHandler
{
    public class GetTotalBalanceByTypeQueryHandler : BaseHandler<GetTotalBalanceByTypeQuery>
    {
        private readonly IAccountQueryService _svc;
        public GetTotalBalanceByTypeQueryHandler(IAccountQueryService svc, ILogger<BaseHandler<GetTotalBalanceByTypeQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(GetTotalBalanceByTypeQuery req, CancellationToken ct)
        {
            var balance = await _svc.GetTotalBalanceByTypeAsync(req.TransactionType, ct);
            return new TotalBalanceByTypeDto
            {
                TransactionType = req.TransactionType,
                TotalBalance = balance
            };
        }
    }

}
