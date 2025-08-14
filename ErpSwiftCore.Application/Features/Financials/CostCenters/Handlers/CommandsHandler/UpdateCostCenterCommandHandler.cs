using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.CommandsHandler
{
    public class UpdateCostCenterCommandHandler : BaseHandler<UpdateCostCenterCommand>
    {
        private readonly ICostCenterCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateCostCenterCommandHandler(ICostCenterCommandService svc, IMapper mapper, ILogger<BaseHandler<UpdateCostCenterCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateCostCenterCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<ErpSwiftCore.Domain.Entities.EntityFinancial.CostCenter>(req.Dto);
            return await _svc.UpdateCostCenterAsync(entity, ct);
        }
    }

}
