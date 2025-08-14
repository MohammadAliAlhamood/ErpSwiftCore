using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetLatestPriceByProductQueryHandler : BaseHandler<GetLatestPriceByProductQuery>
    {
        private readonly IProductPriceQueryService _svc;
        private readonly IMapper _mapper;
        public GetLatestPriceByProductQueryHandler(
            IProductPriceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetLatestPriceByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetLatestPriceByProductQuery req, CancellationToken ct)
        {
            var entity = await _svc.GetLatestPriceByProductAsync(req.ProductId, req.PriceType, ct);
            return _mapper.Map<ProductPriceDto?>(entity);
        }
    }



}
