using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class DeleteAllOrderLinesCommandHandler
        : BaseHandler<DeleteAllOrderLinesCommand>
    {
        private readonly IOrderCommandService _svc;

        public DeleteAllOrderLinesCommandHandler(
            IOrderCommandService svc,
            ILogger<BaseHandler<DeleteAllOrderLinesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllOrderLinesCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllLinesOfOrderAsync(req.OrderId, ct);
            return new { Success = ok };
        }
    }

}
