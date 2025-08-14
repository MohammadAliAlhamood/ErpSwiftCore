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
    /// <summary>
    /// معالج جلب الطلبات حسب الحالة
    /// </summary>
    public class GetOrdersByStatusQueryHandler
        : BaseHandler<GetOrdersByStatusQuery>
    {
        private readonly IOrderQueryService _svc;
        private readonly IMapper _mapper;

        public GetOrdersByStatusQueryHandler(
            IOrderQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetOrdersByStatusQuery>> logger
        ) : base(logger)
        {
            _svc = svc ?? throw new ArgumentNullException(nameof(svc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<object?> HandleRequestAsync(
            GetOrdersByStatusQuery req,
            CancellationToken ct)
        {
            var orders = await _svc.GetOrdersByStatusAsync(req.Status, ct);
            var dtos = orders.Select(o => _mapper.Map<OrderDto>(o));
            return dtos;
        }
    }
}