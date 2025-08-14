using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{

    public class GetProductsByPriceTypeQueryHandler : BaseHandler<GetProductsByPriceTypeQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;

        public GetProductsByPriceTypeQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetProductsByPriceTypeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductsByPriceTypeQuery req, CancellationToken ct)
        {
            var list = await _svc.GetProductsByPriceTypeAsync(req.PriceType, ct);
            return list.Select(e => _mapper.Map<ProductDto>(e));
        }
    }



}
