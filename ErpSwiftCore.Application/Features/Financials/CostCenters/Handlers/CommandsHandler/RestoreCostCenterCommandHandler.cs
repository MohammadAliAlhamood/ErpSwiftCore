using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.CommandsHandler
{
    public class RestoreCostCenterCommandHandler : BaseHandler<RestoreCostCenterCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public RestoreCostCenterCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<RestoreCostCenterCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreCostCenterCommand req, CancellationToken ct)
        {
            return await _svc.RestoreCostCenterAsync(req.CenterId, ct);
        }
    }

}
