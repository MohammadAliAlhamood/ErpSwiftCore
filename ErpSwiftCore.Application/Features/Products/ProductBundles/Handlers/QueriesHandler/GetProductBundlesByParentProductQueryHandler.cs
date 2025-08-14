using AutoMapper;
using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{

    public class GetProductBundlesByParentProductQueryHandler : BaseHandler<GetProductBundlesByParentProductQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        private readonly IMapper _mapper;
        public GetProductBundlesByParentProductQueryHandler(
            IProductBundleQueryService queryService,
            IMapper mapper,
            ILogger<GetProductBundlesByParentProductQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundlesByParentProductQuery request, CancellationToken cancellationToken)
        {
            var entities = await _queryService.GetBundlesByParentProductAsync(request.ParentProductId, cancellationToken);
            var dtos = entities.Select(e => _mapper.Map<ProductBundleDto>(e)).ToList();
            return dtos;
        }
    }



}
