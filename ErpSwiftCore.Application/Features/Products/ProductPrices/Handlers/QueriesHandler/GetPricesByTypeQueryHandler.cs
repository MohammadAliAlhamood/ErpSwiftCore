using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPricesByTypeQueryHandler : BaseHandler<GetPricesByTypeQuery>
    {
        private readonly IProductPriceQueryService _svc;
        private readonly IMapper _mapper;

        public GetPricesByTypeQueryHandler(
            IProductPriceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPricesByTypeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetPricesByTypeQuery req, CancellationToken ct)
        {
            var list = await _svc.GetPricesByTypeAsync(req.PriceType, ct);
            return _mapper.Map<IEnumerable<ProductPriceDto>>(list);
        }
    } 
}
