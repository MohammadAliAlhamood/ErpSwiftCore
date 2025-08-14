using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.QueriesHandler
{
    public class GetCostCenterByIdQueryHandler : BaseHandler<GetCostCenterByIdQuery>
    {
        private readonly IMapper _mapper;
        private readonly ICostCenterQueryService _svc;
        public GetCostCenterByIdQueryHandler(ICostCenterQueryService svc, IMapper mapper, ILogger<BaseHandler<GetCostCenterByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetCostCenterByIdQuery req, CancellationToken ct)
        {
            var entity = await _svc.GetCostCenterByIdAsync(req.CenterId, ct);
            return _mapper.Map<CostCenterDto?>(entity);
        }
    }
}
