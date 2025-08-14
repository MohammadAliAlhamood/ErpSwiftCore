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
    public class DeleteCostCenterCommandHandler : BaseHandler<DeleteCostCenterCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public DeleteCostCenterCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<DeleteCostCenterCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteCostCenterCommand req, CancellationToken ct)
        {
            return await _svc.DeleteCostCenterAsync(req.CenterId, ct);
        }
    }


}
