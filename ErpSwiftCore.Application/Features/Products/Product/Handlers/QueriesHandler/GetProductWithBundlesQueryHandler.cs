using AutoMapper;
using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{ 
    public class GetProductWithBundlesQueryHandler : BaseHandler<GetProductWithBundlesQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductWithBundlesQueryHandler(IProductQueryService svc, IMapper mapper, ILogger<BaseHandler<GetProductWithBundlesQuery>> logger) : base(logger)
        {
            _mapper = mapper;
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductWithBundlesQuery req, CancellationToken ct)
        {
            var e = await _svc.GetProductWithBundlesAsync(req.ProductId, ct);
            return _mapper.Map<ProductDto?>(e);
        }
    }
}
