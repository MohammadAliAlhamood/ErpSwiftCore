using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class IsOrderLinkedToPartyQueryHandler
       : BaseHandler<IsOrderLinkedToPartyQuery>
    {
        private readonly IOrderValidationService _svc;

        public IsOrderLinkedToPartyQueryHandler(
            IOrderValidationService svc,
            ILogger<BaseHandler<IsOrderLinkedToPartyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            IsOrderLinkedToPartyQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.IsOrderLinkedToPartyAsync(req.OrderId, req.PartyId, ct);
            return new { Linked = ok };
        }
    }

}
