using AutoMapper;
using Microsoft.Extensions.Logging;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundleByIdQueryHandler : BaseHandler<GetProductBundleByIdQuery>
    {
        private readonly IMapper _mapper;
        private readonly IProductBundleQueryService _queryService;
        public GetProductBundleByIdQueryHandler(IProductBundleQueryService queryService, IMapper mapper, ILogger<GetProductBundleByIdQueryHandler> logger) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundleByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _queryService.GetBundleByIdAsync(request.BundleId, cancellationToken);
            var dto = entity == null ? null : _mapper.Map<ProductBundleDto>(entity);
            return dto;
        }
    }
}
