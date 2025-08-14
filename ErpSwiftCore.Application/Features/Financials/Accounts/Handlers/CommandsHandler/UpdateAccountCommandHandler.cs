using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.Accounts.Handlers.CommandsHandler
{
    public class UpdateAccountCommandHandler : BaseHandler<UpdateAccountCommand>
    {
        private readonly IAccountCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateAccountCommandHandler(IAccountCommandService svc, IMapper mapper, ILogger<BaseHandler<UpdateAccountCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateAccountCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<Account>(req.Dto);
            var ok = await _svc.UpdateAccountAsync(entity, ct);
            return new { Success = ok };
        }
    }


}
