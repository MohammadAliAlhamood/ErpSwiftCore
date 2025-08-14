using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{
    public class GetProductByIdQueryHandler : BaseHandler<GetProductByIdQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetProductByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductByIdQuery req, CancellationToken ct)
        {
            var e = await _svc.GetProductByIdAsync(req.ProductId, ct);
            return _mapper.Map<ProductDto?>(e);
        }
    }
}