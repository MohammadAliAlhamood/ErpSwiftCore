using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetAllProductBundlesQueryHandler : BaseHandler<GetAllProductBundlesQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        private readonly IMapper _mapper;
        public GetAllProductBundlesQueryHandler(
            IProductBundleQueryService queryService,
            IMapper mapper,
            ILogger<GetAllProductBundlesQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetAllProductBundlesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _queryService.GetAllBundlesAsync(cancellationToken);
            var dtos = entities.Select(e => _mapper.Map<ProductBundleDto>(e)).ToList();
            return dtos;
        }
    }


}
