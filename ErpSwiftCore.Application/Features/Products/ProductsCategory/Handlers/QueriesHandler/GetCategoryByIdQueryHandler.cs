using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using Microsoft.Extensions.Logging;  
namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Handlers.QueriesHandler
{
    public class GetCategoryByIdQueryHandler : BaseHandler<GetCategoryByIdQuery>
    {
        private readonly IProductCategoryQueryService _service;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(
            IProductCategoryQueryService service,
            IMapper mapper,
            ILogger<GetCategoryByIdQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCategoryByIdQuery request, CancellationToken ct)
        {
            var entity = await _service.GetCategoryByIdAsync(request.CategoryId, ct);
            return _mapper.Map<ProductCategoryDto>(entity);
        }
    } 
}