using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
using ErpSwiftCore.Application.Features.Financials.Accounts.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.QueriesHandler
{
    public class GetAccountByIdQueryHandler : BaseHandler<GetAccountByIdQuery>
    {
        private readonly IAccountQueryService _svc;
        private readonly IMapper _mapper;
        public GetAccountByIdQueryHandler(IAccountQueryService svc, IMapper mapper, ILogger<BaseHandler<GetAccountByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetAccountByIdQuery req, CancellationToken ct)
        {
            var entity = await _svc.GetAccountByIdAsync(req.AccountId, ct);
            return _mapper.Map<AccountDto?>(entity);
        }
    } 
}