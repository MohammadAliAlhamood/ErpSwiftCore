using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{

    public class GetSoftDeletedProductByIdQueryHandler : BaseHandler<GetSoftDeletedProductByIdQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;

        public GetSoftDeletedProductByIdQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetSoftDeletedProductByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedProductByIdQuery req, CancellationToken ct)
        {
            var e = await _svc.GetSoftDeletedProductByIdAsync(req.ProductId, ct);
            return _mapper.Map<ProductDto?>(e);
        }
    }
}




