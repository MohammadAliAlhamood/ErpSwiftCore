using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPricesByProductQueryHandler : BaseHandler<GetPricesByProductQuery>
    {
        private readonly IProductPriceQueryService _svc;
        private readonly IMapper _mapper;

        public GetPricesByProductQueryHandler(
            IProductPriceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPricesByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetPricesByProductQuery req, CancellationToken ct)
        {
            var list = await _svc.GetPricesByProductAsync(req.ProductId, ct);
            return _mapper.Map<IEnumerable<ProductPriceDto>>(list);
        }
    }


}
