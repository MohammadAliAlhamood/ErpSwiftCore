using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundleWithParentProductQueryHandler : BaseHandler<GetProductBundleWithParentProductQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        private readonly IMapper _mapper;
        public GetProductBundleWithParentProductQueryHandler(
            IProductBundleQueryService queryService,
            IMapper mapper,
            ILogger<GetProductBundleWithParentProductQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundleWithParentProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await _queryService.GetBundleWithParentProductAsync(request.BundleId, cancellationToken);
            var dto = entity == null ? null : _mapper.Map<ProductBundleWithRelationsDto>(entity);
            return dto;
        }
    }


}
