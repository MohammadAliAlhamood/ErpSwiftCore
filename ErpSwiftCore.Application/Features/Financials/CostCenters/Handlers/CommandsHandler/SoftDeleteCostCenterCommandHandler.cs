using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.CommandsHandler
{

    public class SoftDeleteCostCenterCommandHandler : BaseHandler<SoftDeleteCostCenterCommand>
    {
        private readonly ICostCenterCommandService _svc;
        public SoftDeleteCostCenterCommandHandler(ICostCenterCommandService svc, ILogger<BaseHandler<SoftDeleteCostCenterCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(SoftDeleteCostCenterCommand req, CancellationToken ct)
        {
            return await _svc.SoftDeleteCostCenterAsync(req.CenterId, ct);
        }
    }

}
