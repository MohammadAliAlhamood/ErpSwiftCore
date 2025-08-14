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
    public class SoftBatchDeleteCostCentersCommandHandler : BaseHandler<SoftBatchDeleteCostCentersCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public SoftBatchDeleteCostCentersCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<SoftBatchDeleteCostCentersCommand>> logger) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(SoftBatchDeleteCostCentersCommand req, CancellationToken ct)
        {
            return await _svc.SoftDeleteCostCentersRangeAsync(req.CenterIds, ct);
        }
    }

}
