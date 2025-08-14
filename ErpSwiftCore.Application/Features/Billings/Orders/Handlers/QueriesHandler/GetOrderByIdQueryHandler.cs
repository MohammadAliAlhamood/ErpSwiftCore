using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class GetOrderByIdQueryHandler : BaseHandler<GetOrderByIdQuery>
    {
        private readonly IOrderQueryService _svc;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(   IOrderQueryService svc,   IMapper mapper,   ILogger<BaseHandler<GetOrderByIdQuery>> logger  ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetOrderByIdQuery req,CancellationToken ct)
        {
            var order = await _svc.GetOrderByIdAsync(req.OrderId, ct);
            return _mapper.Map<OrderDto?>(order);
        }
    }
}