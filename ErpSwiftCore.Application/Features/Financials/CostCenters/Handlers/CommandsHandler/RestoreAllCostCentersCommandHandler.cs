using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.CommandsHandler
{
    public class RestoreAllCostCentersCommandHandler : BaseHandler<RestoreAllCostCentersCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public RestoreAllCostCentersCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<RestoreAllCostCentersCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreAllCostCentersCommand req, CancellationToken ct)
        {
            return await _svc.RestoreAllCostCentersAsync(ct);
        }
    }
}
