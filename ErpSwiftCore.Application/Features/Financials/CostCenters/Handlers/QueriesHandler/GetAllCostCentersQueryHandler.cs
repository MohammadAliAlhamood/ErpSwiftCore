using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.QueriesHandler
{
    public class GetAllCostCentersQueryHandler : BaseHandler<GetAllCostCentersQuery>
    {
        private readonly ICostCenterQueryService _svc;
        private readonly IMapper _mapper;
        public GetAllCostCentersQueryHandler(ICostCenterQueryService svc, IMapper mapper, ILogger<BaseHandler<GetAllCostCentersQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetAllCostCentersQuery req, CancellationToken ct)
        {
            var list = await _svc.GetAllCostCentersAsync(ct);
            return list.Select(e => _mapper.Map<CostCenterDto>(e));
        }
    }
}
