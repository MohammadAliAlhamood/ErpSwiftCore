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
    public class GetOrderLinesCountQueryHandler
        : BaseHandler<GetOrderLinesCountQuery>
    {
        private readonly IOrderQueryService _svc;

        public GetOrderLinesCountQueryHandler(
            IOrderQueryService svc,
            ILogger<BaseHandler<GetOrderLinesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetOrderLinesCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetOrderLinesCountAsync(req.OrderId, ct);
            return new { Count = cnt };
        }
    }

}
