using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{ 
    public class ChangeOrderStatusCommandHandler
        : BaseHandler<ChangeOrderStatusCommand>
    {
        private readonly IOrderCommandService _svc;

        public ChangeOrderStatusCommandHandler(
            IOrderCommandService svc,
            ILogger<BaseHandler<ChangeOrderStatusCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(ChangeOrderStatusCommand req, CancellationToken ct)
        {
            var ok = await _svc.ChangeOrderStatusAsync(req.Dto.OrderId, req.Dto.NewStatus, ct);
            return new { Success = ok };
        }
    }
}
