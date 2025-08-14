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
    public class GetSoftDeletedAccountByIdQueryHandler : BaseHandler<GetSoftDeletedAccountByIdQuery>
    {
        private readonly IAccountQueryService _svc;
        private readonly IMapper _mapper;
        public GetSoftDeletedAccountByIdQueryHandler(IAccountQueryService svc, IMapper mapper, ILogger<BaseHandler<GetSoftDeletedAccountByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedAccountByIdQuery req, CancellationToken ct)
        {
            var entity = await _svc.GetSoftDeletedAccountByIdAsync(req.AccountId, ct);
            return _mapper.Map<AccountDto?>(entity);
        }
    }


}
