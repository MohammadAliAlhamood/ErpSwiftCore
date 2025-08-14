using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class AddOrderLinesCommandHandler : BaseHandler<AddOrderLinesCommand>
    {
        private readonly IOrderCommandService _svc;
        private readonly IMapper _mapper;
        public AddOrderLinesCommandHandler(IOrderCommandService svc, IMapper mapper, ILogger<BaseHandler<AddOrderLinesCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(AddOrderLinesCommand req, CancellationToken ct)
        {
            var lines = req.Dtos.Select(d => _mapper.Map<OrderLine>(d));
            var added = await _svc.AddOrderLinesAsync(req.OrderId, lines, ct);
            return added.Select(l => _mapper.Map<OrderLineDto>(l));
        }
    } 

}
