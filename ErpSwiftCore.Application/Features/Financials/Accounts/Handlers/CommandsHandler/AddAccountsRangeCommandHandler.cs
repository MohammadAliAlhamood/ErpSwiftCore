using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class AddAccountsRangeCommandHandler : BaseHandler<AddAccountsRangeCommand>
    {
        private readonly IAccountCommandService _svc;
        private readonly IMapper _mapper;
        public AddAccountsRangeCommandHandler(IAccountCommandService svc, IMapper mapper, ILogger<BaseHandler<AddAccountsRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(AddAccountsRangeCommand req, CancellationToken ct)
        {
            var entities = req.Accounts.Select(dto => _mapper.Map<Account>(dto)).ToList();
            return await _svc.AddAccountsRangeAsync(entities, ct);
        }
    }

}
