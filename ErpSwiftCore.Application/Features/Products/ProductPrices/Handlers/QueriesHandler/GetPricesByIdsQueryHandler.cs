using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPricesByIdsQueryHandler : BaseHandler<GetPricesByIdsQuery>
    {
        private readonly IMapper _mapper; 
        private readonly IProductPriceQueryService _svc;
        public GetPricesByIdsQueryHandler(    IProductPriceQueryService svc,  IMapper mapper,   ILogger<BaseHandler<GetPricesByIdsQuery>> logger ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetPricesByIdsQuery req, CancellationToken ct)
        {
            var list = await _svc.GetPricesByIdsAsync(req.PriceIds, ct);
            return _mapper.Map<IEnumerable<ProductPriceDto>>(list);
        }
    }


}
