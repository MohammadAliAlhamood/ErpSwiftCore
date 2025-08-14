using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPricesByCurrencyQueryHandler : BaseHandler<GetPricesByCurrencyQuery>
    {
        private readonly IProductPriceQueryService _svc;
        private readonly IMapper _mapper;

        public GetPricesByCurrencyQueryHandler(
            IProductPriceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPricesByCurrencyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetPricesByCurrencyQuery req, CancellationToken ct)
        {
            var list = await _svc.GetPricesByCurrencyAsync(req.CurrencyId, ct);
            return _mapper.Map<IEnumerable<ProductPriceDto>>(list);
        }
    }


}
