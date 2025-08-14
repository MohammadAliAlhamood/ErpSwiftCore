using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class AddOrderLineCommandHandler : BaseHandler<AddOrderLineCommand>
    {
        private readonly IOrderCommandService _svc;
        private readonly IMapper _mapper;
        public AddOrderLineCommandHandler(IOrderCommandService svc, IMapper mapper, ILogger<BaseHandler<AddOrderLineCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(AddOrderLineCommand req, CancellationToken ct)
        {
            var line = _mapper.Map<OrderLine>(req.Dto);
            var added = await _svc.AddOrderLineAsync(req.OrderId, line, ct);
            return _mapper.Map<OrderLineDto>(added);
        }
    }


}
