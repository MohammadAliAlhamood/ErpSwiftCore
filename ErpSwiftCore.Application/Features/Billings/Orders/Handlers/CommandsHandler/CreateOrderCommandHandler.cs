using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class CreateOrderCommandHandler : BaseHandler<CreateOrderCommand>
    {
        private readonly IOrderCommandService _svc;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IOrderCommandService svc,IMapper mapper,ILogger<BaseHandler<CreateOrderCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(CreateOrderCommand req,CancellationToken ct)
        {
            var entity = _mapper.Map<Order>(req.Dto);
            if (req.Dto.OrderLines?.Any() == true)
            {
                var lines = req.Dto.OrderLines.Select(d => _mapper.Map<OrderLine>(d));
                var order = await _svc.CreateOrderWithLinesAsync(entity, lines, ct);
                return _mapper.Map<OrderDto>(order);
            }
            var created = await _svc.CreateOrderAsync(entity, ct);
            return _mapper.Map<OrderDto>(created);
        }
    } 
}