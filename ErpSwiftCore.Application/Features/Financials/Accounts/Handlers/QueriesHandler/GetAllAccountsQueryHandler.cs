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
    public class GetAllAccountsQueryHandler : BaseHandler<GetAllAccountsQuery>
    {
        private readonly IAccountQueryService _svc;
        private readonly IMapper _mapper;
        public GetAllAccountsQueryHandler(IAccountQueryService svc, IMapper mapper, ILogger<BaseHandler<GetAllAccountsQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetAllAccountsQuery req, CancellationToken ct)
        {
            var list = await _svc.GetAllAccountsAsync(ct);
            return list.Select(e => _mapper.Map<AccountDto>(e));
        }
    }
}
