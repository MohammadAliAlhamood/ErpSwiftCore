using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class UpdateOrderCommandHandler : BaseHandler<UpdateOrderCommand>
    {
        private readonly IOrderCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateOrderCommandHandler(IOrderCommandService svc, IMapper mapper, ILogger<BaseHandler<UpdateOrderCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateOrderCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<Order>(req.Dto);
            var updated = await _svc.UpdateOrderWithLinesAsync(
                entity,
                req.Dto.LinesToAdd?.Select(d => _mapper.Map<OrderLine>(d)),
                req.Dto.LinesToUpdate?.Select(d => _mapper.Map<OrderLine>(d)),
                req.Dto.LineIdsToDelete,
                ct
            );
            return _mapper.Map<OrderDto>(updated);
        }
    }
}
