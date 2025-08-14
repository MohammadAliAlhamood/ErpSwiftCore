using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{ 
    public class DeleteOrderLineCommandHandler  : BaseHandler<DeleteOrderLineCommand>
    {
        private readonly IOrderCommandService _svc;

        public DeleteOrderLineCommandHandler(IOrderCommandService svc,ILogger<BaseHandler<DeleteOrderLineCommand>> logger) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteOrderLineCommand req,CancellationToken ct)
        {
            return await _svc.DeleteOrderLineAsync(req.OrderLineId, ct);
            
        }
    }

}
