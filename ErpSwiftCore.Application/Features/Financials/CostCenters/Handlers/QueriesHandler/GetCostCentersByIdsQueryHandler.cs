using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Handlers.QueriesHandler
{
    // 5. Get by IDs
    public class GetCostCentersByIdsQueryHandler
        : BaseHandler<GetCostCentersByIdsQuery>
    {
        private readonly ICostCenterQueryService _svc;
        private readonly IMapper _mapper;

        public GetCostCentersByIdsQueryHandler(
            ICostCenterQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetCostCentersByIdsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetCostCentersByIdsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetCostCentersByIdsAsync(req.CenterIds, ct);
            return list.Select(e => _mapper.Map<CostCenterDto>(e));
        }
    }

}
