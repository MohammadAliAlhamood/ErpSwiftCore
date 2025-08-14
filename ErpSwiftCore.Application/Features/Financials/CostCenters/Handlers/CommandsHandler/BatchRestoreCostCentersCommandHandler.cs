using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.CommandsHandler
{
    public class BatchRestoreCostCentersCommandHandler : BaseHandler<BatchRestoreCostCentersCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public BatchRestoreCostCentersCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<BatchRestoreCostCentersCommand>> logger) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(BatchRestoreCostCentersCommand req, CancellationToken ct)
        {
            return await _svc.RestoreCostCentersRangeAsync(req.CenterIds, ct);
        }
    }

}
