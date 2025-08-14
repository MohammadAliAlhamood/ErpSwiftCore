using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class OrderLineExistsQueryHandler
       : BaseHandler<OrderLineExistsQuery>
    {
        private readonly IOrderValidationService _svc;

        public OrderLineExistsQueryHandler(
            IOrderValidationService svc,
            ILogger<BaseHandler<OrderLineExistsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            OrderLineExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.OrderLineExistsAsync(req.OrderLineId, ct);
            return new { Exists = ok };
        }
    }


}
