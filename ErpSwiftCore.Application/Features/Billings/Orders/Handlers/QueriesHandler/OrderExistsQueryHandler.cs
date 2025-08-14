using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class OrderExistsQueryHandler
        : BaseHandler<OrderExistsQuery>
    {
        private readonly IOrderValidationService _svc;
        public OrderExistsQueryHandler(IOrderValidationService svc, ILogger<BaseHandler<OrderExistsQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(OrderExistsQuery req, CancellationToken ct)
        {
            return await _svc.OrderExistsAsync(req.OrderId, ct);
        }
    }
}
