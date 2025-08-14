using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class HasOrderLinesQueryHandler
       : BaseHandler<HasOrderLinesQuery>
    {
        private readonly IOrderValidationService _svc;

        public HasOrderLinesQueryHandler(
            IOrderValidationService svc,
            ILogger<BaseHandler<HasOrderLinesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            HasOrderLinesQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.HasOrderLinesAsync(req.OrderId, ct);
            return new { Has = ok };
        }
    }

}
