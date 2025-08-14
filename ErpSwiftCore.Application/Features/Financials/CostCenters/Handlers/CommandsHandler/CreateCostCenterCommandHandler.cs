using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging;

namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.CommandsHandler
{
    public class CreateCostCenterCommandHandler : BaseHandler<CreateCostCenterCommand>
    {
        private readonly ICostCenterCommandService _svc;
        private readonly IMapper _mapper;
        public CreateCostCenterCommandHandler(ICostCenterCommandService svc, IMapper mapper, ILogger<BaseHandler<CreateCostCenterCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(CreateCostCenterCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map< CostCenter>(req.Dto);
            return await _svc.CreateCostCenterAsync(entity, ct);
        }
    } 
}