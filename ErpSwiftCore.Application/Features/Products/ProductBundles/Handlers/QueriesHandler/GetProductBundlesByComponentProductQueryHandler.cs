using AutoMapper;
using Microsoft.Extensions.Logging;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundlesByComponentProductQueryHandler : BaseHandler<GetProductBundlesByComponentProductQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        private readonly IMapper _mapper;
        public GetProductBundlesByComponentProductQueryHandler(IProductBundleQueryService queryService, IMapper mapper, ILogger<GetProductBundlesByComponentProductQueryHandler> logger) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundlesByComponentProductQuery request, CancellationToken cancellationToken)
        {
            var entities = await _queryService.GetBundlesByComponentProductAsync(request.ComponentProductId, cancellationToken);
            return  entities.Select(e => _mapper.Map<ProductBundleDto>(e)).ToList();
        }
    }
}
