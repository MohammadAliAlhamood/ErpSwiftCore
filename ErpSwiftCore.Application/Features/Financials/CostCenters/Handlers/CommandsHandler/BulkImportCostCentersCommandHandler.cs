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
    public class BulkImportCostCentersCommandHandler : BaseHandler<BulkImportCostCentersCommand>
    {
        private readonly ICostCenterCommandService _svc;
        private readonly IMapper _mapper;
        public BulkImportCostCentersCommandHandler(ICostCenterCommandService svc, IMapper mapper, ILogger<BaseHandler<BulkImportCostCentersCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(BulkImportCostCentersCommand req, CancellationToken ct)
        {
            var entities = req.Centers
                .Select(d => _mapper.Map< CostCenter>(d));
            return await _svc.BulkImportCostCentersAsync(entities, ct);
        }
    }
}
