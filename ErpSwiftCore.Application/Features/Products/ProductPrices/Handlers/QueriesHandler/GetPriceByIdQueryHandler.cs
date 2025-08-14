using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPriceByIdQueryHandler : BaseHandler<GetPriceByIdQuery>
    {
        private readonly IProductPriceQueryService _svc;
        private readonly IMapper _mapper;
        public GetPriceByIdQueryHandler(IProductPriceQueryService svc, IMapper mapper, ILogger<BaseHandler<GetPriceByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetPriceByIdQuery req, CancellationToken ct)
        {
            var entity = await _svc.GetPriceByIdAsync(req.PriceId, ct);
            return _mapper.Map<ProductPriceDto?>(entity);
        }
    } 
}