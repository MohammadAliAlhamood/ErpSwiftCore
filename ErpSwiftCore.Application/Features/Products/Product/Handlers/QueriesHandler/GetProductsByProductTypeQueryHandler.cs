using AutoMapper;
using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{

    public class GetProductsByProductTypeQueryHandler : BaseHandler<GetProductsByProductTypeQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductsByProductTypeQueryHandler(IProductQueryService svc, IMapper mapper, ILogger<BaseHandler<GetProductsByProductTypeQuery>> logger) : base(logger)
        {
            _mapper = mapper;
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductsByProductTypeQuery req, CancellationToken ct)
        {
            var list = await _svc.GetProductsByProductTypeAsync(req.ProductTypeId, ct);
            return list.Select(e => _mapper.Map<ProductDto>(e));
        }
    }
}
