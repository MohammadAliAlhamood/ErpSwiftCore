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
    public class BatchDeleteCostCentersCommandHandler : BaseHandler<BatchDeleteCostCentersCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public BatchDeleteCostCentersCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<BatchDeleteCostCentersCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(BatchDeleteCostCentersCommand req, CancellationToken ct)
        {
            return await _svc.DeleteCostCentersRangeAsync(req.CenterIds, ct);
        }
    }


}
