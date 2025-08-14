using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class UpdateOrderLineCommandHandler : BaseHandler<UpdateOrderLineCommand>
    {
        private readonly IOrderCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateOrderLineCommandHandler(IOrderCommandService svc, IMapper mapper, ILogger<BaseHandler<UpdateOrderLineCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateOrderLineCommand req, CancellationToken ct)
        {
            var line = _mapper.Map<OrderLine>(req.Dto);
            var updated = await _svc.UpdateOrderLineAsync(line, ct);
            return _mapper.Map<OrderLineDto>(updated);
        }
    }


}
