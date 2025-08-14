using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class ValidateOrderQueryHandler : BaseHandler<ValidateOrderQuery>
    {
        private readonly IOrderValidationService _svc;
        public ValidateOrderQueryHandler(IOrderValidationService svc, ILogger<BaseHandler<ValidateOrderQuery>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(ValidateOrderQuery req, CancellationToken ct)
        {
            var ok = await _svc.ValidateOrderAsync(req.OrderId, ct);
            return new { Valid = ok };
        }
    }

}
