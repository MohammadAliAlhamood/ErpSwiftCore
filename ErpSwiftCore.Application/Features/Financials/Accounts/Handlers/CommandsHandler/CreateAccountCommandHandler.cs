using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class CreateAccountCommandHandler : BaseHandler<CreateAccountCommand>
    {
        private readonly IAccountCommandService _svc;
        private readonly IMapper _mapper;
        public CreateAccountCommandHandler(IAccountCommandService svc, IMapper mapper, ILogger<BaseHandler<CreateAccountCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(CreateAccountCommand req, CancellationToken ct)
        { 
            var entity = _mapper.Map<Account>(req.Dto);
            return await _svc.CreateAccountAsync(entity, ct);
            
        }
    }
}