using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{

    public class GetOrderLinesQueryHandler : BaseHandler<GetOrderLinesQuery>
    {
        private readonly IOrderQueryService _svc;
        private readonly IMapper _mapper;
        public GetOrderLinesQueryHandler(IOrderQueryService svc, IMapper mapper, ILogger<BaseHandler<GetOrderLinesQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetOrderLinesQuery req, CancellationToken ct)
        {
            var lines = await _svc.GetOrderLinesAsync(req.OrderId, ct);
            return lines.Select(l => _mapper.Map<OrderLineDto>(l));
        }
    }


}
