using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    // 10. Bulk delete orders
    public class BulkDeleteOrdersCommandHandler
        : BaseHandler<BulkDeleteOrdersCommand>
    {
        private readonly IOrderCommandService _svc;

        public BulkDeleteOrdersCommandHandler(
            IOrderCommandService svc,
            ILogger<BaseHandler<BulkDeleteOrdersCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkDeleteOrdersCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteOrdersRangeAsync(req.OrderIds, ct);
            return new { Success = ok };
        }
    }

}
