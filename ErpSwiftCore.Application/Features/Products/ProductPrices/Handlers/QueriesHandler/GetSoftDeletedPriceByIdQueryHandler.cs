using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetSoftDeletedPriceByIdQueryHandler : BaseHandler<GetSoftDeletedPriceByIdQuery>
    {
        private readonly IProductPriceQueryService _svc;
        private readonly IMapper _mapper;

        public GetSoftDeletedPriceByIdQueryHandler(
            IProductPriceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetSoftDeletedPriceByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedPriceByIdQuery req, CancellationToken ct)
        {
            var entity = await _svc.GetSoftDeletedPriceByIdAsync(req.PriceId, ct);
            return _mapper.Map<ProductPriceDto?>(entity);
        }
    } 
}
