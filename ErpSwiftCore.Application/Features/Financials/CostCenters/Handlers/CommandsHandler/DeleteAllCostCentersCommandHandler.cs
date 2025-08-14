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
    public class DeleteAllCostCentersCommandHandler : BaseHandler<DeleteAllCostCentersCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public DeleteAllCostCentersCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<DeleteAllCostCentersCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteAllCostCentersCommand req, CancellationToken ct)
        {
            return await _svc.DeleteAllCostCentersAsync(ct);
        }
    }

}
