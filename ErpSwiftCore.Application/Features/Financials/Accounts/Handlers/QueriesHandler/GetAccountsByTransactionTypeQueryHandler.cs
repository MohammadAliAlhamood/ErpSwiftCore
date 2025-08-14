using AutoMapper;
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
    public class GetAccountsByTransactionTypeQueryHandler : BaseHandler<GetAccountsByTransactionTypeQuery>
    {
        private readonly IAccountQueryService _svc;
        private readonly IMapper _mapper;
        public GetAccountsByTransactionTypeQueryHandler(IAccountQueryService svc, IMapper mapper, ILogger<BaseHandler<GetAccountsByTransactionTypeQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetAccountsByTransactionTypeQuery req, CancellationToken ct)
        {
            var list = await _svc.GetAccountsByTransactionTypeAsync(req.TransactionType, ct);
            return list.Select(e => _mapper.Map<AccountDto>(e));
        }
    }

}
