using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetSoftDeletedProductBundleByIdQueryHandler : BaseHandler<GetSoftDeletedProductBundleByIdQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        private readonly IMapper _mapper;
        public GetSoftDeletedProductBundleByIdQueryHandler(
            IProductBundleQueryService queryService,
            IMapper mapper,
            ILogger<GetSoftDeletedProductBundleByIdQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedProductBundleByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _queryService.GetSoftDeletedBundleByIdAsync(request.BundleId, cancellationToken);
            var dto = entity == null ? null : _mapper.Map<ProductBundleDto>(entity);
            return dto;
        }
    } 
}
