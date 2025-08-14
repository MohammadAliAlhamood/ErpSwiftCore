using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{

    public class GetSoftDeletedProductsQueryHandler : BaseHandler<GetSoftDeletedProductsQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;
        public GetSoftDeletedProductsQueryHandler(IProductQueryService svc, IMapper mapper, ILogger<BaseHandler<GetSoftDeletedProductsQuery>> logger) : base(logger)
        {
            _mapper = mapper;
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedProductsQuery req, CancellationToken ct)
        {
            var list = await _svc.GetSoftDeletedProductsAsync(ct);
            return list.Select(e => _mapper.Map<ProductDto>(e));
        }
    }
}
