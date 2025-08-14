using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.QueriesHandler
{
    public class GetOrdersByIdsQueryHandler
       : BaseHandler<GetOrdersByIdsQuery>
    {
        private readonly IOrderQueryService _svc;
        private readonly IMapper _mapper;

        public GetOrdersByIdsQueryHandler(
            IOrderQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetOrdersByIdsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetOrdersByIdsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetOrdersByIdsAsync(req.OrderIds, ct);
            return list.Select(o => _mapper.Map<OrderDto>(o));
        }
    }

}
