using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class CreateOrderWithLinesCommandHandler : BaseHandler<CreateOrderWithLinesCommand>
    {
        private readonly IOrderCommandService _svc;
        private readonly IMapper _mapper;

        public CreateOrderWithLinesCommandHandler(
            IOrderCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<CreateOrderWithLinesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(CreateOrderWithLinesCommand req, CancellationToken ct)
        {
            var orderEntity = _mapper.Map<Order>(req.Dto.Order);
            var lineEntities = _mapper.Map<IEnumerable<OrderLine>>(req.Dto.OrderLines);
            var createdOrder = await _svc.CreateOrderWithLinesAsync(orderEntity, lineEntities, ct);
            return createdOrder;
        }
    }
}