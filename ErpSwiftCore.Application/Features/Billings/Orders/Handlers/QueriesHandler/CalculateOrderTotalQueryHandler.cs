using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class CalculateOrderTotalQueryHandler
      : BaseHandler<CalculateOrderTotalQuery>
    {
        private readonly IOrderValidationService _svc;

        public CalculateOrderTotalQueryHandler(
            IOrderValidationService svc,
            ILogger<BaseHandler<CalculateOrderTotalQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            CalculateOrderTotalQuery req,
            CancellationToken ct)
        {
            var total = await _svc.CalculateTotalAmountAsync(req.OrderId, ct);
            return new { Total = total };
        }
    }

}
