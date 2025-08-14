using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.CommandsHandler
{
    public class AddCostCentersRangeCommandHandler : BaseHandler<AddCostCentersRangeCommand>
    {
        private readonly ICostCenterCommandService _svc;
        private readonly IMapper _mapper;
        public AddCostCentersRangeCommandHandler(ICostCenterCommandService svc, IMapper mapper, ILogger<BaseHandler<AddCostCentersRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(AddCostCentersRangeCommand req, CancellationToken ct)
        {
            var entities = req.Centers.Select(d => _mapper.Map<CostCenter>(d));
            return await _svc.AddCostCentersRangeAsync(entities, ct);
        }
    }
}
