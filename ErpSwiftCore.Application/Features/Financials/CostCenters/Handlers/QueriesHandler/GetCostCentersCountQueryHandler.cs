using ErpSwiftCore.Application.Features.Financials.CostCenters.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.QueriesHandler
{
    public class GetCostCentersCountQueryHandler : BaseHandler<GetCostCentersCountQuery>
    {
        private readonly ICostCenterQueryService _svc;
        public GetCostCentersCountQueryHandler(ICostCenterQueryService svc, ILogger<BaseHandler<GetCostCentersCountQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(GetCostCentersCountQuery req, CancellationToken ct)
        {
            return await _svc.GetCostCentersCountAsync(ct);
        }
    }

}
