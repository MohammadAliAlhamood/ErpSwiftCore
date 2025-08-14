using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundleWithComponentProductQueryHandler : BaseHandler<GetProductBundleWithComponentProductQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        private readonly IMapper _mapper;
        public GetProductBundleWithComponentProductQueryHandler(
            IProductBundleQueryService queryService,
            IMapper mapper,
            ILogger<GetProductBundleWithComponentProductQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundleWithComponentProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await _queryService.GetBundleWithComponentProductAsync(request.BundleId, cancellationToken);
            var dto = entity == null ? null : _mapper.Map<ProductBundleWithRelationsDto>(entity);
            return dto;
        }
    }


}
