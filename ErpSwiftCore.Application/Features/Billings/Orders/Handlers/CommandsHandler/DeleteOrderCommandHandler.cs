using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class DeleteOrderCommandHandler : BaseHandler<DeleteOrderCommand>
    {
        private readonly IOrderCommandService _svc;
        public DeleteOrderCommandHandler(IOrderCommandService svc, ILogger<BaseHandler<DeleteOrderCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteOrderCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteOrderAsync(req.OrderId, ct);
            return new { Success = ok };
        }
    }

}
